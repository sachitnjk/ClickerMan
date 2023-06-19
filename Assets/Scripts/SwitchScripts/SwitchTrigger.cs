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
	[SerializeField] private float overheatIncreaseAmount;
	[SerializeField] private float maxOverheatAmount;
	[SerializeField] private float overheatDecreaseOverTimeAmount;
	[SerializeField] private float overheatDecrementTimeDelay;

	private float currentDelay = 0f;
	private float currentClickCounterAmount;
	private bool isClicked = false;
	private bool overheatDecreasing  = false;
	private bool canInteract  = true;

	private void Start()
	{
		playerInputControls = GetComponent<PlayerInputControls>();
		scoreTextBox = Storage.StorageInstance.GetScoreTextBox();
		overheatSliderElement = Storage.StorageInstance.GetOverheatSlider();

		overheatSliderElement.maxValue = maxOverheatAmount;
		overheatSliderElement.value = Mathf.Clamp(overheatSliderElement.value, 0f, maxOverheatAmount);

		currentClickCounterAmount = 0;
	}
	private void Update()
	{
		InteractCheck();
		Debug.Log(currentClickCounterAmount);
		Debug.Log(overheatDecreasing);
	}

	void InteractCheck()
	{
		if(currentDelay > 0f) 
		{
			currentDelay -= Time.deltaTime;
		}

		if(currentDelay <= 0f && playerInputControls.leftClick.WasPressedThisFrame() && canInteract)
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

		scoreTextBox.text = currentClickCounterAmount.ToString();
		
		OverheatCounterHandling();

		if(currentClickCounterAmount >= maxClickCounterAmount )
		{
			Debug.Log("Max click score reached");
		}

	}

	void OverheatCounterHandling()
	{
		if(overheatSliderElement.value < maxOverheatAmount && !overheatDecreasing && canInteract)
		{
			overheatSliderElement.value += overheatIncreaseAmount;

			if(overheatSliderElement.value >= maxOverheatAmount)
			{
				StartCoroutine(DecrementOverheatSliderValue());
			}
		}
	}

	IEnumerator DecrementOverheatSliderValue()
	{
		canInteract = false;
		overheatDecreasing = true;

		yield return new WaitForSeconds(overheatDecrementTimeDelay);

		while (overheatSliderElement.value > 0f)
		{
			overheatSliderElement.value -= overheatDecreaseOverTimeAmount * Time.deltaTime;

			yield return null;
		}

		overheatDecreasing = false;
		canInteract= true;
	}
}
