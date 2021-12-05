using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Dialogs.Configs
{
    [System.Serializable]
    public struct Speaker
    {
        public string ID;
        public Sprite SpeakerSprite;
    }

    [System.Serializable]
    public struct Phrase
    {
        public string SpeakerId;
        public string PhraseAlias;
        public Color PhraseColor;
    }

    [CreateAssetMenu(fileName = "DialogData", menuName = "Dialogs/CreateDialogData", order = 0)]
    public class DialogData : ScriptableObject
    {
        [SerializeField] private Speaker _speakerOne;
        [SerializeField] private Speaker _speakerTwo;
        [SerializeField] private List<Phrase> _phrases;

        public (Speaker speakerOne, Speaker speakerTwo) Speakers => (_speakerOne, _speakerTwo);
        public List<Phrase> Phrases => _phrases;
    }
}
