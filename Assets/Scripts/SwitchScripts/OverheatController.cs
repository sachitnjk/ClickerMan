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
		if(overheatSlider.value != maxOverheatSliderAmount) 
		{
			overheatSlider.value += overheatSliderIncreaseAmount;
		}
	}
}
