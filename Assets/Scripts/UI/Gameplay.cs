using UnityEngine.UI;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    //����� ������ ������� ��������,������������ �� ��� ������� ��� ������ � ���������� ������
    //��� ������ � ������� ���� �������� �������.
    [SerializeField] private Button _jumpBut = null;

    private Player _hero = null;

    private void Awake()
    {
        if (_jumpBut == null)
            return;

        _jumpBut.gameObject.SetActive(false);
        _hero ??= FindObjectOfType<Player>();
    }

    private void OnEnable() => _hero.JumpMod += CheckJumpButton;
    private void OnDisable() => _hero.JumpMod -= CheckJumpButton;
    private void OnDestroy() => _hero.JumpMod -= CheckJumpButton;

    private void CheckJumpButton() => _jumpBut.gameObject.SetActive(!_jumpBut.gameObject.activeSelf);
}
