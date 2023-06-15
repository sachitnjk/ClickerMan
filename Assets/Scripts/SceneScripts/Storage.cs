using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
	public static Storage StorageInstance;

	[SerializeField] private TextMeshProUGUI scoreTextBox;
	[SerializeField] private Slider overheatSlider;
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

	public TextMeshProUGUI GetScoreTextBox()
	{
		return scoreTextBox;
	}
}
