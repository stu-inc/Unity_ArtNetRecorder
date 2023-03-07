using com.kodai100.ArtNetApp.Entities;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class DmxChannelListUI : ReorderableListView<DmxChannelEntity>
    {
        [SerializeField] private ReorderableListComponentView<DmxChannelEntity> _componentViewPrefab;

        protected override ReorderableListComponentView<DmxChannelEntity> ReorderableListComponentViewPrefab =>
            _componentViewPrefab;
    }
}