using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    [SerializeField]
    private Weapon _weapon;

    [SerializeField]
    private BoxCollider _collider;

    public override void ItemActivation(GameObject player)
    {
        WeaponHandler wh = player.GetComponent<WeaponHandler>();

        wh.TakeNewWeapon(_weapon);

        _collider.enabled = false;

    }

    public void DropWeapon(Vector3 position)
    {
        _collider.enabled = true;

        transform.SetParent(null);

        transform.position = position;

        transform.rotation = Quaternion.identity;

    }

}
