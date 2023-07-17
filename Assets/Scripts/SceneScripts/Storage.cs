using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

	[SerializeField] private CoolingController coolingController;
	[SerializeField] private OverheatController overheatController;

	private float currentScore;
	private float updatedScore;

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

	public float GetCurrentScore()
	{
		float.TryParse(scoreTextBox.text, out currentScore);
		return currentScore;
	}
	public void SetCurrentScore(float UpgradeCost)
	{
		float.TryParse(scoreTextBox.text, out currentScore);
		updatedScore = currentScore - UpgradeCost;
		scoreTextBox.text = updatedScore.ToString();
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
