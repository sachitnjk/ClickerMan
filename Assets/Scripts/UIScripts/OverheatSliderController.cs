using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatSliderController : MonoBehaviour
{
	private Slider overheatSlider;

	[SerializeField] private float decrementAmount;
	[SerializeField] private float decrementTimeDelay;

	private bool isDecrementing = false;

	private void Start()
	{
		overheatSlider = Storage.StorageInstance.GetOverheatSlider();
	}

	private void Update()
	{
		SliderCheck();
	}

	void SliderCheck()
	{
		if(overheatSlider.value >= overheatSlider.maxValue && !isDecrementing)
		{
			StartCoroutine(DecrementOverheatSliderValue());
		}
	}

	IEnumerator DecrementOverheatSliderValue()
	{
		isDecrementing = true;

		yield return new WaitForSeconds(decrementTimeDelay);

		while (overheatSlider.value > 0f)
		{
			overheatSlider.value -= decrementAmount * Time.deltaTime;

			yield return null;
		}

		isDecrementing=false;
	}
}
