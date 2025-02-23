using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour
{
	public GameObject lightningPrefab; 
	public string[] spawnTags = { "Bridge", "Road", "Corner", "Curve", "Intersection" };

	private GameObject currentLightning; 

	void Start()
	{
		StartCoroutine(SpawnLightningLoop()); 
	}

	private IEnumerator SpawnLightningLoop()
	{
		while (true) 
		{
			SpawnLightning(); 
			yield return new WaitForSeconds(10f); 
			Destroy(currentLightning);
		}
	}

	private void SpawnLightning()
	{
		List<GameObject> possibleSpawnPoints = new List<GameObject>();

		foreach (string tag in spawnTags)
		{
			GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tag);
			possibleSpawnPoints.AddRange(foundObjects);
		}

		if (possibleSpawnPoints.Count == 0)
		{
			Debug.LogWarning("Không tìm thấy vị trí nào để spawn tia sét!");
			return;
		}

		int randomIndex = Random.Range(0, possibleSpawnPoints.Count);
		Vector3 spawnPosition = possibleSpawnPoints[randomIndex].transform.position;

		currentLightning = Instantiate(lightningPrefab, spawnPosition, Quaternion.identity);
	}
}
