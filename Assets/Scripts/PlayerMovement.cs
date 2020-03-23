using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	#region Editor Variables
	[Header("Grounded Movement Settings")]
	[SerializeField]
	[Tooltip("The controller will move this number of units per second while grounded.")]
	private float _movementSpeed = 5.5f;

	[SerializeField]
	[Tooltip("When running, this speed replaces the controller's default movement speed.")]
	private float _sprintSpeed = 8.8f;

	[Header("Vertical Movement Settings")]
	[SerializeField]
	[Tooltip("This value is applied to the player's vertical speed when they jump.")]
	private float _jumpSpeed = 5.0f;

	[SerializeField]
	[Tooltip("Gravity is constantly applied to the controller manually.")]
	private float _gravity = 18.0f;
	#endregion

	#region Class Fields
	private CharacterController _controller;
	#endregion

	#region Properties
	/// <summary>
	/// Contains the controller's speed during the last frame.
	/// </summary>
	public Vector3 Speed { get; private set; }
	#endregion

	void Start ()
	{
		_controller = GetComponent<CharacterController>();
	}
	
	void Update ()
	{
		if (_controller.isGrounded)
		{
			Speed = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
			
			if (Speed.magnitude > 1)
			{
				Speed /= Speed.magnitude;
			}
			
			Speed *= (Input.GetButton("Run") ? _sprintSpeed : _movementSpeed);
			
			Speed = transform.TransformDirection(Speed);
			
			if (Input.GetButton("Jump"))
			{
				Speed += new Vector3(0, _jumpSpeed, 0);
			}
		}
		
		Speed += new Vector3(0, -_gravity * Time.deltaTime, 0);
		_controller.Move(Speed * Time.deltaTime);
	}
}
