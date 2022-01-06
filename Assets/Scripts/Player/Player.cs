using System.Collections.Generic;
using UnityEngine;

public class Player : MonoCache
{
    //Класс настройки данных свзянных с игроком.
    [SerializeField] private FixedJoystick _fixedJoystick = null;
    [SerializeField] private PlayerStats _playerStats = null;

    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;
    private PlayerController _controller = null;

    private Singleton _singleton = Singleton.GetInstance();

    private void Awake()
    {
        _rigidbody ??= GetComponent<Rigidbody2D>();
        _animator ??= GetComponent<Animator>();
        _controller = new PlayerController(ref _rigidbody, ref _animator, ref _fixedJoystick,_playerStats.SpeedList);
    }

    private void OnEnable() => AddUpdate();
    private void OnDisable() => RemoveUpdate();
    private void OnDestroy() => RemoveUpdate();

    public override void UpdateTick()
    {
        if (!_singleton.IsPause)
            _controller.Execute();
    }
}

[System.Serializable]
public class PlayerStats
{
    //Класс для храненния данных игрока.
    [SerializeField] private float _healt = 100;
    [SerializeField] private List<VariableFloat> _speedList = null;

    public float Healt { get => _healt; set => _healt = value; }
    public List<VariableFloat> SpeedList => _speedList;
}

[System.Serializable]
public class VariableFloat
{
    [SerializeField] private string _name = null;
    [SerializeField] private float _speed = 0f;

    public string Name => _name;
    public float Speed => _speed;
}
