using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	public GameObject obstaclePrefab;     // Prefab chướng ngại vật
	public Transform[] roadPoints;         // Mảng các điểm trên đường
	public int numberOfObstacles = 5;     // Số lượng chướng ngại vật

	void Start()
	{
		SpawnObstacles();
	}

	public void SpawnObstacles()
	{
		// Xóa obstacles cũ
		GameObject[] oldObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		foreach (GameObject obstacle in oldObstacles)
		{
			Destroy(obstacle);
		}

		// Tạo list các vị trí có thể spawn
		List<int> availablePositions = new List<int>();
		for (int i = 0; i < roadPoints.Length; i++)
		{
			availablePositions.Add(i);
		}

		// Spawn obstacles tại các điểm ngẫu nhiên trên đường
		for (int i = 0; i < Mathf.Min(numberOfObstacles, roadPoints.Length); i++)
		{
			if (availablePositions.Count == 0) break;

			// Chọn ngẫu nhiên một vị trí từ các vị trí còn lại
			int randomIndex = Random.Range(0, availablePositions.Count);
			int positionIndex = availablePositions[randomIndex];

			// Spawn obstacle tại vị trí đã chọn
			Vector3 spawnPosition = roadPoints[positionIndex].position;
			GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
			newObstacle.tag = "Obstacle";

			// Xóa vị trí đã sử dụng
			availablePositions.RemoveAt(randomIndex);
		}
	}
}
