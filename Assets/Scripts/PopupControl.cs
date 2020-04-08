using UnityEngine;
using UnityEngine.UI;

public class PopupControl : MonoBehaviour
{
	public string PopupText = "";

	void OnTriggerEnter(Collider other)
	{
		PlayerMovement p = other.GetComponent<PlayerMovement>();

		if(p != null)
		{
			Debug.Log("TEST 3");
			p.ShowTutorial(PopupText);
		}
	}
}
