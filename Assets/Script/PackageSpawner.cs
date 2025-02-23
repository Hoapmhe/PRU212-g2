using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
	public GameObject packagePrefab;
	public Transform[] spawnPoints;
	public int numberOfPackages = 5;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		SpawnPackages();
	}
	public void SpawnPackages()
	{
		// Xóa tất cả package cũ
		GameObject[] oldPackages = GameObject.FindGameObjectsWithTag("Package");
		foreach (GameObject package in oldPackages)
		{
			Destroy(package);
		}

		// Tạo danh sách các vị trí có thể spawn
		List<int> availablePositions = new List<int>();
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			availablePositions.Add(i);
		}

		// Spawn packages tại các điểm ngẫu nhiên
		for (int i = 0; i < Mathf.Min(numberOfPackages, spawnPoints.Length); i++)
		{
			if (availablePositions.Count == 0) break;

			// Chọn ngẫu nhiên một vị trí
			int randomIndex = Random.Range(0, availablePositions.Count);
			int positionIndex = availablePositions[randomIndex];

			// Spawn package
			Vector3 spawnPosition = spawnPoints[positionIndex].position;
			GameObject newPackage = Instantiate(packagePrefab, spawnPosition, Quaternion.identity);
			newPackage.tag = "Package";

			// Xóa vị trí đã dùng
			availablePositions.RemoveAt(randomIndex);
		}
	}

}
