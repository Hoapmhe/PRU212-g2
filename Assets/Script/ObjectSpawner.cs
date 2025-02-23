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

	private List<Vector3> usedPositions = new List<Vector3>(); 

	void Start()
	{
		SpawnAllObjects();
	}

	public void SpawnAllObjects()
	{
		usedPositions.Clear(); 

		foreach (SpawnObject spawnObject in objectsToSpawn)
		{
			SpawnObjects(spawnObject);
		}
	}

	private void SpawnObjects(SpawnObject spawnObject)
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag(spawnObject.tag))
		{
			Destroy(obj);
		}

		List<GameObject> spawnableObjects = new List<GameObject>();
		foreach (string tag in spawnObjectTags)
		{
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
			{
				if (!usedPositions.Contains(obj.transform.position)) 
				{
					spawnableObjects.Add(obj);
				}
			}
		}

		if (spawnableObjects.Count < spawnObject.numberToSpawn)
		{
			Debug.LogWarning($"Không đủ vị trí để spawn {spawnObject.name}. Chỉ có {spawnableObjects.Count} vị trí.");
		}

		int spawnedCount = 0;
		while (spawnedCount < spawnObject.numberToSpawn && spawnableObjects.Count > 0)
		{
			int randomIndex = Random.Range(0, spawnableObjects.Count);
			GameObject spawnPosition = spawnableObjects[randomIndex];
			Vector3 position = spawnPosition.transform.position;

			GameObject newObject = Instantiate(spawnObject.prefab, position, spawnObject.prefab.transform.rotation);
			newObject.transform.localScale = Vector3.one;
			newObject.tag = spawnObject.tag;

			usedPositions.Add(position); 
			spawnableObjects.RemoveAt(randomIndex); 

			spawnedCount++;
		}

		if (spawnedCount < spawnObject.numberToSpawn)
		{
			Debug.LogWarning($"Không đủ vị trí để spawn {spawnObject.name}. Chỉ spawn được {spawnedCount}/{spawnObject.numberToSpawn}");
		}
	}
}
