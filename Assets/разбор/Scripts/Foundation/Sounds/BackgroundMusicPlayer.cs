using UnityEngine;
using Zenject;
using Foundation.Sound;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [Inject]
    private IMusicPlayer _soundController;

    [SerializeField] private AudioClip _clip;
    [SerializeField] private bool _loop;


    private void Start()
    {
        _soundController.PlaySound(_clip);
    }
}
