using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Controller Settings")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _jumpHeight = 4f;

    [Header("Camera Settings")]
    [SerializeField] private float _minCameraXRotation = 5f;
    [SerializeField] private float _maxCameraXRotation = 25f;
    [SerializeField] private bool _isLookUpDownInverted = false;
    [SerializeField][Range(0.1f,5f)] private float _mouseSensitivity = 1f;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private bool _isGrounded;
    private float _horizontalAxis;
    private float _verticalAxis;
    private bool _wasJumpPressed = false;
    private Transform _cameraTransform;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        if (!_characterController)
            Debug.LogError(name + ": CharacterController component not found!");

        _cameraTransform = GetComponentInChildren<Camera>().transform;
        if (!_cameraTransform)
            Debug.LogError(name + ": Camera Transform not found in children!");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GatherMovementInput();
        CameraController();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void GatherMovementInput()
    {
        _horizontalAxis = Input.GetAxis(Constants.HORIZONTAL);
        _verticalAxis = Input.GetAxis(Constants.VERTICAL);

        _isGrounded = _characterController.isGrounded;

        if (Input.GetButtonDown(Constants.JUMP) && _isGrounded)
        {
            _wasJumpPressed = true;
        }
    }

    private void CameraController()
    {
        // Get input
        float mouseX = Input.GetAxis(Constants.MOUSE_X);
        float mouseY = Input.GetAxis(Constants.MOUSE_Y);

        // Look left and right
        Vector3 currentPlayerRotation = transform.localEulerAngles;
        currentPlayerRotation.y += mouseX * _mouseSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentPlayerRotation.y, Vector3.up);

        // Look up and down
        Vector3 currentCameraRotation = _cameraTransform.localEulerAngles;
        if (_isLookUpDownInverted)
        {
            currentCameraRotation.x = Mathf.Clamp(
                currentCameraRotation.x + mouseY,
                _minCameraXRotation,
                _maxCameraXRotation);
        }
        else
        {
            currentCameraRotation.x = Mathf.Clamp(
                currentCameraRotation.x - mouseY,
                _minCameraXRotation,
                _maxCameraXRotation);
        }
        _cameraTransform.localRotation = Quaternion.AngleAxis(
            currentCameraRotation.x,
            Vector3.right);
    }

    private void FixedUpdate()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        Vector3 direction = new Vector3(_horizontalAxis, 0, _verticalAxis);
        direction = transform.TransformDirection(direction);
        _characterController.Move(direction * Time.fixedDeltaTime * _speed);

        if (_wasJumpPressed)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * Constants.GRAVITY);
            _wasJumpPressed = false;
        }

        _velocity.y += Constants.GRAVITY * Time.fixedDeltaTime;

        _characterController.Move(_velocity * Time.fixedDeltaTime);
    }
}
