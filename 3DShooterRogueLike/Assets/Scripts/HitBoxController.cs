using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitBoxController : MonoBehaviour
{
    [SerializeField]
    private List<HitBox> _hitBoxes;

    private HitBox _lastHitBox;

    public Action OnAttackingWeakSpot;

    private void Start()
    {
        foreach (var item in GetComponentsInChildren<HitBox>())
        {
            _hitBoxes.Add(item);
        }
    }

    public void SetLastHitBox(HitBox hitBox)
    {
        if(_hitBoxes.Contains( hitBox))
        {
            _lastHitBox = hitBox;
        }
    }

    public HitBox GetLastHitBox()
    {
        return _lastHitBox;
    }

    public void IsWeakSpot(float modifier)
    {
        if(modifier > 1)
        {
            OnAttackingWeakSpot?.Invoke();
        }
    }

}
