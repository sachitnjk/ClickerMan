using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
	private PlayerInputControls playerInputControls;

	[SerializeField] private float clickDelay;

	private float currentDelay = 0f;
	private bool isClicked = false;

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
		if(currentDelay > 0f) 
		{
			currentDelay -= Time.deltaTime;
		}

		if(currentDelay <= 0f && playerInputControls.leftClick.WasPressedThisFrame())
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.collider == GetComponent<Collider>())
			{
				currentDelay = clickDelay;
				isClicked = true;
			}
		}
		else
		{
			isClicked = false;
		}
	}

	public bool GetClickStatus()
	{
		return isClicked;
	}
}
