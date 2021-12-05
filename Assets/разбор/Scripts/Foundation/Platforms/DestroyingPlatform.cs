using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingPlatform : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _delayTime = 0.5f;
    [SerializeField] private float _backingTime = 2f;
    [SerializeField] private string _playerTag = "Player";
    private Animator _animator;
    private Vector2 _currentPosition;
    private bool _movingBack;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _currentPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {        
        if (other.gameObject.CompareTag(_playerTag))
        {
            _animator.SetTrigger("StartDestroying");
            Invoke("FallPlatform", _delayTime);
        }        
    }

    private void FallPlatform()
    {
        _rb.isKinematic = false;
        _boxCollider2D.enabled = false;
        _animator.SetTrigger("Disappear");
        Invoke("BackPlatform", _backingTime);
    }

    private void BackPlatform()
    {
        _rb.velocity = Vector2.zero;
        _spriteRenderer.enabled = false;
        _rb.isKinematic = true;
        _movingBack = true;
        _animator.SetTrigger("Normal");
       
    }

    private void Update()
    {
        if (_movingBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, _currentPosition, 
                20f * Time.deltaTime);
        }

        if (transform.position.y == _currentPosition.y)
        {
            _movingBack = false;
            _boxCollider2D.enabled = true;
            _spriteRenderer.enabled = true;
        }
    }
}
