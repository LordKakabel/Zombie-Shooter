using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    
    private int _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damage = 1)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Dead();
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
