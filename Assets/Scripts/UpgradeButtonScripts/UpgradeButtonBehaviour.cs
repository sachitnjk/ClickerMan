using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeButtonBehaviour : MonoBehaviour
{
	private PlayerInputControls playerInputControls;
	private MeshRenderer buttonMeshRenderer;
	private Animator upgradeButtonAnimator;

	public bool isInteractable = true;
	private bool increaseRateUpgradeMaxed;

	[SerializeField] private float targetScore;
	[SerializeField] private Material inactiveMat;
	[SerializeField] private Material activeMat;
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
		buttonMeshRenderer = GetComponent<MeshRenderer>();

		increaseRateUpgradeMaxed = false;
	}

	private void Update()
	{
		FlagChecks();
		UpgradeInteractCheck();
		if(upgradeForThisButton == Upgrades.ReduceScoreIncreaseRate && increaseRateUpgradeMaxed)
		{
			requiredScore.text = "Upgrade Maxed";
		}
		else
		{
			requiredScore.text = targetScore.ToString();
		}
	}

	private void UpgradeInteractCheck()
	{
		if (playerInputControls.leftClick.WasPressedThisFrame() && isInteractable) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.collider == GetComponent<Collider>())
			{
				Storage.StorageInstance.SetCurrentScore(targetScore, increaseRateUpgradeMaxed);
				targetScore *= 2f;
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
		switch(selectedUpgrades) 
		{
			case Upgrades.IncreaseGeneratedScore:
				Storage.StorageInstance.mechArmGeneratedScore += 1f;
				break;
			case Upgrades.ReduceScoreIncreaseRate:
				if(Storage.StorageInstance.scoreIncreaseRate >= 0.6f)
				{
					Storage.StorageInstance.scoreIncreaseRate -= 0.3f;
				}
				break;
			case Upgrades.InstantiteNewMechArm:
				ObjectPool.Instance.ActivateMechArm();
				break;
			default: 
				break;
		}
	}

	private void FlagChecks()
	{
		if (Storage.StorageInstance.scoreIncreaseRate < 0.6f)
		{
			increaseRateUpgradeMaxed = true;
		}

		if (Storage.StorageInstance.GetCurrentScore() >= targetScore)
		{
			if (upgradeForThisButton == Upgrades.ReduceScoreIncreaseRate && increaseRateUpgradeMaxed)
			{
				isInteractable = false;
			}
			else
			{
				isInteractable = true;
			}
		}
		else
		{
			isInteractable = false;
		}

		if(isInteractable) 
		{
			buttonMeshRenderer.material = activeMat;
		}
		else
		{
			buttonMeshRenderer.material = inactiveMat;
		}
	}

}
