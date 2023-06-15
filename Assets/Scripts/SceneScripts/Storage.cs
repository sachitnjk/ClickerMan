using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Storage : MonoBehaviour
{
	public static Storage StorageInstance;

	[SerializeField] private TextMeshProUGUI scoreTextBox;
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

	public TextMeshProUGUI GetScoreTextBox()
	{
		return scoreTextBox;
	}
}
