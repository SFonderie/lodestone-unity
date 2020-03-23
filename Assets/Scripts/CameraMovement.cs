using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	#region Editor Variables
	[Header("Required Components")]
	[SerializeField]
	[Tooltip("Horizontal camera movement will rotate this game object.")]
	private GameObject _player;

	[Header("Mouse Settings")]
	[SerializeField]
	[Tooltip("Mouse sensetivity affects how much mouse movement adjusts camera movement.")]
	private float _mouseSensitivity = 2.0f;

	[SerializeField]
	[Tooltip("Mouse smoothing smooths camera movement to improve play feel.")]
	private float _mouseSmoothing = 2.0f;

	[SerializeField]
	[Tooltip("The camera cannot rotate past this angle in either vertical direction.")]
	private float _maximumVertical = 90.0f;
	#endregion

	#region Class Fields
	private Vector2 _lookVector;
	private Vector2 _smoothVector;
	private Vector2 _inputVector;
	private Vector2 _scaleVector;
	#endregion

	void Start ()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update ()
	{
		_inputVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		_scaleVector = new Vector2(_mouseSensitivity * _mouseSmoothing, _mouseSensitivity * _mouseSmoothing);
		_inputVector = Vector2.Scale(_inputVector, _scaleVector);

		_smoothVector.x = Mathf.Lerp(_smoothVector.x, _inputVector.x, 1.0f / _mouseSmoothing);
		_smoothVector.y = Mathf.Lerp(_smoothVector.y, _inputVector.y, 1.0f / _mouseSmoothing);
		_lookVector += _smoothVector;

		_lookVector.y = Mathf.Clamp(_lookVector.y, -_maximumVertical, _maximumVertical);
		transform.localRotation = Quaternion.AngleAxis(-_lookVector.y, Vector3.right);
		_player.transform.localRotation = Quaternion.AngleAxis(_lookVector.x, _player.transform.up);
	}
}
