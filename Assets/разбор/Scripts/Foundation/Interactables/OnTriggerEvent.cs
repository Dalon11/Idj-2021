using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Foundation.Interactions
{
    public class OnTriggerEvent : MonoBehaviour
    {
        [SerializeField] private string _playerTag = "Player";
        [SerializeField] private UnityEvent _onTriggerEnter;

        [Space]
        [SerializeField] private bool _disableOnTrigger = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_playerTag))
            {
                _onTriggerEnter?.Invoke();

                if (_disableOnTrigger)
                    enabled = false;
            }
        }
    }
}
