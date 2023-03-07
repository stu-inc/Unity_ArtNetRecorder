using com.kodai100.ArtNetApp.Entities;
using TMPro;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class FixturePresetListComponentUI : ListComponentView<FixturePresetEntity>
    {

        [SerializeField] private TMP_Text _text;
        [SerializeField] private UnityEngine.UI.Button _deleteButton;
        [SerializeField] private UnityEngine.UI.Button _editButton;
        
        public override void Initialize(FixturePresetEntity data)
        {
            base.Initialize(data);
            
            _text.text = data.Name;
        }

    }
}

