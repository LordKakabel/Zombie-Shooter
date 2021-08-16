using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _jumpHeight = 4f;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private bool _isGrounded;
    private float _horizontalAxis;
    private float _verticalAxis;
    private bool _wasJumpPressed = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        if (!_characterController)
            Debug.LogError(name + ": CharacterController component not found!");
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalAxis = Input.GetAxis(Constants.HORIZONTAL);
        _verticalAxis = Input.GetAxis(Constants.VERTICAL);

        _isGrounded = _characterController.isGrounded;

        if (Input.GetButtonDown(Constants.JUMP) && _isGrounded)
        {
            _wasJumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        Vector3 direction = new Vector3(_horizontalAxis, 0, _verticalAxis);
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
