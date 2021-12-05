using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuSwitchAnimator : MonoBehaviour
{
    [SerializeField] private CanvasGroup _mainMenuCanvas;
    [SerializeField] private CanvasGroup _optionsMenuCanvas;

    [Space]
    [SerializeField] private float _transitionDuration = 1f;

    public void SwitchToMainMenu()
    {
        StartCoroutine(SwitchToMainMenuInternal());
    }

    public void SwitchToOptions()
    {
        StartCoroutine(SwitchToOptionsInternal());
    }

    private IEnumerator SwitchToOptionsInternal()
    {
        _mainMenuCanvas.DOFade(0f, _transitionDuration / 2);
        DisableCanvas(_mainMenuCanvas);

        yield return new WaitForSeconds(_transitionDuration / 4);

        _optionsMenuCanvas.DOFade(1f, _transitionDuration / 2);
        EnableCanvas(_optionsMenuCanvas);
    }

    private IEnumerator SwitchToMainMenuInternal()
    {
        _optionsMenuCanvas.DOFade(0f, _transitionDuration / 2);
        DisableCanvas(_optionsMenuCanvas);

        yield return new WaitForSeconds(_transitionDuration / 4);

        _mainMenuCanvas.DOFade(1f, _transitionDuration / 2);
        EnableCanvas(_mainMenuCanvas);
    }

    private void DisableCanvas(CanvasGroup canvas)
    {
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

    private void EnableCanvas(CanvasGroup canvas)
    {
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }
}
