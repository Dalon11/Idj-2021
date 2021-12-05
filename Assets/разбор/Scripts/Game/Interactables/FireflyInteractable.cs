using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foundation.Interactions;
using Foundation.Character.Vfx;

namespace Game.Interactables
{
    public class FireflyInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _playerTag = "Player";
        public bool CanInteract => false;

        public void OnInteracted()
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_playerTag))
            {
                var animation = collision.GetComponent<OnInteractedWithCollectableAnimation>();
                animation?.Play();
                Destroy(gameObject);
            }
        }
    }
}
