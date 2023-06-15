using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTrigger : MonoBehaviour
{
	private PlayerInputControls playerInputControls;
	private TextMeshProUGUI scoreTextBox;
	private Slider overheatSliderElement;

	[Header("Clicker Attributes")]
	[SerializeField] private float clickDelay;
	[SerializeField] private float maxClickCounterAmount;
	[SerializeField] private float clickIncreaseAmount;

	[Header("Overheat Attributes")]
	[SerializeField] private float maxOverheatAmount;

	private float currentDelay = 0f;
	private float currentClickCounterAmount;
	private bool isClicked = false;

	private void Start()
	{
		playerInputControls = GetComponent<PlayerInputControls>();
		scoreTextBox = Storage.StorageInstance.GetScoreTextBox();
		overheatSliderElement = Storage.StorageInstance.GetOverheatSlider();

		overheatSliderElement.maxValue = maxOverheatAmount;
		overheatSliderElement.value = currentClickCounterAmount;

		currentClickCounterAmount = 0;
	}
	private void Update()
	{
		InteractCheck();
		Debug.Log(currentClickCounterAmount);
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
		currentClickCounterAmount += clickIncreaseAmount;
		if(overheatSliderElement.value < maxOverheatAmount)
		{
			currentClickCounterAmount = Mathf.Clamp(currentClickCounterAmount, 0, maxClickCounterAmount);
		}
		else if (overheatSliderElement.value >= maxOverheatAmount)
		{
			currentClickCounterAmount = Mathf.Clamp(currentClickCounterAmount, 0, maxOverheatAmount);
		}

		scoreTextBox.text = currentClickCounterAmount.ToString();
		overheatSliderElement.value = currentClickCounterAmount;

		if(currentClickCounterAmount >= maxClickCounterAmount )
		{
			Debug.Log("Max click score reached");
		}
	}
}
