using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aid : MonoBehaviour
{
    [SerializeField]
    private float _healAmount;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<HealthHandler>().TakeDamage(-_healAmount);

        Destroy(gameObject);
    }
}
