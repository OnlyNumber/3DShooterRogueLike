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


    void Update()
    {
        if (_input.attack)
        {
            _currentWeapon.Attack();

            _weaponAmmo.text = _currentWeapon.GetAmmoString();

            /*Vector2 centerCamera = new Vector2(Screen.width/2, Screen.height / 2);

            Ray aimRay = Camera.main.ScreenPointToRay(centerCamera);

            Physics.Raycast(aimRay, out RaycastHit hitInfo, 100, _attackLayer);

            if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<HitBox>().TakeDamage(5);
            }

            _input.attack = false;
            */
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            _animator.SetTrigger("Reload");
        }



    }

    public void EquipWeapon()
    {

    }

}
