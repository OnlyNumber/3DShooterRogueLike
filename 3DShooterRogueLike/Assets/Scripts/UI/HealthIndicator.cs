using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField]
    private HealthHandler _healthHandler;

    [SerializeField]
    Image _healthIndicator;

    [SerializeField]
    private TMP_Text _helthText;

    void Start()
    {
        _healthHandler.OnHealthChange += ShowHealth;
        ShowHealth();
    }


    public void ShowHealth()
    {
        _healthIndicator.fillAmount = _healthHandler.Health / _healthHandler.MaxHealth; // = Health + "/ " + _maxHealth;

        _helthText.text = _healthHandler.Health + " / " + _healthHandler.MaxHealth;
    }

}
