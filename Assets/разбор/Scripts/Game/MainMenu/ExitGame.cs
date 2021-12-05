using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    [SerializeField] private Button _exitGameButton;

    private void Start()
    {
        _exitGameButton.onClick.AddListener(ExitFromGame);
    }

    private void ExitFromGame()
    {
        Application.Quit();
    }
}
