using System;
using com.kodai100.ArtNetApp.Entities;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace com.kodai100.ArtNetApp.View
{
    
    public class DmxChannelListComponentUI : ReorderableListComponentView<DmxChannelEntity>
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_InputField _channelInputField;
        [SerializeField] private TMP_InputField _channelValueInputField;
        [SerializeField] private Slider _slider;
        [SerializeField] private UnityEngine.UI.Button _deleteButton;
        
        public IObservable<Guid> OnDeleteButtonClicked => _deleteButton.OnClickAsObservable().Select(_ => _data.Guid);

        public override void Initialize(DmxChannelEntity data)
        {
            base.Initialize(data);

            _text.text = data.ChannelName;
            _channelInputField.text = data.ChannelIndex.ToString();
            
            _slider.value = data.ChannelValue;
            _channelValueInputField.text = data.ChannelValue.ToString();

        }
    }
    
}