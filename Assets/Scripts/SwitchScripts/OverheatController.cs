using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatController : SwitchCommonBehaviour
{
	private Slider overheatSlider;

	[Header("Overheat Slider Attributes")]
	[SerializeField] private float overheatSliderIncreaseAmount;
	[SerializeField] private float maxOverheatSliderAmount;
	[SerializeField] private float overheatDecreaseTimeDelay;
	[SerializeField] private float overheatSliderDecreaseOverTimeAmount;

	private bool overheatDecreasing = false;

	protected virtual void Start()
	{
		base.Start();	

		overheatSlider = Storage.StorageInstance.GetOverheatSlider();
		overheatSlider.maxValue = maxOverheatSliderAmount;
		overheatSlider.value = 0f;
	}

	protected override void ClickCounterIncrease()
	{
		base.ClickCounterIncrease();
		OverheatSliderIncrease();
	}

	protected virtual void OverheatSliderIncrease()
	{
		if(overheatSlider.value != maxOverheatSliderAmount && base.GetCanInteractStatus()) 
		{
			overheatSlider.value += overheatSliderIncreaseAmount;
			if(overheatSlider.value >= maxOverheatSliderAmount)
			{
				base.SetCanInteractStatus(false);
				StartCoroutine(OverheatSliderDecreaseOverTime());
			}
		}
	}

	IEnumerator OverheatSliderDecreaseOverTime() 
	{
		overheatDecreasing = true;

		yield return new WaitForSeconds(overheatDecreaseTimeDelay);

		while(overheatSlider.value > 0f)
		{
			overheatSlider.value -= overheatSliderDecreaseOverTimeAmount * Time.deltaTime;

			yield return null;
		}

		overheatDecreasing = false;
		base.SetCanInteractStatus(true);
	}
}
