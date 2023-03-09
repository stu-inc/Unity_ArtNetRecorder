using System;
using System.Collections.Generic;
using System.Linq;
using com.kodai100.ArtNetApp.Entities;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class FixturePlacementListUI : ReorderableListView<FixturePlacementEntity>
    {
        
        [SerializeField] private ReorderableListComponentView<FixturePlacementEntity> _componentViewPrefab;

        protected override ReorderableListComponentView<FixturePlacementEntity> ReorderableListComponentViewPrefab =>
            _componentViewPrefab;

        private Subject<(Guid, int)> _onChannelOffsetChanged = new();
        public IObservable<(Guid, int)> OnChannelOffsetChanged => _onChannelOffsetChanged;

        protected override void RegisterComponentEvent(ListComponentView<FixturePlacementEntity> component, List<IDisposable> disposables)
        {
            base.RegisterComponentEvent(component, disposables);
            
            var converted = component as FixturePlacementListComponentUI;
            if (converted != null)
            {
                converted.OnChannelOffsetChanged.Subscribe(v =>
                {
                    _onChannelOffsetChanged.OnNext(v);
                })
                .AddTo(disposables);
            }
        }
        
        public void SetChannelOffset(Guid id, int value)
        {
            // TODO: 値が変更されるたびに検索が走るので、もっといい方法があるはず
            // → キャッシュして、GUIDが変わってない時はキャッシュから取得するようにする
            
            var target = _listComponents.FirstOrDefault(x => x.Data.Guid == id);
            if (target == null) return;
            
            var converted = target as FixturePlacementListComponentUI;
            if (converted == null) return;
            
            converted.SetChannelOffset(value);
        }

    }
}

