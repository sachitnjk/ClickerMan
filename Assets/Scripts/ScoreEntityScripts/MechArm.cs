using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MechArm : MonoBehaviour
{
	private float existingScoreAmount;
	private float currentScoreAmount;
	private float timeSinceLastIncrease;

	[SerializeField] private float generatedScoreAmount;
	[SerializeField] private float scoreIncreaseRate;

	private TextMeshProUGUI scoreTextBox;

	private void Start()
	{
		scoreTextBox = Storage.StorageInstance.GetScoreTextBox();
	}
	private void Update()
	{
		GenerateScore();
	}

	private void GenerateScore()
	{
		float.TryParse(scoreTextBox.text, out existingScoreAmount);

		timeSinceLastIncrease += Time.deltaTime;
		if(timeSinceLastIncrease >= scoreIncreaseRate)
		{
			currentScoreAmount = existingScoreAmount + generatedScoreAmount;
			scoreTextBox.text = currentScoreAmount.ToString();

			timeSinceLastIncrease = 0f;
		}
	}
}
