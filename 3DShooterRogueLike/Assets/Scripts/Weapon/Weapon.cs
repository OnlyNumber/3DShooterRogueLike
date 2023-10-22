using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEditor.Animations;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField]
    public AnimatorController Animator { get; private set; }

    [SerializeField]
    protected LayerMask AttackLayer;

    [SerializeField]
    protected StarterAssetsInputs Input;

    [SerializeField]
    protected int CartrigeAmmo;

    [SerializeField]
    protected int LastAmmo;

    [SerializeField]
    protected int CurrentAmmo;

    [SerializeField]
    protected float CurrentTime;

    [SerializeField]
    protected float TimeBetweenShoots;

    [SerializeField]
    protected float Damage;


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

    public abstract void Reload();

    public abstract void ReloadSound();


}
