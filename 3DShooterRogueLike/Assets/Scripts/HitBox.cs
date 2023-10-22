using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    private HealthHandler _healthHandler;

    [SerializeField]
    private float _modifier;

    [SerializeField]
    private HitBoxController _hitBoxController;

    public Action OnHit;

    private void Start()
    {
        _hitBoxController = GetComponentInParent<HitBoxController>();
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log(damage * _modifier);

        _healthHandler.TakeDamage(damage * _modifier);

        _hitBoxController.IsWeakSpot(_modifier);

    }
}
