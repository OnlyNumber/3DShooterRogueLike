using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private StarterAssetsInputs _input;

    [SerializeField]
    private LayerMask _attackLayer;

    [SerializeField]
    private List<Weapon> _listWeapons;

    [SerializeField]
    private Weapon _currentWeapon;

    [SerializeField]
    private TMP_Text _weaponAmmo;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private ThirdPersonShooterController _tpscontroller;

    private void Start()
    {
        _weaponAmmo.text = _currentWeapon.GetAmmoString();
    }

    void Update()
    {
        if (_input.attack && !_animator.GetCurrentAnimatorStateInfo(2).IsName("Reload"))
        {
            _currentWeapon.Attack();

            _weaponAmmo.text = _currentWeapon.GetAmmoString();

            _tpscontroller.StartAim();

        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            _animator.SetTrigger("Reload");
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(2);
        }

    }

    public void EquipWeapon(int weaponNumber)
    {
        _currentWeapon.gameObject.SetActive(false);

        _currentWeapon = _listWeapons[weaponNumber];

        _animator.runtimeAnimatorController = _currentWeapon.Animator;

        _currentWeapon.gameObject.SetActive(true);

        _weaponAmmo.text = _currentWeapon.GetAmmoString();

    }

    private void Reload()
    {
        _currentWeapon.Reload();
        _weaponAmmo.text = _currentWeapon.GetAmmoString();
    }

    private void ReloadSound()
    {
        _currentWeapon.ReloadSound();
    }

}
