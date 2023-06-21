using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolingController : SwitchCommonBehaviour
{
	private Slider coolingSlider;

	[Header("Cooling Slider Attributes")]
	[SerializeField] private float coolingSliderIncreaseAmount;
	[SerializeField] private float maxCoolingSliderAmount;
	protected virtual void Start()
	{
		base.Start();

		coolingSlider = Storage.StorageInstance.GetCoolingSlider();
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
		}
	}
}
