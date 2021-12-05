using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFlipper : MonoBehaviour
{
    [SerializeField] List<Transform> _transformToFlip;
    [SerializeField] List<GameObject> _objectToFlip;
    [SerializeField] private List<SpriteRenderer> _spriteToFlip;
    public void SetFlip(float directionX)
    {
        if (_transformToFlip != null)
        {
            foreach (var transform in _transformToFlip)
            {
                var currentPosition = transform.localPosition;
                currentPosition.x = Mathf.Abs(currentPosition.x) * directionX;
                transform.localPosition = currentPosition;
            }
        }
        if (_spriteToFlip != null)
        {
            foreach (var sr in _spriteToFlip)
            {
                bool flipX = sr.flipX;
                flipX = (directionX < 0f) ? true : false;
                sr.flipX = flipX;
            }
        }

        if (_objectToFlip != null)
        {
            foreach (var obj in _objectToFlip)
            {
                if (directionX > 0)
                {
                    obj.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (directionX < 0)
                {
                    obj.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }
}
