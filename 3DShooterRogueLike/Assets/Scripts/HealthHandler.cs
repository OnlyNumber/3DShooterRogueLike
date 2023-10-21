using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    [field: SerializeField]
    public float MaxHealth { get; private set; }

    private float _health;

    public Action OnHealthChange;

    public Action OnDeath;



    public float Health
    {
        get
        {
            return _health;
        }

        private set
        {
            _health = value;

            OnHealthChange?.Invoke();

            Debug.Log(_health);

            if(_health <= 0)
            {
                OnDeath?.Invoke();

                Debug.Log("Dead");
            }

        }
    }

    private void Start()
    {
        _health = MaxHealth;
        OnHealthChange?.Invoke();
    }

    public void TakeDamage(float damage)
    {

        Health -= damage;

    }
}
