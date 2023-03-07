using com.kodai100.ArtNetApp.Entities;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class FixturePresetListUI : ListView<FixturePresetEntity>
    {
        [SerializeField] private ListComponentView<FixturePresetEntity> _componentViewPrefab;

        protected override ListComponentView<FixturePresetEntity> ListComponentViewPrefab => _componentViewPrefab;
    }

}
