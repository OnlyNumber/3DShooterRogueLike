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
    private Weapon[] _listWeapons;

    [SerializeField]
    private Weapon _currentWeapon;

    [SerializeField]
    private TMP_Text _weaponAmmo;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private ThirdPersonShooterController _tpscontroller;

    [SerializeField]
    private float _weightAimForShoot;

    [SerializeField]
    private HealthHandler _healthHandler;

    private void Start()
    {

        _healthHandler.OnDeath += Death;

        _weaponAmmo.text = _currentWeapon.GetAmmoString();

        foreach (var weapon in _listWeapons)
        {
            if(weapon != null)
            weapon.OnChangeAmmo += ChangeAmmoText;
        }

        //Debug.Log(FindWeaponIndex(null));

    }

    void Update()
    {
        if (_input.attack && !_animator.GetCurrentAnimatorStateInfo(2).IsName("Reload"))
        {
            if(_animator.GetLayerWeight(1) > 0.9f || _animator.GetLayerWeight(1) > _weightAimForShoot && _animator.GetLayerWeight(2) > 0.9f)
            _currentWeapon.Attack();

            _tpscontroller.StartAim();

        }

        if(_input.reload && _currentWeapon.IsCanReload())
        {
            _animator.SetTrigger("Reload");

            _input.reload = false;

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

        ChangeAmmoText();

    }

    private void Reload()
    {
        _currentWeapon.Reload();
        _animator.ResetTrigger("Reload");
    }

    private void ReloadSound()
    {
        _currentWeapon.ReloadSound();
    }

    public void GetAmmo(int cartrigesAmount)
    {
        _currentWeapon.GetAmmo(cartrigesAmount);
        
        _weaponAmmo.text = _currentWeapon.GetAmmoString();
    }

    private void ChangeAmmoText()
    {
        _weaponAmmo.text = _currentWeapon.GetAmmoString();
    }

    private void Death()
    {
        this.enabled = false;

        _animator.SetTrigger("Death");
    }

    public void TakeNewWeapon(Weapon newWeapon)
    {
        int currentWeaponIndex = FindWeaponIndex(_currentWeapon);

        newWeapon.transform.SetParent(_currentWeapon.transform.parent);

        newWeapon.Initialize(_input);

        newWeapon.transform.localPosition = Vector3.zero;

        newWeapon.transform.localRotation = Quaternion.identity;

        _listWeapons[currentWeaponIndex] = newWeapon;

        newWeapon.OnChangeAmmo += ChangeAmmoText;

        _currentWeapon.OnChangeAmmo -= ChangeAmmoText;

        _currentWeapon.GetComponent<WeaponItem>().DropWeapon(transform.position);

        _currentWeapon = newWeapon;


    }

    public int FindWeaponIndex(Weapon searchingWeapon)
    {
        int index = 0;

        foreach (var weapon in _listWeapons)
        {
            if(searchingWeapon == weapon)
            {
                return index;
            }

            index++;
        }
        index = -1;

        return index;
    }
}