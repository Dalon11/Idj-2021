using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoCache
{
    //Класс настройки данных свзянных с игроком.
    [SerializeField] private FixedJoystick _fixedJoystick = null;
    [SerializeField] private PlayerStats _playerStats = null;

    //Компоненты
    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;
    //Синглетон
    private Singleton _singleton = Singleton.GetInstance();
    //Событие для активации кнопки прыжка.
    public event Action JumpMod = delegate { };
    //Класс в котором будет всё настраиваться.
    private PlayerController _controller = null;

    private void Awake()
    {
        //Получение компонентов.
        _rigidbody ??= GetComponent<Rigidbody2D>();
        _animator ??= GetComponent<Animator>();
        //Передача нужных данных в класс управленияю
        _controller = new PlayerController(ref _rigidbody, ref _animator, ref _fixedJoystick, _playerStats.SpeedList);
    }

    //Так как класс наследуеться от Монокеш,то заносим его в список для апдейт менеджера.
    //Так же и убираем для безопасности в следующих сценах.
    private void OnEnable() => AddUpdate();
    private void OnDisable() => RemoveUpdate();
    private void OnDestroy() => RemoveUpdate();

    public override void UpdateTick()
    {
        //Если паузы не было.
        //То можно использовать управление.
        if (!_singleton.IsPause)
            _controller.Execute();
    }

    //Методы для прыжка.
    //Можешь их переписать под своё усмотрение.
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

    //Эти методы вызваны на кнопках канвасах,по этому публичны.
    public void Jump() => _controller.Jump();
    public void SneakAround() => _controller.SneakAround();
}

//Класс для настройки данных.
[System.Serializable]
public class PlayerStats
{
    //Класс для храненния данных игрока.
    [SerializeField] private float _healt = 100;
    [SerializeField] private List<VariableFloat> _speedList = null;

    public float Healt { get => _healt; set => _healt = value; }
    public List<VariableFloat> SpeedList => _speedList;
}

//Класс для удобного заполнения данных разных скоростях игрока.
//Но иожно будет применить не только для этого.
[System.Serializable]
public class VariableFloat
{
    [SerializeField] private string _name = null;
    [SerializeField] private float _speed = 0f;

    public string Name => _name;
    public float Speed => _speed;
}
