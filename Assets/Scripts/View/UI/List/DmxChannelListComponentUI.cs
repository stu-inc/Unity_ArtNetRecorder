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

        private Subject<(Guid, int)> _onChannelIndexChanged = new();
        public IObservable<(Guid, int)> OnChannelIndexChanged => _onChannelIndexChanged;

        private Subject<(Guid, int)> _onChannelValueChanged = new();
        public IObservable<(Guid, int)> OnChannelValueChanged => _onChannelValueChanged;
        
        public IObservable<Guid> OnDeleteButtonClicked => _deleteButton.OnClickAsObservable().Select(_ => _data.Guid);

        public override void Initialize(DmxChannelEntity data)
        {
            base.Initialize(data);

            _text.text = data.ChannelName;
            _channelInputField.text = data.ChannelIndex.ToString();
            
            _slider.value = data.ChannelValue;
            _channelValueInputField.text = data.ChannelValue.ToString();

            _channelInputField.onEndEdit.AsObservable().Subscribe(text =>
            {
                if (int.TryParse(text, out var result))
                {
                    _onChannelIndexChanged.OnNext((_data.Guid, result));
                }
                else
                {
                    _onChannelIndexChanged.OnNext((_data.Guid, 0));
                    _channelInputField.SetTextWithoutNotify(0.ToString());
                }
            }).AddTo(this);

            _channelValueInputField.onEndEdit.AsObservable().Subscribe(text =>
            {
                if (int.TryParse(text, out var result))
                {
                    _onChannelValueChanged.OnNext((_data.Guid, result));
                }
                else
                {
                    _onChannelValueChanged.OnNext((_data.Guid, 0));
                    _channelValueInputField.SetTextWithoutNotify(0.ToString());
                }
            }).AddTo(this);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">0-255</param>
        public void SetChannelValue(int value)
        {
            _slider.value = value;
            _channelValueInputField.text = value.ToString();
        }

        public void SetChannelIndex(int value)
        {
            _channelInputField.text = value.ToString();
        }
    }
    
}