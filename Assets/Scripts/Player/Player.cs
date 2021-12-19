using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoCache
{
    //Класс настройки данных свзянных с игроком.
    [SerializeField] private PlayerStats _playerStats = null;

    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;
    private PlayerController _controller = null;

    private void Awake()
    {
        _rigidbody ??= GetComponent<Rigidbody2D>();
        _animator ??= GetComponent<Animator>();
        _controller = new PlayerController(ref _rigidbody, ref _animator, _playerStats.Speed);
    }

    private void OnEnable() => AddUpdate();
    private void OnDisable() => RemoveUpdate();
    private void OnDestroy() => RemoveUpdate();

    public override void UpdateTick() => _controller.Execute();

}

[System.Serializable]
public class PlayerStats
{
    //Класс для храненния данных игрока.
    [SerializeField] private float _healt = 100;
    [SerializeField] private float _speed = 10;

    public float Healt { get => _healt; set => _healt = value; }
    public float Speed { get => _speed; set => _speed = value; }
}
