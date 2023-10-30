using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Item
{
    [SerializeField]
    private int _amountOfCartriges;

    public override void ItemActivation(GameObject player)
    {
        player.GetComponent<WeaponHandler>().GetAmmo(_amountOfCartriges);

        Destroy(gameObject);
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<WeaponHandler>().GetAmmo(_amountOfCartriges);

        Destroy(gameObject);

    }
    */
}
