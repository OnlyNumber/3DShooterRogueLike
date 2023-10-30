using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : Weapon
{

    [SerializeField]
    private ParticleSystem _particleEffect;

    [SerializeField]
    private List<AudioClip> _gunSounds;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField, Range(0, 1f)]
    private float _volume;

    [SerializeField]
    private GameObject _projectile;

    [SerializeField]
    private AudioClip _reloadSound;

    public override void Attack()
    {
        if (CurrentTime < TimeBetweenShoots || AmmoVariable <= 0)
        {
            return;
        }

        Vector2 centerCamera = new Vector2(Screen.width / 2, Screen.height / 2);

        Ray aimRay = Camera.main.ScreenPointToRay(centerCamera);

        RaycastHit[] hitInfoList = Physics.RaycastAll(aimRay, 100, AttackLayer);

        List<GameObject> hitedObjects = new List<GameObject>();

        foreach (var hitInfo in hitInfoList)
        {
            if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                if (hitInfo.collider.TryGetComponent<HitBox>(out HitBox hitBox) && !hitedObjects.Contains(hitInfo.collider.transform.root.gameObject))
                {
                    hitedObjects.Add(hitInfo.collider.transform.root.gameObject);

                    hitBox.TakeDamage(Damage);
                }
            }
        }
        hitedObjects.Clear();

        Instantiate(_projectile, _firePoint.position, _firePoint.rotation);

        _particleEffect.Play();

        GetRandomSound();

        CurrentTime = 0;

        AmmoVariable--;
    }

    private void GetRandomSound()
    {
        if (_gunSounds.Count > 0)
        {
            var index = Random.Range(0, _gunSounds.Count);
            AudioSource.PlayClipAtPoint(_gunSounds[index], transform.TransformPoint(_firePoint.position), _volume);
        }
    }

    

    public override void ReloadSound()
    {
        AudioSource.PlayClipAtPoint(_reloadSound, transform.TransformPoint(_firePoint.position), _volume);
    }
}
