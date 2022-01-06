using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    //Управления игроком.
    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;
    private List<VariableFloat> _speedList = null;
    private Vector3 _velocity, tmp = Vector3.zero;

    private FixedJoystick _fixedJoystick = null;

    private string _typeSpeed = null;

    public PlayerController(ref Rigidbody2D rigidbody, ref Animator animator, ref FixedJoystick fixedJoystick, List <VariableFloat> speedList)
    {
        _rigidbody = rigidbody;
        _animator = animator;
        _fixedJoystick = fixedJoystick;
        _speedList = speedList;
    }

    public void Execute()
    {
        Move();
        Flip();
        AnimateControl();
    }

    private void Flip()
    {
        if (_fixedJoystick.Horizontal != 0f)
        {
            tmp = _rigidbody.transform.localScale;

            if (_fixedJoystick.Horizontal < 0f && tmp.x > 0f)
                tmp.x *= -1;
            if (_fixedJoystick.Horizontal > 0f && tmp.x < 0f)
                tmp.x *= -1;

            _rigidbody.transform.localScale = tmp;
        }
    }

    private void AnimateControl()
    {
        if (_fixedJoystick.Horizontal != 0f)
            LeftOrRight();
        else Animate(PlayerAnimate.Idle);
    }

    private void LeftOrRight()
    {
        if (_fixedJoystick.Horizontal < 0f)
        {
            if (_fixedJoystick.Horizontal >= -.5f)
                Animate(PlayerAnimate.Walk);
            else
                Animate(PlayerAnimate.Run);
        }
        else
        {
            if (_fixedJoystick.Horizontal <= .5f)
                Animate(PlayerAnimate.Walk);
            else
                Animate(PlayerAnimate.Run);
        }
    }
    private void Animate(PlayerAnimate playerAnimate) => _animator.SetInteger("Poss", (int)playerAnimate);

    private float GetVariableFloat(string variable)
    {
        float result = 0f;
        bool stop = false;

        _speedList.ForEach(var =>
        {
            if (stop) return;

            if (var.Name.Equals(variable))
            {
                result = var.Speed;
                stop = true;
            }
        });

        return result;
    }

#if UNITY_ANDROID
    private void Move()
    {
        _typeSpeed = _animator.GetInteger("Poss") == 1 ? "Walk" : "Run";

        _velocity.Set(_fixedJoystick.Horizontal * GetVariableFloat(_typeSpeed), _rigidbody.velocity.y, _rigidbody.transform.position.z);
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
    Walk,
    Run
}