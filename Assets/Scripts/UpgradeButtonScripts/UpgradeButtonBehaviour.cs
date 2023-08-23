using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using TMPro;
using UnityEngine;

public class UpgradeButtonBehaviour : MonoBehaviour
{
	private PlayerInputControls playerInputControls;
	private Animator upgradeButtonAnimator;

	private bool isInteractable = true;

	[SerializeField] private float targetScore;
	[SerializeField] private TextMeshProUGUI requiredScore;
	[SerializeField] private Upgrades upgradeForThisButton;

	public enum Upgrades
	{
		IncreaseGeneratedScore,
		ReduceScoreIncreaseRate,
		InstantiteNewMechArm
	}

	private void Start()
	{
		playerInputControls = GetComponent<PlayerInputControls>();
		upgradeButtonAnimator = GetComponent<Animator>();
	}

	private void Update()
	{
		UpgradeInteractCheck();
		requiredScore.text = targetScore.ToString();
	}

	private void UpgradeInteractCheck()
	{
		if (Storage.StorageInstance.GetCurrentScore() >= targetScore)
		{
			isInteractable = true;
		}
		else
		{
			isInteractable = false;
		}

		if (playerInputControls.leftClick.WasPressedThisFrame() && isInteractable) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.collider == GetComponent<Collider>())
			{
				Storage.StorageInstance.SetCurrentScore(targetScore);
				targetScore *= 3;
				upgradeButtonAnimator.SetBool("PlayUpgradeButtonDown", true);

				ApplyUpgradeFunctionality(upgradeForThisButton);
			}
		}
		else
		{
			upgradeButtonAnimator.SetBool("PlayUpgradeButtonDown", false);
		}
	}

	private void ApplyUpgradeFunctionality(Upgrades selectedUpgrades)
	{
		if(Storage.StorageInstance.scoreIncreaseRate >= 0.6f)
		{
			isInteractable = true;
		}
		else
		{
			isInteractable = false;
		}

		switch(selectedUpgrades) 
		{
			case Upgrades.IncreaseGeneratedScore:
				Storage.StorageInstance.mechArmGeneratedScore += 1f;
				break;
			case Upgrades.ReduceScoreIncreaseRate:
				Storage.StorageInstance.scoreIncreaseRate -= 0.3f;
				break;
			case Upgrades.InstantiteNewMechArm:
				ObjectPool.Instance.ActivateTurret();
				break;
			default: 
				break;
		}
	}
}
