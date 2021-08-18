using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _bloodPrefab = null;
    [SerializeField] private float _bloodDuration = 1.1f;

    private int _allLayers;

    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        if (!_camera)
            Debug.LogError(name + ": Main Camera not found!");

        _allLayers = 1 << LayerMask.NameToLayer("Default");
    }

    // Update is called once per frame
    void Update()
    {
        // Left click
        if (Input.GetButtonDown(Constants.FIRE_1))
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _allLayers, QueryTriggerInteraction.Ignore))
            {
                Health health = hit.collider.GetComponent<Health>();
                if (health != null)
                {
                    GameObject blood = Instantiate(_bloodPrefab, hit.point, Quaternion.LookRotation(hit.normal), hit.collider.transform);
                    Destroy(blood, _bloodDuration);
                    health.Damage();
                }
            }
        }
    }
}
