using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpawnObject
{
	public string name;
	public GameObject prefab;
	public int numberToSpawn = 5;
	public string tag;
}
public class ObjectSpawner : MonoBehaviour
{
	public SpawnObject[] objectsToSpawn;
	public string[] spawnObjectTags = { "Bridge", "Road", "Corner", "Curve", "Intersection" };

	// Danh sách lưu trữ các vị trí đã được sử dụng
	private HashSet<Vector3> usedPositions = new HashSet<Vector3>();

	void Start()
	{
		SpawnAllObjects();
	}


	public void SpawnAllObjects()
	{
		// Reset danh sách vị trí đã sử dụng khi bắt đầu spawn mới
		usedPositions.Clear();

		foreach (SpawnObject spawnObject in objectsToSpawn)
		{
			SpawnObjects(spawnObject);
		}
	}

	private void SpawnObjects(SpawnObject spawnObject)
	{
		// Xóa object cũ trước khi spawn
		GameObject[] oldObjects = GameObject.FindGameObjectsWithTag(spawnObject.tag);
		foreach (GameObject obj in oldObjects)
		{
			Vector3 pos = obj.transform.position;
			usedPositions.Remove(pos); // Xóa vị trí cũ khỏi danh sách đã sử dụng
			Destroy(obj);
		}

		// Lấy danh sách vị trí hợp lệ
		List<GameObject> spawnableObjects = new List<GameObject>();
		foreach (string tag in spawnObjectTags)
		{
			GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
			foreach (GameObject obj in taggedObjects)
			{
				// Chỉ thêm vào danh sách nếu vị trí chưa được sử dụng
				if (!usedPositions.Contains(obj.transform.position))
				{
					spawnableObjects.Add(obj);
				}
			}
		}

		if (spawnableObjects.Count < spawnObject.numberToSpawn)
		{
			Debug.LogWarning($"Không đủ vị trí để spawn {spawnObject.name}. Có {spawnableObjects.Count} vị trí.");
		}

		int spawnedCount = 0;
		while (spawnedCount < spawnObject.numberToSpawn && spawnableObjects.Count > 0)
		{
			int randomIndex = Random.Range(0, spawnableObjects.Count);
			GameObject spawnPosition = spawnableObjects[randomIndex];
			Vector3 position = spawnPosition.transform.position;

			// Kiểm tra lại để đảm bảo vị trí vẫn chưa được sử dụng
			if (!usedPositions.Contains(position))
			{
				Quaternion rotation = spawnObject.prefab.transform.rotation;

				// Tạo object mới
				GameObject newObject = Instantiate(spawnObject.prefab, position, rotation);
				newObject.transform.localScale = Vector3.one;
				newObject.tag = spawnObject.tag;

				// Thêm vị trí vào danh sách đã sử dụng
				usedPositions.Add(position);
				spawnedCount++;
			}

			// Xóa vị trí khỏi danh sách để tránh trùng lặp
			spawnableObjects.RemoveAt(randomIndex);
		}

		if (spawnedCount < spawnObject.numberToSpawn)
		{
			Debug.LogWarning($"Không đủ chỗ để spawn {spawnObject.name}. Spawn được {spawnedCount}/{spawnObject.numberToSpawn}");
		}
	}
}
