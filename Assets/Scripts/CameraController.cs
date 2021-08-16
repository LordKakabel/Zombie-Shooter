using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _minCameraXRotation = 0f;
    [SerializeField] private float _maxCameraXRotation = 15f;
    [SerializeField] private bool _isLookUpDownInverted = false;

    private Transform _playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindWithTag(Constants.TAG_PLAYER).transform;
        if (!_playerTransform)
            Debug.LogError(name + ": Transform on GameObeject tagged Player not found!");
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        float mouseX = Input.GetAxis(Constants.MOUSE_X);
        float mouseY = Input.GetAxis(Constants.MOUSE_Y);

        // Look left and right
        Vector3 currentPlayerRotation = _playerTransform.localEulerAngles;
        currentPlayerRotation.y += mouseX;
        _playerTransform.rotation = Quaternion.AngleAxis(currentPlayerRotation.y, Vector3.up);

        // Look up and down
        Vector3 currentCameraRotation = transform.localEulerAngles;
        if (_isLookUpDownInverted)
        {
            currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x + mouseY, _minCameraXRotation, _maxCameraXRotation);
        }
        else
        {
            currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x - mouseY, _minCameraXRotation, _maxCameraXRotation);
        }
        transform.rotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }
}
