using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonBehaviour : MonoBehaviour
{
	private PlayerInputControls playerInputControls;
	private Animator upgradeButtonAnimator;

	private bool isInteractable = true;

	[SerializeField] private float targetScore;

	private void Start()
	{
		playerInputControls = GetComponent<PlayerInputControls>();
		upgradeButtonAnimator = GetComponent<Animator>();
	}

	private void Update()
	{
		UpgradeInteractCheck();
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
				targetScore *= 2;
				upgradeButtonAnimator.SetBool("PlayUpgradeButtonDown", true);
			}
		}
		else
		{
			upgradeButtonAnimator.SetBool("PlayUpgradeButtonDown", false);
		}
	}
}
