using System;
using com.kodai100.ArtNetApp.Entities;
using TMPro;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class FixturePresetListComponentUI : ListComponentView<FixturePresetEntity>
    {

        [SerializeField] private TMP_Text _text;
        [SerializeField] private UnityEngine.UI.Button _editButton;
        public IObservable<Guid> OnEditButtonClicked => _editButton.OnClickAsObservable().Select(_ => _data.Guid);

        public override void Initialize(FixturePresetEntity data)
        {
            base.Initialize(data);
            
            _text.text = data.FixtureName;
        }

    }
}

