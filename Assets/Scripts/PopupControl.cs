using UnityEngine;
using UnityEngine.UI;

public class PopupControl : MonoBehaviour
{
	public string PopupText = "";

	private bool _triggeredBefore = false;

	void OnTriggerEnter(Collider other)
	{
		PlayerMovement p = other.GetComponent<PlayerMovement>();

		if(p != null && !_triggeredBefore)
		{
			p.ShowTutorial(PopupText);
			_triggeredBefore = true;
		}
	}
}
