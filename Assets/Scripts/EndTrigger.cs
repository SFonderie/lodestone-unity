using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		PlayerMovement p = other.GetComponent<PlayerMovement>();

		if (p != null)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			SceneManager.LoadScene("EndScene");
		}
	}
}
