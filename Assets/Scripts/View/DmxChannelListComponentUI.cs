using com.kodai100.ArtNetApp.Entities;
using TMPro;
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
        
        public override void Initialize(DmxChannelEntity data)
        {
            base.Initialize(data);

            _text.text = data.ChannelName;
            _channelInputField.text = data.ChannelIndex.ToString();
            
            _slider.value = data.ChannelValue / 255f;
            _channelValueInputField.text = data.ChannelValue.ToString();

        }
    }
    
}