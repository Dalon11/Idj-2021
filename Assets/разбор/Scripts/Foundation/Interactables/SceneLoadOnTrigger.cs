using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Foundation.Interactions
{
    public class SceneLoadOnTrigger : MonoBehaviour
    {
        [SerializeField] private string _playerTag = "Player";
        [SerializeField] private int _sceneIndex = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_playerTag))
            {
                SceneManager.LoadScene(_sceneIndex);
            }
        }
    }
}
