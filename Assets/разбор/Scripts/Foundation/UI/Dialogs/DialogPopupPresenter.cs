using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Foundation.UI.Localization;
using Foundation.Dialogs.Configs;

namespace Foundation.UI.Dialogs
{
    public class DialogPopupPresenter : MonoBehaviour
    {
        [SerializeField] private Image _leftSpeakerImage;
        [SerializeField] private Image _rightSpeakerImage;
        [SerializeField] private LocalizedTextWrapper _dialogText;

        [Space]
        [SerializeField] private Color _inactiveColor;
        [SerializeField] private float _animationDuration = 0.5f;

        private Speaker _leftSpeaker;
        private Speaker _rightSpeaker;

        private Speaker _currentSpeaker;

        private enum SpeakerSide
        {
            Left, Right
        }

        public void SetSpeakers(Speaker speakerOne, Speaker speakerTwo)
        {
            _leftSpeaker = speakerOne;
            _rightSpeaker = speakerTwo;

            _leftSpeakerImage.sprite = _leftSpeaker.SpeakerSprite;
            _rightSpeakerImage.sprite = _rightSpeaker.SpeakerSprite;
        }

        public void DisplayMessage(Phrase phrase)
        {
            if (phrase.SpeakerId != _currentSpeaker.ID)
            {
                ChangeSpeaker(phrase.SpeakerId);
            }

            _dialogText.SetLocalizationKey(phrase.PhraseAlias);
            _dialogText.InnetText.color = phrase.PhraseColor;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void ChangeSpeaker(string newSpeakerId)
        {
            if (newSpeakerId == _leftSpeaker.ID)
            {
                _currentSpeaker = _leftSpeaker;
                SwitchToSpeakerSide(SpeakerSide.Left);
            }
            else if (newSpeakerId == _rightSpeaker.ID)
            {
                _currentSpeaker = _rightSpeaker;
                SwitchToSpeakerSide(SpeakerSide.Right);
            }
            else
            {
                Debug.LogError($"No speaker with such id {newSpeakerId}. Available speakers: {_leftSpeaker.ID} | {_rightSpeaker.ID}");
            }
        }

        private void SwitchToSpeakerSide(SpeakerSide speakerSide)
        {
            if (speakerSide == SpeakerSide.Left)
            {
                _leftSpeakerImage.DOColor(Color.white, _animationDuration);
                _rightSpeakerImage.DOColor(_inactiveColor, _animationDuration);
            }
            else
            {
                _leftSpeakerImage.DOColor(_inactiveColor, _animationDuration); ;
                _rightSpeakerImage.DOColor(Color.white, _animationDuration);
            }
        }
    }
}
