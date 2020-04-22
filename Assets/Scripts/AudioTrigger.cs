using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
	public AudioSource Audio;

	private bool _hasPlayed = false;

	private void OnTriggerEnter(Collider other)
	{
		PlayerMovement p = other.GetComponent<PlayerMovement>();

		if (p != null && !_hasPlayed)
		{
			Audio.Play();
			_hasPlayed = true;
		}
	}
}
