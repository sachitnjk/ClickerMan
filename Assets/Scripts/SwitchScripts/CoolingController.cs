using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolingController : SwitchCommonBehaviour
{
	private Slider coolingSlider;
	private Slider overheatSlider;

	[Header("Cooling Slider Attributes")]
	[SerializeField] private float coolingSliderIncreaseAmount;
	[SerializeField] private float maxCoolingSliderAmount;
	[SerializeField] private float coolingDecreaseTimeDelay;
	[SerializeField] private float coolingSliderDecreaseOverTimeAmount;
	[SerializeField] private float overheatSliderDecrease_CoolingIncreaseAmount;

	private bool coolingSliderDecreasing = false;

	protected override void Start()
	{
		base.Start();

		coolingSlider = Storage.StorageInstance.GetCoolingSlider();
		overheatSlider = Storage.StorageInstance.GetOverheatSlider();
		coolingSlider.maxValue = maxCoolingSliderAmount;
		coolingSlider.value = 0f;
	}

	protected override void ClickCounterIncrease()
	{
		base.ClickCounterIncrease();
		CoolingSliderIncrease();
	}

	protected virtual void CoolingSliderIncrease()
	{
		if(coolingSlider.value != maxCoolingSliderAmount)
		{
			coolingSlider.value += coolingSliderIncreaseAmount;
			CoolingImpactOnOverheat();

			if(coolingSlider.value >= maxCoolingSliderAmount)
			{
				StartCoroutine(CoolingSliderDecreaseOvertime());
			}
		}
		if (coolingSliderDecreasing)
		{
			base.ClickAmount_SliderDecrease();
		}
		else
		{
			base.ClickAmount_Normal();
		}
	}

	IEnumerator CoolingSliderDecreaseOvertime()
	{
		coolingSliderDecreasing = true;

		yield return new WaitForSeconds(coolingDecreaseTimeDelay);

		while (coolingSlider.value > 0f)
		{
			coolingSlider.value -= coolingSliderDecreaseOverTimeAmount * Time.deltaTime;

			yield return null;
		}

		coolingSliderDecreasing = false;
	}

	protected virtual void CoolingImpactOnOverheat()
	{
		if(overheatSlider.value > 0f)
		{
			overheatSlider.value -= overheatSliderDecrease_CoolingIncreaseAmount;
		}
	}
}
