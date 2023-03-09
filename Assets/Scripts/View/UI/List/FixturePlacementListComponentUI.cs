using System;
using com.kodai100.ArtNetApp.Entities;
using TMPro;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class FixturePlacementListComponentUI : ReorderableListComponentView<FixturePlacementEntity>
    {

        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_InputField _channelOffsetInputField;
        [SerializeField] private UnityEngine.UI.Button _deleteButton;

        private Subject<(Guid, int)> _onChannelOffsetChanged = new();
        public IObservable<(Guid, int)> OnChannelOffsetChanged => _onChannelOffsetChanged;

        public override void Initialize(FixturePlacementEntity data)
        {
            base.Initialize(data);

            _text.text = data.Name;
            _channelOffsetInputField.text = data.ChannelOffset.ToString();

            _channelOffsetInputField.onEndEdit.AsObservable().Subscribe(text =>
            {
                if (int.TryParse(text, out var result))
                {
                    _onChannelOffsetChanged.OnNext((_data.Guid, result));
                }
                else
                {
                    _onChannelOffsetChanged.OnNext((_data.Guid, 0));
                    _channelOffsetInputField.SetTextWithoutNotify(0.ToString());
                }
            }).AddTo(this);
        }
        
        public void SetChannelOffset(int value)
        {
            _channelOffsetInputField.text = value.ToString();
        }

    }

}

