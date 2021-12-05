using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    public MovementType _movementType;
    [SerializeField] private float _transitionDuration = 2f;
    [SerializeField] private float _delayTime = 3f;
    [SerializeField] private Transform[] _verticalWayPoints;
    [SerializeField] private Transform[] _horizontalWayPoints;
    public void Start()
    {
        switch (_movementType)
        {
            case MovementType.Horizontal:
                StartCoroutine(HorizontalMoveCoroutine());
                break;
            case MovementType.Vertical:
                StartCoroutine(VerticalMoveCoroutine());
                break;
            case MovementType.Static:
               
                break;
        }
    }

    private void MoveToPoint(Transform point)
    {
        this.transform.DOMove(point.position, _transitionDuration);
    }
    
    private IEnumerator VerticalMoveCoroutine()
    {
        var pointIndex = 0;

        while (true)
        {
            var point = _verticalWayPoints[pointIndex];

            pointIndex++;
            if (pointIndex >= _verticalWayPoints.Length)
                pointIndex = 0;

            MoveToPoint(point);

            yield return new WaitForSeconds(_transitionDuration);
            yield return new WaitForSeconds(_delayTime);
        }
    }
    private IEnumerator HorizontalMoveCoroutine()
    {
        var pointIndex = 0;

        while (true)
        {
            var point = _horizontalWayPoints[pointIndex];

            pointIndex++;
            if (pointIndex >= _horizontalWayPoints.Length)
                pointIndex = 0;

            MoveToPoint(point);

            yield return new WaitForSeconds(_transitionDuration);
            yield return new WaitForSeconds(_delayTime);
        }
    }
    public enum MovementType
    {
        Vertical,
        Horizontal,
        Static
    }
}
