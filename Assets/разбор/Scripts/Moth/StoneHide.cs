using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHide : MonoBehaviour
{
    [SerializeField] private LookMoth _lookMoth;
    [SerializeField] private Collider2D _targetCollider2D;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == _targetCollider2D)
        {
            _lookMoth._canlook = true;
        }
        else
        {
            _lookMoth._canlook = false;
        }
    }
}
