using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
	public static Storage StorageInstance;

	[Header("Scene UI References")]
	[SerializeField] private TextMeshProUGUI scoreTextBox;
	[SerializeField] private Slider overheatSlider;
	[SerializeField] private Slider coolingSlider;

	private void Awake()
	{
		if (StorageInstance == null)
		{
			StorageInstance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	public Slider GetOverheatSlider()
	{
		return overheatSlider;
	}
	public Slider GetCoolingSlider() 
	{
		return coolingSlider;
	}

	public TextMeshProUGUI GetScoreTextBox()
	{
		return scoreTextBox;
	}
}
