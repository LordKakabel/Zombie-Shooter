using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        if (!_camera)
            Debug.LogError(name + ": Main Camera not found!");
    }

    // Update is called once per frame
    void Update()
    {
        // Left click
        if (Input.GetButtonDown(Constants.FIRE_1))
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.transform.name);
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Damage();
                }
            }
        }
    }
}
