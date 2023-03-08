using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class AddChannelUI : MonoBehaviour
    {

        [SerializeField] private UnityEngine.UI.Button _addButton;
        [SerializeField] private TMP_InputField _inputField;
        
        public IObservable<string> OnAddChannelButtonClicked => _addButton.OnClickAsObservable().Select(x => _inputField.text);

    }

}

