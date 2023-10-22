using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Item
{
    [SerializeField]
    private int _amountOfCartriges;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<WeaponHandler>().GetAmmo(_amountOfCartriges);

        Destroy(gameObject);

    }

}
