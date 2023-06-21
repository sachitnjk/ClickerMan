using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwitchCommonBehaviour : MonoBehaviour
{
	private PlayerInputControls playerInputControls;
	private TextMeshProUGUI scoreTextBox;

	[Header("Clicker Attributes")]
	[SerializeField] private float clickDelay;
	[SerializeField] private float maxClickCounterAmount;
	[SerializeField] private float clickIncreaseAmount;

	private float currentDelay = 0f;
	private float currentClickCounterAmount;
	private float existingCounterAmount;

	private bool isClicked = false;
	private bool canInteract = true;

	protected virtual void Start()
	{
		playerInputControls = GetComponent<PlayerInputControls>();
		scoreTextBox = Storage.StorageInstance.GetScoreTextBox();

		currentClickCounterAmount = Mathf.Clamp(currentClickCounterAmount, 0f, maxClickCounterAmount);
		currentClickCounterAmount = 0;
	}

	private void Update()
	{
		InteractCheck();
	}

	private void InteractCheck()
	{
		if (currentDelay > 0f)
		{
			currentDelay -= Time.deltaTime;
		}

		if (currentDelay <= 0f && playerInputControls.leftClick.WasPressedThisFrame() && canInteract)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.collider == GetComponent<Collider>())
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

	protected virtual void ClickCounterIncrease()
	{
		float.TryParse(scoreTextBox.text, out existingCounterAmount);
		currentClickCounterAmount = existingCounterAmount + clickIncreaseAmount;

		scoreTextBox.text = currentClickCounterAmount.ToString();

		if (currentClickCounterAmount >= maxClickCounterAmount)
		{
			Debug.Log("Max click score reached");
		}

	}
}
