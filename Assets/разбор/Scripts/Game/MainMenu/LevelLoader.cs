    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    private void Start()
    {
        _startGameButton.onClick.AddListener(LoadLevel);
    }

    public void LoadLevel()
    {
        Debug.Log("Start");
        SceneManager.LoadScene(1);
    }
}
