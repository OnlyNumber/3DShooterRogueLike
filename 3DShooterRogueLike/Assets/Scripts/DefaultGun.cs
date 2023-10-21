using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Weapon
{
    [SerializeField]
    private bool _isOneTap;

    [SerializeField]
    private ParticleSystem _particleEffect;

    [SerializeField]
    private List<AudioClip> _gunSounds;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField, Range(0, 1f)]
    private float _volume;


    public override void Attack()
    {
        if(CurrentTime < TimeBetweenShoots || CurrentAmmo <= 0 )
        {
            return;
        }

        Vector2 centerCamera = new Vector2(Screen.width / 2, Screen.height / 2);

        Ray aimRay = Camera.main.ScreenPointToRay(centerCamera);

        Physics.Raycast(aimRay, out RaycastHit hitInfo, 100, AttackLayer);

        //Debug.Log(hitInfo.collider.gameObject.name);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Enemy"))
        {
            
            hitInfo.collider.GetComponent<HitBox>().TakeDamage(5);

        }

        if (_isOneTap)
        {
            Input.attack = false;
        }

        _particleEffect.Play();

        GetRandomSound();

        CurrentTime = 0;

        CurrentAmmo--;



    }
    private void GetRandomSound()
    {
        if (_gunSounds.Count > 0)
        {
            var index = Random.Range(0, _gunSounds.Count);
            AudioSource.PlayClipAtPoint(_gunSounds[index], transform.TransformPoint(_firePoint.position), _volume);
        }
    }
}
