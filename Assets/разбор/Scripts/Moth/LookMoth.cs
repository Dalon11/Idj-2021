using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class LookMoth : MonoBehaviour
{
    [SerializeField] private float _direction;
    [SerializeField] private CommonFlipper _commonFlipper;
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private float _transitionDuration = 5f;
    [SerializeField] private float _delayTime = 1f;
    [SerializeField] private bool _isRightLooking;
    [SerializeField] private Transform[] _lookPoints;
    [SerializeField] private GameObject _lookTarget;
    public bool _canlook;
    [SerializeField] private UnityEvent _event;
    

    public void Start()
    {
        _isRightLooking = true;
        _canlook = true;
        StartCoroutine(HorizontalMoveCoroutine());
        StartCoroutine(LookArround());
    }

    private void MoveToPoint(Transform point)
    {
        _commonFlipper.SetFlip(_direction);
        this.transform.DOMove(point.position, _transitionDuration);
    }

    private IEnumerator HorizontalMoveCoroutine()
    {
        var pointIndex = 0;

        while (true)
        {
            var point = _movePoints[pointIndex];

            pointIndex++;
            if (pointIndex >= _movePoints.Length)
            {
                pointIndex = 0;
            }

            if (pointIndex == 1)
            {
                _direction = -1f;
                _isRightLooking = false;
                Debug.Log(_isRightLooking);
            }
            else
            {
                _direction = 1f;
                _isRightLooking = true;
                Debug.Log((_isRightLooking));
            }
            

            MoveToPoint(point);

            yield return new WaitForSeconds(_transitionDuration);
            yield return new WaitForSeconds(_delayTime);
        }
    }

    private IEnumerator LookArround()
    {
        while (true)
        {
            if (_canlook)
            {
                if (_isRightLooking)
                {
                    if (_lookTarget.transform.position.x < _lookPoints[1].position.x &&
                        _lookTarget.transform.position.x > transform.position.x)
                    {
                        _event.Invoke();
                    }
                }
                else
                {
                    if (_lookTarget.transform.position.x > _lookPoints[0].position.x &&
                        _lookTarget.transform.position.x < transform.position.x)
                    {
                        _event.Invoke();
                    }
                }
            }

            yield return null;
        }
    }

    public void HasBeenFounded()
    {
        Debug.Log("Вас обнаружили");
    }
}
