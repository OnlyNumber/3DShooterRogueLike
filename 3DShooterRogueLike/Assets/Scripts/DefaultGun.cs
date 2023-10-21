using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Weapon
{
    [SerializeField]
    private bool _isOneTap;

    [SerializeField]
    private ParticleSystem _particleEffect;

    public override void Attack()
    {
        if(CurrentTime < TimeBetweenShoots)
        {
            return;
        }

        Vector2 centerCamera = new Vector2(Screen.width / 2, Screen.height / 2);

        Ray aimRay = Camera.main.ScreenPointToRay(centerCamera);

        Physics.Raycast(aimRay, out RaycastHit hitInfo, 100, AttackLayer);

        Debug.Log(hitInfo.collider.gameObject.name);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Enemy"))
        {
            
            hitInfo.collider.GetComponent<HitBox>().TakeDamage(5);

        }

        if (_isOneTap)
        {
            Input.attack = false;
        }

        CurrentTime = 0;

        CurrentAmmo--;

    }

}
