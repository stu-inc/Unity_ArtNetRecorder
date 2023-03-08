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

        protected override void RegisterComponentEvent(ListComponentView<FixturePresetEntity> component,
            List<IDisposable> disposables)
        {
            
            base.RegisterComponentEvent(component, disposables);
            
            var converted = component as FixturePresetListComponentUI;
            if (converted != null)
            {
                converted.OnEditButtonClicked.Subscribe(guid => { _onComponentEditButtonClicked.OnNext(guid); })
                    .AddTo(disposables);
            }
            
        }
    }

}
