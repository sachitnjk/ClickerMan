using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	public static ObjectPool Instance { get; private set; }

	[System.Serializable]
	private class TurretData
	{
		public Vector3 position;
	}

	[SerializeField] private GameObject mechTurretPrefab;
	[SerializeField] private int poolSize;
	[SerializeField] private List<TurretData> turretDataList = new List<TurretData>();
	private int currentTurretIndex = 0;

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

		foreach (TurretData turretData in turretDataList)
		{
			GameObject newTurret = Instantiate(mechTurretPrefab, turretData.position, Quaternion.identity);
			newTurret.SetActive(false);
			mechTurrets.Add(newTurret);
		}
	}

	public void ActivateTurret()
	{
		GameObject inactiveTurret = mechTurrets.Find(turret => !turret.activeSelf);

		if (inactiveTurret != null)
		{
			inactiveTurret.SetActive(true);
		}
	}

	public void DeactivateTurret(GameObject turret)
	{
		turret.SetActive(false);
	}
}
