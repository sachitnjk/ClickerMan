using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonActions : MonoBehaviour
{
	public void ChangeToSceneWithIndex(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
