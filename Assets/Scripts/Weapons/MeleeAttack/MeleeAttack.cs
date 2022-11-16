using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private GameObject _attackArea = default;

    private bool _isAttacking = false;

    private float _timeToAttack = 0.05f;
    private float _timer = 0f;

    void Start()
    {
        _attackArea = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (_isAttacking)
        {
            _timer += Time.deltaTime;
            if (_timer >= _timeToAttack)
            {
                _isAttacking = false;
                _timer = 0f;
                _attackArea.SetActive(_isAttacking);
            }
        }
    }

    public void Attack()
    {
        _isAttacking = true;
        _attackArea.SetActive(_isAttacking);
    }
}
