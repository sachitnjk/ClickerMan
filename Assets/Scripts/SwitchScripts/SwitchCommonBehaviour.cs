using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCommonBehaviour : MonoBehaviour
{
	private PlayerInputControls playerInputControls;
	private TextMeshProUGUI scoreTextBox;

	[Header("Clicker Attributes")]
	[SerializeField] private GameObject floatingText;
	[SerializeField] private float increaseAmount;

	private float increaseAmountWhileDecrease;
	private float clickIncreaseAmount;

	private float currentClickCounterAmount;
	private float existingCounterAmount;

	private bool isClicked = false;

	protected virtual void Start()
	{
		playerInputControls = GetComponent<PlayerInputControls>();
		scoreTextBox = Storage.StorageInstance.GetScoreTextBox();

		increaseAmountWhileDecrease = increaseAmount / 2;
		clickIncreaseAmount = increaseAmount;

		currentClickCounterAmount = 0;
	}

	private void Update()
	{
		InteractCheck();
	}

	private void InteractCheck()
	{
		if ( playerInputControls.leftClick.WasPressedThisFrame())
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.collider == GetComponent<Collider>())
			{
				isClicked = true;
				ClickCounterIncrease();
			}
		}
		else
		{
			isClicked = false;
		}
	}

	protected virtual void ClickCounterIncrease()
	{
		float.TryParse(scoreTextBox.text, out existingCounterAmount);
		currentClickCounterAmount = existingCounterAmount + clickIncreaseAmount;

		scoreTextBox.text = currentClickCounterAmount.ToString();
		if(floatingText)
		{
			ShowFloatingText();
		}
	}

	private void ShowFloatingText()
	{
		var floatingTextObject = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
		floatingTextObject.GetComponent<TextMesh>().text = clickIncreaseAmount.ToString();
	}

	protected virtual void ClickAmount_SliderDecrease()
	{
		clickIncreaseAmount = increaseAmountWhileDecrease;
	}
	protected virtual void ClickAmount_Normal() 
	{
		clickIncreaseAmount = increaseAmount;
	}
}
