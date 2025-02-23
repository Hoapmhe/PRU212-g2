using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	public GameObject obstaclePrefab;     
	public Transform[] roadPoints;        
	public int numberOfObstacles = 5;     

	void Start()
	{
		SpawnObstacles();
	}

	public void SpawnObstacles()
	{
		GameObject[] oldObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		foreach (GameObject obstacle in oldObstacles)
		{
			Destroy(obstacle);
		}

		List<int> availablePositions = new List<int>();
		for (int i = 0; i < roadPoints.Length; i++)
		{
			availablePositions.Add(i);
		}

		for (int i = 0; i < Mathf.Min(numberOfObstacles, roadPoints.Length); i++)
		{
			if (availablePositions.Count == 0) break;

			int randomIndex = Random.Range(0, availablePositions.Count);
			int positionIndex = availablePositions[randomIndex];

			Vector3 spawnPosition = roadPoints[positionIndex].position;
			GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
			newObstacle.tag = "Obstacle";

			availablePositions.RemoveAt(randomIndex);
		}
	}
}
