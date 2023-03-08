using com.kodai100.ArtNetApp.Entities;
using TMPro;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class FixtureManufacturerListComponentUI : ListComponentView<FixtureManufacturerEntity>
    {
        [SerializeField] private TMP_Text _text;
        
        public override void Initialize(FixtureManufacturerEntity data)
        {
            base.Initialize(data);
            _text.text = data.ManufacturerName;
        }
    }
}