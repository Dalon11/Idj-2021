using System.Collections;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    //Так как котсная анимация которую я делал полетела.
    //То этот класс сейчас бесполезен.
    //Он нужен был для кат сцены
    //Где персонаж берёт банку(физический объект) из рук девы.
    [SerializeField] private Animator _animator = null;
    [SerializeField] private HingeJoint2D _rope = null;

    private Rigidbody2D _rigidbody2D = null;
    private Singleton _singleton = Singleton.GetInstance();
    private void Start() => _rigidbody2D = GetComponent<Rigidbody2D>();
    private void OnEnable()
    {
        _singleton.IsPause = true;
        _animator.SetInteger("Poss", 2);
        StartCoroutine(DoEnd());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("R_Hand")) _rope.connectedBody = _rigidbody2D;
    }

    IEnumerator DoEnd()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * 
            (1 /_animator.GetCurrentAnimatorStateInfo(0).speed));

        enabled = _singleton.IsPause = false;
    }

}
