using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoCache
{
    //����� ��������� ������ �������� � �������.
    [SerializeField] private FixedJoystick _fixedJoystick = null;
    [SerializeField] private PlayerStats _playerStats = null;

    //����������
    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;
    //���������
    private Singleton _singleton = Singleton.GetInstance();
    //������� ��� ��������� ������ ������.
    public event Action JumpMod = delegate { };
    //����� � ������� ����� �� �������������.
    private PlayerController _controller = null;

    private void Awake()
    {
        //��������� �����������.
        _rigidbody ??= GetComponent<Rigidbody2D>();
        _animator ??= GetComponent<Animator>();
        //�������� ������ ������ � ����� �����������
        _controller = new PlayerController(ref _rigidbody, ref _animator, ref _fixedJoystick, _playerStats.SpeedList);
    }

    //��� ��� ����� ������������ �� �������,�� ������� ��� � ������ ��� ������ ���������.
    //��� �� � ������� ��� ������������ � ��������� ������.
    private void OnEnable() => AddUpdate();
    private void OnDisable() => RemoveUpdate();
    private void OnDestroy() => RemoveUpdate();

    public override void UpdateTick()
    {
        //���� ����� �� ����.
        //�� ����� ������������ ����������.
        if (!_singleton.IsPause)
            _controller.Execute();
    }

    //������ ��� ������.
    //������ �� ���������� ��� ��� ����������.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Ground"))
        {
            _controller.IsJump = true;
            JumpMod?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Ground"))
        {
            _controller.IsJump = false;
            JumpMod?.Invoke();
        }
    }

    //��� ������ ������� �� ������� ��������,�� ����� ��������.
    public void Jump() => _controller.Jump();
    public void SneakAround() => _controller.SneakAround();
}

//����� ��� ��������� ������.
[System.Serializable]
public class PlayerStats
{
    //����� ��� ��������� ������ ������.
    [SerializeField] private float _healt = 100;
    [SerializeField] private List<VariableFloat> _speedList = null;

    public float Healt { get => _healt; set => _healt = value; }
    public List<VariableFloat> SpeedList => _speedList;
}

//����� ��� �������� ���������� ������ ������ ��������� ������.
//�� ����� ����� ��������� �� ������ ��� �����.
[System.Serializable]
public class VariableFloat
{
    [SerializeField] private string _name = null;
    [SerializeField] private float _speed = 0f;

    public string Name => _name;
    public float Speed => _speed;
}
