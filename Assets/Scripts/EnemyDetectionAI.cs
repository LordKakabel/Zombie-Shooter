using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionAI : MonoBehaviour
{
    private EnemyAI _enemyAI;

    private void Awake()
    {
        _enemyAI = GetComponentInParent<EnemyAI>();
        if (!_enemyAI)
            Debug.LogError(name + ": EnemyAI component not found in parent!");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER))
            _enemyAI.StartAttack();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER))
            _enemyAI.StopAttack();
    }
}
