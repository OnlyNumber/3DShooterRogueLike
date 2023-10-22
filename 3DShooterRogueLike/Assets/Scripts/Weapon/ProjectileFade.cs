using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFade : MonoBehaviour
{
    private float _currentTime;

    [SerializeField]
    private float _timeToDestroy;

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime >= _timeToDestroy)
        {
            Destroy(gameObject);
        }

    }



}

