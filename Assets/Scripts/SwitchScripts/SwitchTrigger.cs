using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
	private PlayerInputControls playerInputControls;

	private void Start()
	{
		playerInputControls = GetComponent<PlayerInputControls>();
	}
	private void Update()
	{
		InteractCheck();
	}

	void InteractCheck()
	{
		if(playerInputControls.leftClick.WasPressedThisFrame())
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.collider == GetComponent<Collider>()) 
			{
				Debug.Log("Click is detected");
			}
		}
	}
}
