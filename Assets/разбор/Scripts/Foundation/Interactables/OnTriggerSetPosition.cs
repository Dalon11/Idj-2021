using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Interactions
{
    public class OnTriggerSetPosition : MonoBehaviour
    {
        [SerializeField] private string _playerTag = "Player";
        [SerializeField] private Transform _position;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_playerTag))
                collision.gameObject.transform.position = _position.position;
        }
    }
}
