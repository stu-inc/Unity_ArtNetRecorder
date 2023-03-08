using com.kodai100.ArtNetApp.Entities;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class FixtureManufacturerListUI : ListView<FixtureManufacturerEntity>
    {
        [SerializeField] private FixtureManufacturerListComponentUI _componentUI;

        protected override ListComponentView<FixtureManufacturerEntity> ListComponentViewPrefab => _componentUI;

    }

}

