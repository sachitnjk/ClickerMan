using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	public static ObjectPool Instance { get; private set; }

	[SerializeField] private GameObject mechArmPrefab;
	[SerializeField] private int poolSize;
	[SerializeField] private GameObject spawnArea;
	[SerializeField] private float minDistanceBetweenEachArm;

	private List<GameObject> mechTurrets = new List<GameObject>();

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.LogWarning("Duplicate ObjectPool instance found. Destroying the duplicate.");
			Destroy(gameObject);
			return;
		}

		Renderer spawnRenderer = spawnArea.GetComponent<Renderer>();
		Vector3 spawnAreaMin = spawnRenderer.bounds.min;
		Vector3 spawnAreaMax = spawnRenderer.bounds.max;

		float cellSize = minDistanceBetweenEachArm * 2;

		// Generate valid spawn positions using grid-based approach
		List<Vector3> validSpawnPositions = GenerateValidSpawnPositions(spawnAreaMin, spawnAreaMax, cellSize);

		// Spawn MechArm objects at valid positions
		for (int i = 0; i < poolSize && i < validSpawnPositions.Count; i++)
		{
			Vector3 spawnPosition = validSpawnPositions[i];
			GameObject newTurret = Instantiate(mechArmPrefab, spawnPosition, Quaternion.identity);
			newTurret.SetActive(false);
			mechTurrets.Add(newTurret);
		}
	}

	private List<Vector3> GenerateValidSpawnPositions(Vector3 minBounds, Vector3 maxBounds, float cellSize)
	{
		List<Vector3> validPositions = new List<Vector3>();

		for (float x = minBounds.x; x <= maxBounds.x; x += cellSize)
		{
			for (float z = minBounds.z; z <= maxBounds.z; z += cellSize)
			{
				Vector3 gridCellCenter = new Vector3(x + cellSize / 2, maxBounds.y, z + cellSize / 2);

				if (IsValidSpawnPosition(gridCellCenter))
				{
					validPositions.Add(gridCellCenter);
				}
			}
		}
		return validPositions;
	}

	private bool IsValidSpawnPosition(Vector3 position)
	{
		foreach (GameObject turret in mechTurrets)
		{
			if (Vector3.Distance(position, turret.transform.position) < minDistanceBetweenEachArm)
			{
				return false;
			}
		}
		return true;
	}

	public void ActivateMechArm()
	{
		GameObject inactiveTurret = mechTurrets.Find(turret => !turret.activeSelf);

		if (inactiveTurret != null)
		{
			inactiveTurret.SetActive(true);
		}
	}

	public void DeactivateMechArm(GameObject turret)
	{
		turret.SetActive(false);
	}
}
