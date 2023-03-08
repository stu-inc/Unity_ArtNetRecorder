using System;
using System.Collections.Generic;
using System.Linq;
using com.kodai100.ArtNetApp.Entities;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class DmxChannelListUI : ReorderableListView<DmxChannelEntity>
    {
        [SerializeField] private DmxChannelListComponentUI _componentViewPrefab;
        
        private Subject<Guid> _onComponentDeleted = new();
        public IObservable<Guid> OnComponentDeleted => _onComponentDeleted;
        
        protected override ReorderableListComponentView<DmxChannelEntity> ReorderableListComponentViewPrefab =>
            _componentViewPrefab;

        protected override void RegisterComponentEvent(ListComponentView<DmxChannelEntity> component, List<IDisposable> disposables)
        {
            base.RegisterComponentEvent(component, disposables);
            
            var converted = component as DmxChannelListComponentUI;
            if (converted != null)
            {
                converted.OnDeleteButtonClicked.Subscribe(guid =>
                {
                    _onComponentDeleted.OnNext(guid);
                })
                .AddTo(disposables);
            }
        }
        
    }
}