using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Chase, Attack }

    [SerializeField] float _speed = 2f;
    [SerializeField] float _delayBetweenAttacks = 3f;
    [SerializeField] private EnemyState _state = EnemyState.Chase;

    private CharacterController _characterController;
    private Transform _player;
    private Health _playerHealth;
    private Vector3 _velocity;
    private float _nextAttack = -1f;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        if (!_characterController)
            Debug.LogError(name + ": CharacterController component not found!");
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).transform;
        if (!_player)
            Debug.LogError(name + ": Transform tagged Player not found!");

        _playerHealth = _player.GetComponent<Health>();
        if (!_playerHealth)
            Debug.LogError(name + ": Health component on Player not found!");
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                Movement();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            default:
                break;
        }
    }

    private void Attack()
    {
        Debug.Log("Attack called.");
        if (Time.time > _nextAttack)
        {
            if (_playerHealth != null)
            {
                Debug.Log("Calling damage.");
                _playerHealth.Damage();
            }
            _nextAttack = Time.time + _delayBetweenAttacks;
        }
    }

    private void Movement()
    {
        if (_characterController.isGrounded)
        {
            Vector3 direction = (_player.position - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            _velocity = direction * _speed;
        }

        _velocity.y += Constants.GRAVITY * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    public void StartAttack()
    {
        _state = EnemyState.Attack;
    }

    public void StopAttack()
    {
        _state = EnemyState.Chase;
    }
}
