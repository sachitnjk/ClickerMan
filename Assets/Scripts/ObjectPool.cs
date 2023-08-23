using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	public static ObjectPool Instance { get; private set; }

	[System.Serializable]
	private class MechArmData
	{
		public Vector3 position;
	}

	[SerializeField] private GameObject mechArmPrefab;
	[SerializeField] private int poolSize;
	[SerializeField] private List<MechArmData> mechArmDataList = new List<MechArmData>();

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

		foreach (MechArmData turretData in mechArmDataList)
		{
			GameObject newTurret = Instantiate(mechArmPrefab, turretData.position, Quaternion.identity);
			newTurret.SetActive(false);
			mechTurrets.Add(newTurret);
		}
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
