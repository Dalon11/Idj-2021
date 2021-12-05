using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Foundation.UI
{
    public class PauseController : AbstractService<IPauseEventProvider>, IPauseEventProvider
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _toMenuButton;
        [SerializeField] private PausePopupAnimator _animator;

        private bool _shown = false;

        public ObserverList<IPauseEventHolder> OnPauseObservers => _pauseObservers;
        private ObserverList<IPauseEventHolder> _pauseObservers = new ObserverList<IPauseEventHolder>();

        public override void Start()
        {
            base.Start();

            _continueButton.onClick.AddListener(() => { Hide(); SendOnPauseEvent(false); });
            _toMenuButton.onClick.AddListener(BackToMainMenu);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_shown)
                    Hide();
                else
                    Show();

                _shown = !_shown;
                SendOnPauseEvent(_shown);
            }
        }

        private void Show()
        {
            _animator.Show();
        }

        private void Hide()
        {
            _animator.Hide();
        }
        
        private void SendOnPauseEvent(bool shown)
        {
            foreach (var obs in _pauseObservers.Enumerate())
                obs.OnPause(shown);
        }

        private void BackToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}