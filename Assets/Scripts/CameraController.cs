using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _minCameraXRotation = 0f;
    [SerializeField] private float _maxCameraXRotation = 15f;

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
        Vector3 currentPlayerRotation = _playerTransform.rotation.eulerAngles;
        currentPlayerRotation.y += mouseX;
        Quaternion newPlayerRotation = Quaternion.Euler(currentPlayerRotation);
        _playerTransform.rotation = newPlayerRotation;

        // Look up and down
        Vector3 currentCameraRotation = transform.rotation.eulerAngles;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x + mouseY, _minCameraXRotation, _maxCameraXRotation);
        Quaternion newCameraRotation = Quaternion.Euler(currentCameraRotation);
        transform.rotation = newCameraRotation;
    }
}
