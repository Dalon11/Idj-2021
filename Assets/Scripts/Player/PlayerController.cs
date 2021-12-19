using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    //Управления игроком.
    private Rigidbody2D _rigidbody = null;
    private Animator _animatior = null;
    private float _speed = 0;
    private Vector3 _velocity,tmp = Vector3.zero;

    public PlayerController(ref Rigidbody2D rigidbody, ref Animator animatior, float speed)
    {
        _rigidbody = rigidbody;
        _animatior = animatior;
            _speed = speed;
    }

    public void Execute()
    {
        Move();
        Flip();
        AnimateControl();
    }

    private void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            tmp = _rigidbody.transform.localScale;
            
            if (Input.GetAxisRaw("Horizontal") < 0f && tmp.x > 0f)
                tmp.x *= -1;
            if (Input.GetAxisRaw("Horizontal") > 0f && tmp.x < 0f)
                tmp.x *= -1;

            _rigidbody.transform.localScale = tmp;
        }
    }

    private void AnimateControl()
    {
        if (Input.GetAxis("Horizontal") != 0f)
            Animate(PlayerAnimate.Walk);
        else Animate(PlayerAnimate.Idle);
    }
    private void Animate(PlayerAnimate playerAnimate) => _animatior.SetInteger("Poss", (int)playerAnimate);

#if UNITY_ANDROID
    private void Move()
    {
    _velocity.Set(Input.GetAxisRaw("Horizontal") * _speed, _rigidbody.velocity.y,_rigidbody.transform.position.z);
    _rigidbody.velocity = _velocity;
    }
#endif

#if UNITY_STANDALONE
    private void Move()
    {
        _velocity.Set(Input.GetAxisRaw("Horizontal") * _speed, _rigidbody.velocity.y);
        _rigidbody.velocity = _velocity;
    }
#endif
}

public enum PlayerAnimate
{
    Idle,
    Walk
}