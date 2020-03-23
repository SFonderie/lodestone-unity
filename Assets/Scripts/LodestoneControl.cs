using UnityEngine;

public class LodestoneControl : MonoBehaviour
{
	[SerializeField]
	[Tooltip("What kind of Barrier this Lodestone can bypass.")]
	private LodestoneColor _color = LodestoneColor.Red;

	[SerializeField]
	[Tooltip("How large this Lodestone's aura is.")]
	private float _range = 3.0f;
	
	void Update ()
	{
		LayerMask mask = LayerMask.GetMask("Barrier");
		RaycastHit[] hits = Physics.SphereCastAll(transform.position, _range, transform.forward, 0.0f, mask);

		foreach (RaycastHit hit in hits)
		{
			BarrierControl barrier = hit.transform.GetComponent<BarrierControl>();
			
			if (barrier)
			{
				barrier.AddKey(_color);
			}
		}
	}
}
