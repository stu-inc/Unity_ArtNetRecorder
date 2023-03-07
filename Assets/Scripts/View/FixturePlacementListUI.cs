using com.kodai100.ArtNetApp.Entities;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class FixturePlacementListUI : ReorderableListView<FixturePlacementEntity>
    {
        
        [SerializeField] private ReorderableListComponentView<FixturePlacementEntity> _componentViewPrefab;

        protected override ReorderableListComponentView<FixturePlacementEntity> ReorderableListComponentViewPrefab =>
            _componentViewPrefab;
    }
}

