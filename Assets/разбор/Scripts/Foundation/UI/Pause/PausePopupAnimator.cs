using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.UI
{
    public class PausePopupAnimator : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private float _animationDuration;

        public void Show()
        {
            _canvas.DOFade(1, _animationDuration);
            _canvas.blocksRaycasts = true;
            _canvas.interactable = true;
        }

        public void Hide()
        {
            _canvas.DOFade(0, _animationDuration);
            _canvas.blocksRaycasts = false;
            _canvas.interactable = false;
        }
    }
}
