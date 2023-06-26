using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatController : SwitchCommonBehaviour
{
	private Slider overheatSlider;
	private Slider coolingSlider;

	[Header("Overheat Slider Attributes")]
	[SerializeField] private float overheatSliderIncreaseAmount;
	[SerializeField] private float maxOverheatSliderAmount;
	[SerializeField] private float overheatDecreaseTimeDelay;
	[SerializeField] private float overheatSliderDecreaseOverTimeAmount;
	[SerializeField] private float coolingSliderDecrease_OverheatIncreaseAmount;

	private bool overheatDecreasing = false;

	protected override void Start()
	{
		base.Start();	

		overheatSlider = Storage.StorageInstance.GetOverheatSlider();
		coolingSlider = Storage.StorageInstance.GetCoolingSlider();
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
			OverheatImpactOnCooling();

			if(overheatSlider.value >= maxOverheatSliderAmount)
			{
				StartCoroutine(OverheatSliderDecreaseOverTime());
			}
		}
	}

	IEnumerator OverheatSliderDecreaseOverTime() 
	{
		base.SetCanInteractStatus(false);
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

	protected virtual void OverheatImpactOnCooling()
	{
		if (coolingSlider.value > 0f)
		{
			coolingSlider.value -= coolingSliderDecrease_OverheatIncreaseAmount;
		}
	}

	public void SetOverheatSliderAmountIncrease(float increaseAmount)
	{
		overheatSliderIncreaseAmount = increaseAmount;
	}
	public void SetMaxOverheatValue(float maxValue)
	{
		maxOverheatSliderAmount = maxValue;
	}
	public void SetOverheatSliderDecreaseAmount(float decreaseAmount)
	{
		overheatSliderDecreaseOverTimeAmount = decreaseAmount;
	}
	public void SetOverheatAffectAmount(float affectAmount)
	{
		coolingSliderDecrease_OverheatIncreaseAmount = affectAmount;
	}
}
