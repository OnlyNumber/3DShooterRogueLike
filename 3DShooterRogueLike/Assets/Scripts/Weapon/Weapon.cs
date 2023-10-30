using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField]
    public RuntimeAnimatorController Animator { get; private set; }

    [SerializeField]
    protected LayerMask AttackLayer;

    [SerializeField]
    protected StarterAssetsInputs Input;

    [SerializeField]
    protected int CartrigeAmmo;

    [SerializeField]
    protected int LastAmmo;

    [SerializeField]
    private int CurrentAmmo;

    public Action OnChangeAmmo;

    protected int AmmoVariable
    {
        set
        {
            CurrentAmmo = value;
            
            OnChangeAmmo?.Invoke();
        }

        get
        {
            return CurrentAmmo;
        }
    }



    [SerializeField]
    protected float CurrentTime;

    [SerializeField]
    protected float TimeBetweenShoots;

    [SerializeField]
    protected float Damage;

    public void Initialize(StarterAssetsInputs input)
    {
        Input = input;
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
    }

    public abstract void Attack();

    public string GetAmmoString()
    {
        string str = CurrentAmmo + " / " + CartrigeAmmo + "  " + LastAmmo;

        return str;
    }

    public void Reload()
    {

        int needAmmo = CartrigeAmmo - AmmoVariable;

        if (needAmmo < LastAmmo)
        {
            LastAmmo -= needAmmo;
            AmmoVariable = CartrigeAmmo;
        }
        else
        {
            int saveNumber = LastAmmo;
            LastAmmo = 0;
            AmmoVariable += saveNumber;
        }


    }

    public abstract void ReloadSound();

    public void GetAmmo(int amountCartriges)
    {
        LastAmmo += CartrigeAmmo * amountCartriges;
    }

    public bool IsCanReload()
    {
        if(LastAmmo == 0 || AmmoVariable == CartrigeAmmo)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
