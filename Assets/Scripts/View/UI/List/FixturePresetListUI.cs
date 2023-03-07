using System;
using System.Collections.Generic;
using System.Linq;
using com.kodai100.ArtNetApp.Entities;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class FixturePresetListUI : ListView<FixturePresetEntity>
    {
        [SerializeField] private FixturePresetListComponentUI _componentViewPrefab;

        protected override ListComponentView<FixturePresetEntity> ListComponentViewPrefab => _componentViewPrefab;


        private Subject<Guid> _onComponentEditButtonClicked = new();
        public IObservable<Guid> OnComponentEditButtonClicked => _onComponentEditButtonClicked;
        
        public override void Initialize(IEnumerable<FixturePresetEntity> dataList)
        {
            base.Initialize(dataList);
            
            _listComponents
                .Select(x => x as FixturePresetListComponentUI)
                .ToList()
                .ForEach(c =>
            {
                c.OnEditButtonClicked.Subscribe(guid =>
                {
                    _onComponentEditButtonClicked.OnNext(guid);
                }).AddTo(_disposables);
            });
        }
    }

}
