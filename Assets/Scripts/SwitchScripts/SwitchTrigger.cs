using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
	private PlayerInputControls playerInputControls;
	private TextMeshProUGUI scoreTextBox;

	[Header("Clicker Attributes")]
	[SerializeField] private float clickDelay;
	[SerializeField] private float maxClickCounterAmount;
	[SerializeField] private float clickIncreaseAmount;

	private float currentDelay = 0f;
	private float currentClickCounter;
	private bool isClicked = false;

	private void Start()
	{
		playerInputControls = GetComponent<PlayerInputControls>();
		scoreTextBox = Storage.StorageInstance.GetScoreTextBox();

		currentClickCounter = 0;
	}
	private void Update()
	{
		InteractCheck();
		Debug.Log(currentClickCounter);
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
				ClickCounterIncrease();
			}
		}
		else
		{
			isClicked = false;
		}
	}

	void ClickCounterIncrease()
	{
		currentClickCounter += clickIncreaseAmount;
		currentClickCounter = Mathf.Clamp(currentClickCounter, 0, maxClickCounterAmount);

		scoreTextBox.text = currentClickCounter.ToString();

		if(currentClickCounter >= maxClickCounterAmount )
		{
			Debug.Log("Max click score reached");
		}
	}

	public bool GetClickStatus()
	{
		return isClicked;
	}
}
