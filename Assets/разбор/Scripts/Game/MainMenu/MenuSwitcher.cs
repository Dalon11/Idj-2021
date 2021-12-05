using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSwitcher : MonoBehaviour
{
    [SerializeField] private MenuSwitchAnimator _animator;

    [Space]
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitOptionsButton;

    private void Start()
    {
        _optionsButton.onClick.AddListener(_animator.SwitchToOptions);
        _exitOptionsButton.onClick.AddListener(_animator.SwitchToMainMenu);
    }


}
