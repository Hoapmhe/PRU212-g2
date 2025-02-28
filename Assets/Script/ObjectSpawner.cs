using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnObject
{
    public string name;
    public GameObject prefab;
    public int numberToSpawn = 5;
    public string tag;
    public CarColor locationColor;
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

        List<SpawnObject> greenLocations = new List<SpawnObject>();
        List<SpawnObject> blueLocations = new List<SpawnObject>();
        List<SpawnObject> yellowLocations = new List<SpawnObject>();

        foreach (SpawnObject spawnObject in objectsToSpawn)
        {
            if (spawnObject.tag == "Location") // Chỉ xử lý Location theo màu
            {
                switch (spawnObject.locationColor)
                {
                    case CarColor.Green:
                        greenLocations.Add(spawnObject);
                        break;
                    case CarColor.Blue:
                        blueLocations.Add(spawnObject);
                        break;
                    case CarColor.Yellow:
                        yellowLocations.Add(spawnObject);
                        break;
                }
            }
            else
            {
                SpawnObjects(spawnObject); // Các object khác như Rock, Package không bị ảnh hưởng
            }
        }

        SpawnLocations(greenLocations);
        SpawnLocations(blueLocations);
        SpawnLocations(yellowLocations);
    }


    private void SpawnObjects(SpawnObject spawnObject)
    {
        List<GameObject> spawnableObjects = new List<GameObject>();

        // Tìm tất cả các vị trí hợp lệ từ Bridge, Road, Corner, Curve, Intersection
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

        if (spawnableObjects.Count == 0)
        {
            Debug.LogWarning($"Không tìm thấy vị trí hợp lệ để spawn {spawnObject.name}");
            return;
        }

        for (int i = 0; i < spawnObject.numberToSpawn; i++)
        {
            if (spawnableObjects.Count == 0) break; // Nếu hết vị trí thì dừng lại

            int randomIndex = Random.Range(0, spawnableObjects.Count);
            GameObject spawnPosition = spawnableObjects[randomIndex];
            Vector3 position = spawnPosition.transform.position;

            GameObject newObject = Instantiate(spawnObject.prefab, position, Quaternion.identity);
            newObject.tag = spawnObject.tag;

            usedPositions.Add(position);
            spawnableObjects.RemoveAt(randomIndex); // Xóa vị trí đã sử dụng
        }
    }


    private void SpawnLocations(List<SpawnObject> locationObjects)
    {
        if (locationObjects.Count == 0) return;

        List<GameObject> spawnableObjects = new List<GameObject>();

        // Tìm tất cả vị trí hợp lệ để spawn
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

        if (spawnableObjects.Count < locationObjects.Count)
        {
            Debug.LogWarning($"Không đủ vị trí để spawn {locationObjects.Count} Location.");
        }

        foreach (SpawnObject location in locationObjects)
        {
            if (spawnableObjects.Count == 0) break;

            int randomIndex = Random.Range(0, spawnableObjects.Count);
            GameObject spawnPosition = spawnableObjects[randomIndex];
            Vector3 position = spawnPosition.transform.position;

            // ✅ Tạo đúng Prefab dựa trên LocationColor
            GameObject newObject = Instantiate(location.prefab, position, Quaternion.identity);
            newObject.tag = location.tag;

            // ✅ Kiểm tra xem object có component Location không
            Location locComponent = newObject.GetComponent<Location>();
            if (locComponent != null)
            {
                locComponent.locationColor = location.locationColor;
                locComponent.UpdateLocationColor();
            }

            usedPositions.Add(position);
            spawnableObjects.RemoveAt(randomIndex);
        }
    }


    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
    }
}
