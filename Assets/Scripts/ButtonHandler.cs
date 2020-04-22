﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
	public void SwitchScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
