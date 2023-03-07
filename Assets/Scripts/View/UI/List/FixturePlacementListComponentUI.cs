using com.kodai100.ArtNetApp.Entities;
using TMPro;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class FixturePlacementListComponentUI : ReorderableListComponentView<FixturePlacementEntity>
    {

        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_InputField _channelOffsetInputField;
        [SerializeField] private UnityEngine.UI.Button _deleteButton;
        
        public override void Initialize(FixturePlacementEntity data)
        {
            base.Initialize(data);

            _text.text = data.Name;
            _channelOffsetInputField.text = data.ChannelOffset.ToString();
        }
    }

}

