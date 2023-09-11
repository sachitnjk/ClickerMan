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
	[SerializeField] private float minAnimChangeTime;
	[SerializeField] private float maxAnimChangeTime;

	[SerializeField] Animator mechArmAnimator;

	private TextMeshProUGUI scoreTextBox;
	private float animChangeTime;
	private float timeSinceLastAnimChange;
	private void Start()
	{
		scoreTextBox = Storage.StorageInstance.GetScoreTextBox();
		AnimChangeTimeCalc(minAnimChangeTime, maxAnimChangeTime);
	}
	private void Update()
	{
		generatedScoreAmount = Storage.StorageInstance.mechArmGeneratedScore;
		scoreIncreaseRate = Storage.StorageInstance.scoreIncreaseRate;

		GenerateScore();

		timeSinceLastAnimChange += Time.deltaTime;
		if(timeSinceLastAnimChange >= animChangeTime)
		{
			if (!mechArmAnimator.GetBool("isIdle"))
			{
				mechArmAnimator.SetBool("isIdle", true);
			}
			else
			{
				mechArmAnimator.SetBool("isIdle", false);
			}
			timeSinceLastAnimChange = 0f;
		}
	}

	private void AnimChangeTimeCalc(float minAnimChangeTime, float maxAnimChangeTime)
	{
		animChangeTime = Random.Range(minAnimChangeTime, maxAnimChangeTime);
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
