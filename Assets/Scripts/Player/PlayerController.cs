using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerController
{
    //Управления игроком.

    //Компоненты.
    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;
    //Список скоростей.
    private List<VariableFloat> _speedList = null;
    //Ветора для скорости и скейла. Скейл для измени разворота.
    private Vector3 _velocity, _scale = Vector3.zero;
    //Виртуальный джойстик из ассета.
    private FixedJoystick _fixedJoystick = null;
    //Переменная для поиска скорости из списка скоростей.
    //С ней часто будет работа,так что я выбрал странгбилдер.
    private StringBuilder _typeSpeed = null;
    //Пока не решил как лучше и пришлось наделать переключателей.
    private bool _isJump = false, _sneakAroundMod = false,_jumpMod = false;
    //Так как тут проверку на колизии сделать нельзя,пришлось вынести доступ в Player.
    public bool IsJump { set => _isJump = value; }

    //Получение нужных нам данных.
    public PlayerController(ref Rigidbody2D rigidbody, ref Animator animator, ref FixedJoystick fixedJoystick, List <VariableFloat> speedList)
    {
        _rigidbody = rigidbody;
        _animator = animator;
        _fixedJoystick = fixedJoystick;
        _speedList = speedList;
    }

    //Метод через который будет доступен контроль.
    public void Execute()
    {
        _jumpMod = !_isJump;//Прыжок имеет обратную сторону от разрешения для прыжка.
        //Движение
        Move();
        //Разворот
        Flip();
        //Анимации
        AnimateControl();
    }

    //Разворот
    private void Flip()
    {
        //Данные с вритуального джойстика.
        if (_fixedJoystick.Horizontal != 0f)
        {
            _scale = _rigidbody.transform.localScale;
            //Уверен что эти две условки можно как то упростить или создать одну,но
            //я вообще ничего лучше не придумал.
            //Придумаешь,сделай. Я только за.
            if (_fixedJoystick.Horizontal < 0f && _scale.x > 0f)
                _scale.x *= -1;
            if (_fixedJoystick.Horizontal > 0f && _scale.x < 0f)
                _scale.x *= -1;

            _rigidbody.transform.localScale = _scale;
        }
    }

    //Прыжок
    public void Jump()
    {
        //Выключаем красться если он включён.
        if (_sneakAroundMod)
            _sneakAroundMod = false;
        //Если прижок разрешён,то прыгаем.
        if (_isJump)
            _rigidbody.AddForce(
                new Vector2(_rigidbody.velocity.x,_rigidbody.velocity.y + GetVariableFloat("Jump")),
                ForceMode2D.Impulse);
    }

    //Красться
    public void SneakAround() =>  _sneakAroundMod = ! _sneakAroundMod;

    //Переключение анимаций
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

    //Не хотел ещё сильнее напичкивать animeteControl и вынес переключение хотьбы и бега сюда.
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

    //Метод для вызова анимации в аниматоре.
    private void Animate(PlayerAnimate playerAnimate) => _animator.SetInteger("Poss", (int)playerAnimate);

    //Получение нужной скорости по имени.
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
        //Само передвижение.
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