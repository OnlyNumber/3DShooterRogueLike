using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aid : Item
{
    [SerializeField]
    private float _healAmount;

    public override void ItemActivation(GameObject player)
    {
        player.GetComponent<HealthHandler>().TakeDamage(-_healAmount);

        Destroy(gameObject);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<HealthHandler>().TakeDamage(-_healAmount);

        Destroy(gameObject);
    }*/
}
