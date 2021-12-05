using Foundation.Dialogs.Configs;
using System;
using System.Collections;
using UnityEngine;

namespace Foundation.UI.Dialogs
{
    public class DialogPopupController : AbstractService<IDialogController>, IDialogController
    {
        [SerializeField] private DialogPopupPresenter _dialogPresenter;
        [SerializeField] private float _timeUntilCanSkeep = 1f;

        private DialogData _dialogData;

        public ObserverList<IDialogEventsHolder> DialogEventsObservers => _dialogEventsObservers;
        private ObserverList<IDialogEventsHolder> _dialogEventsObservers = new ObserverList<IDialogEventsHolder>();

        public void StartDialog(DialogData dialogData)
        {
            _dialogData = dialogData;
            _dialogPresenter.SetSpeakers(_dialogData.Speakers.speakerOne, _dialogData.Speakers.speakerTwo);
            _dialogPresenter.Show();

            SendDialogOpenedEvent(true);

            StartCoroutine(DialogCoroutine());
        }

        private IEnumerator DialogCoroutine()
        {
            var phrases = _dialogData.Phrases;

            for (var i = 0; i < phrases.Count; i++)
            {
                _dialogPresenter.DisplayMessage(phrases[i]);

                yield return new WaitForSeconds(_timeUntilCanSkeep);

                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }

            _dialogPresenter.Hide();
            SendDialogOpenedEvent(false);
        }

        private void SendDialogOpenedEvent(bool opened)
        {
            foreach (var obs in _dialogEventsObservers.Enumerate())
                if (opened)
                    obs.OnDialogShown();
                else
                    obs.OnDialogHide();
        }
    }
}
