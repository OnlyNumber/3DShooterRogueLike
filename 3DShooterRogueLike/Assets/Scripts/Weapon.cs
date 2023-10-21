using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private StarterAssetsInputs _input;

    [SerializeField]
    private LayerMask _attackLayer;

    void Update()
    {
        if(_input.attack)
        {
            Vector2 centerCamera = new Vector2(Screen.width/2, Screen.height / 2);

            Ray aimRay = Camera.main.ScreenPointToRay(centerCamera);

            Physics.Raycast(aimRay, out RaycastHit hitInfo, 100, _attackLayer);

            if (hitInfo.collider.gameObject.CompareTag("Enemy") )
            {
                hitInfo.collider.GetComponent<HitBox>().TakeDamage(5);
            }
        }
    }
}
