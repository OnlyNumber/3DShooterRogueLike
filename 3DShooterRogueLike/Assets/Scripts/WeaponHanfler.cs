using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class WeaponHanfler : MonoBehaviour
{
    [SerializeField]
    private StarterAssetsInputs _input;

    [SerializeField]
    private LayerMask _attackLayer;

    [SerializeField]
    private List<Weapon> _listWeapons;

    private Weapon _currentWeapon;


    void Update()
    {
        if (_input.attack)
        {
            Vector2 centerCamera = new Vector2(Screen.width/2, Screen.height / 2);

            Ray aimRay = Camera.main.ScreenPointToRay(centerCamera);

            Physics.Raycast(aimRay, out RaycastHit hitInfo, 100, _attackLayer);

            Debug.Log(hitInfo.collider.gameObject.name);

            if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<HitBox>().TakeDamage(5);
            }

            _input.attack = false;

        }
    }
}
