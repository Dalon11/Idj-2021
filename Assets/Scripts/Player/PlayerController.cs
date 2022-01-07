using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerController
{
    //���������� �������.

    //����������.
    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;
    //������ ���������.
    private List<VariableFloat> _speedList = null;
    //������ ��� �������� � ������. ����� ��� ������ ���������.
    private Vector3 _velocity, _scale = Vector3.zero;
    //����������� �������� �� ������.
    private FixedJoystick _fixedJoystick = null;
    //���������� ��� ������ �������� �� ������ ���������.
    //� ��� ����� ����� ������,��� ��� � ������ ������������.
    private StringBuilder _typeSpeed = null;
    //���� �� ����� ��� ����� � �������� �������� ��������������.
    private bool _isJump = false, _sneakAroundMod = false,_jumpMod = false;
    //��� ��� ��� �������� �� ������� ������� ������,�������� ������� ������ � Player.
    public bool IsJump { set => _isJump = value; }

    //��������� ������ ��� ������.
    public PlayerController(ref Rigidbody2D rigidbody, ref Animator animator, ref FixedJoystick fixedJoystick, List <VariableFloat> speedList)
    {
        _rigidbody = rigidbody;
        _animator = animator;
        _fixedJoystick = fixedJoystick;
        _speedList = speedList;
    }

    //����� ����� ������� ����� �������� ��������.
    public void Execute()
    {
        _jumpMod = !_isJump;//������ ����� �������� ������� �� ���������� ��� ������.
        //��������
        Move();
        //��������
        Flip();
        //��������
        AnimateControl();
    }

    //��������
    private void Flip()
    {
        //������ � ������������ ���������.
        if (_fixedJoystick.Horizontal != 0f)
        {
            _scale = _rigidbody.transform.localScale;
            //������ ��� ��� ��� ������� ����� ��� �� ��������� ��� ������� ����,��
            //� ������ ������ ����� �� ��������.
            //����������,������. � ������ ��.
            if (_fixedJoystick.Horizontal < 0f && _scale.x > 0f)
                _scale.x *= -1;
            if (_fixedJoystick.Horizontal > 0f && _scale.x < 0f)
                _scale.x *= -1;

            _rigidbody.transform.localScale = _scale;
        }
    }

    //������
    public void Jump()
    {
        //��������� �������� ���� �� �������.
        if (_sneakAroundMod)
            _sneakAroundMod = false;
        //���� ������ ��������,�� �������.
        if (_isJump)
            _rigidbody.AddForce(
                new Vector2(_rigidbody.velocity.x,_rigidbody.velocity.y + GetVariableFloat("Jump")),
                ForceMode2D.Impulse);
    }

    //��������
    public void SneakAround() =>  _sneakAroundMod = ! _sneakAroundMod;

    //������������ ��������
    private void AnimateControl()
    {
        if (_jumpMod)
            Animate(PlayerAnimate.Jump);
        else if (_sneakAroundMod && _fixedJoystick.Horizontal != 0f)
            Animate(PlayerAnimate.SitDownWalk);
        else if (_sneakAroundMod)
            Animate(PlayerAnimate.SitDownIdle);
        else if (_fixedJoystick.Horizontal != 0f)
            LeftOrRight();
        else Animate(PlayerAnimate.Idle);
    }

    //�� ����� ��� ������� ����������� animeteControl � ����� ������������ ������ � ���� ����.
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

    //����� ��� ������ �������� � ���������.
    private void Animate(PlayerAnimate playerAnimate) => _animator.SetInteger("Poss", (int)playerAnimate);

    //��������� ������ �������� �� �����.
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
        //���� ������������.
        _typeSpeed ??= new StringBuilder();

        if (! _sneakAroundMod)
            _typeSpeed.Append(_animator.GetInteger("Poss") == 1 ? "Walk" : "Run");
        else _typeSpeed.Append("SneakAround");

        _velocity.Set(_fixedJoystick.Horizontal * GetVariableFloat(_typeSpeed.ToString()), _rigidbody.velocity.y, _rigidbody.transform.position.z);
        _rigidbody.velocity = _velocity;

        _typeSpeed.Clear();
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
    Run,
    Jump,
    SitDown,
    SitDownWalk,
    SitDownIdle
}