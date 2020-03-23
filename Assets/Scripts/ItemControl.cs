using UnityEngine;

public class ItemControl : MonoBehaviour
{
	#region Editor Variables
	[Header("Required Components")]
	[SerializeField]
	[Tooltip("Attach the associated player's controller here to make object throwing / dropping relative to the player's movement.")]
	private PlayerMovement _movementData;

	[SerializeField]
	[Tooltip("All held objects will be made children of the following transform while carried.")]
	private Transform _heldObjectLocation;

	[Header("Interaction Settings")]
	[SerializeField]
	[Tooltip("The player can grab any object they look at within this distance.")]
	private float _maxGrabDistance = 3.0f;
	
	[SerializeField]
	[Tooltip("The player try to force-grab any object that they look at within this distance.")]
	private float _maxPullDistance = 10.0f;

	[SerializeField]
	[Tooltip("Defines how fast objects will move when thrown by the player.")]
	private float _throwStrength = 3.0f;
	#endregion

	#region Class Fields
	private GameObject _heldGameObject;
	private GameObject _parentGameObject;
	private GameObject _pulledGameObject;
	private bool _grabbedThisFrame = false;
	#endregion

	void Update ()
	{
		_grabbedThisFrame = false;

		bool discard = Input.GetButtonDown("Interact");
		bool attract = Input.GetButtonDown("Attract");
		bool pulling = Input.GetButton("Attract");
		bool stopPull = Input.GetButtonUp("Attract");
		
		if(attract && !_heldGameObject)
		{
			RaycastHit hit;
			LayerMask mask = LayerMask.GetMask("PickupObjects");

			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxGrabDistance, mask))
			{
				_heldGameObject = hit.transform.gameObject;
				_heldGameObject.transform.SetParent(_heldObjectLocation);
				_heldGameObject.transform.position = _heldObjectLocation.position;
				_heldGameObject.GetComponent<Rigidbody>().useGravity = false;
				_heldGameObject.GetComponent<Rigidbody>().freezeRotation = true;
				_grabbedThisFrame = true;
			}
			else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxPullDistance, mask))
			{
				_pulledGameObject = hit.transform.gameObject;
				_pulledGameObject.GetComponent<Rigidbody>().velocity = transform.forward * _throwStrength * -1.0f + transform.up * _throwStrength * 0.15f;
			}
		}

		if(pulling && !_heldGameObject && _pulledGameObject)
		{
			LayerMask mask = LayerMask.GetMask("PickupObjects");
			RaycastHit[] hits = Physics.SphereCastAll(transform.position, _maxGrabDistance, transform.forward, 0.0f, mask);

			foreach (RaycastHit hit in hits)
			{
				if(hit.transform.gameObject.Equals(_pulledGameObject))
				{
					_heldGameObject = _pulledGameObject;
					_heldGameObject.transform.SetParent(_heldObjectLocation);
					_heldGameObject.transform.position = _heldObjectLocation.position;
					_heldGameObject.GetComponent<Rigidbody>().useGravity = false;
					_heldGameObject.GetComponent<Rigidbody>().freezeRotation = true;
					_grabbedThisFrame = true;
					_pulledGameObject = null;
					break;
				}
			}
		}

		if(stopPull)
		{
			_pulledGameObject = null;
		}

		if (_heldGameObject)
		{
			_heldGameObject.transform.position = _heldObjectLocation.position;

			if ((discard || attract) && !_grabbedThisFrame)
			{
				_heldGameObject.GetComponent<Rigidbody>().useGravity = true;
				_heldGameObject.GetComponent<Rigidbody>().freezeRotation = false;
				_heldGameObject.GetComponent<Rigidbody>().velocity = transform.forward * (discard ? _throwStrength : 0.5f) + _movementData.Speed;
				_heldGameObject.transform.parent = _parentGameObject ? _parentGameObject.transform : null;
				_heldGameObject = null;
				_parentGameObject = null;
			}
		}
	}
}
