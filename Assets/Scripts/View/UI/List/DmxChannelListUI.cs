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
        private Subject<(Guid, int)> _onChannelIndexChanged = new();
        private Subject<(Guid, int)> _onChannelValueChanged = new();
        
        public IObservable<(Guid, int)> OnChannelIndexChanged => _onChannelIndexChanged;
        public IObservable<(Guid, int)> OnChannelValueChanged => _onChannelValueChanged;
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

                converted.OnChannelIndexChanged.Subscribe(v =>
                {
                    _onChannelIndexChanged.OnNext(v);
                }).AddTo(disposables);

                converted.OnChannelValueChanged.Subscribe(v =>
                {
                    _onChannelValueChanged.OnNext(v);
                }).AddTo(this);
            }
        }

        public void SetChannelIndex(Guid id, int value)
        {
            // TODO: 値が変更されるたびに検索が走るので、もっといい方法があるはず
            // → キャッシュして、GUIDが変わってない時はキャッシュから取得するようにする
            
            var target = _listComponents.FirstOrDefault(x => x.Data.Guid == id);
            if (target == null) return;
            
            var converted = target as DmxChannelListComponentUI;
            if (converted == null) return;
            
            converted.SetChannelIndex(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Target component's GUID</param>
        /// <param name="value">0-255 dmx value</param>
        public void SetChannelValue(Guid id, int value)
        {
            // TODO: 値が変更されるたびに検索が走るので、もっといい方法があるはず
            // → キャッシュして、GUIDが変わってない時はキャッシュから取得するようにする
            
            var target = _listComponents.FirstOrDefault(x => x.Data.Guid == id);
            if (target == null) return;
            
            var converted = target as DmxChannelListComponentUI;
            if (converted == null) return;
            
            converted.SetChannelValue(value);
        }
    }
}