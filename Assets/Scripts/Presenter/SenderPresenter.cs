using System;
using System.Collections.Generic;
using com.kodai100.ArtNetApp.Entities;
using com.kodai100.ArtNetApp.Models;
using com.kodai100.ArtNetApp.View;
using UnityEngine;
using UniRx;
using Button = UnityEngine.UI.Button;

namespace com.kodai100.ArtNetApp.Presenter
{

    public partial class SenderPresenter : Presenter<SenderModel>
    {
        
        public override IEnumerable<IDisposable> Bind(SenderModel model)
        {
            var list = new List<IDisposable>();
            list.AddRange(BindFixturePresetList(model));
            list.AddRange(BindFixturePlacementList(model));
            list.AddRange(BindDmxChannelList(model));
            return list;
        }
    }

    public partial class SenderPresenter
    {
        
        [SerializeField] private FixturePresetListUI _fixturePresetListUI;

        public IObservable<Guid> OnFixturePresetSelected => _fixturePresetListUI.OnComponentSelected;
        public IObservable<Guid> OnEditButtonClicked => _fixturePresetListUI.OnComponentEditButtonClicked;

        private IEnumerable<IDisposable> BindFixturePresetList(SenderModel model)
        {
            yield return model.FixturePresetList.Subscribe(dataList =>
            {
                _fixturePresetListUI.Initialize(dataList);
            });

            yield return model.SelectedFixturePresetEntity.Subscribe(selectedData =>
            {
                _fixturePresetListUI.MarkAsSelected(selectedData?.Guid);
            });
        }
    }

    public partial class SenderPresenter
    {
        
        [SerializeField] private FixturePlacementListUI _fixturePlacementListUI;
        public IObservable<Guid> OnFixturePlacementSelected => _fixturePlacementListUI.OnComponentSelected;
        public IObservable<IEnumerable<ReorderableEntity>> OnFixturePlacementOrderChanged => _fixturePlacementListUI.OnListOrderChanged;
        
        
        [SerializeField] private UniverseSelectionUI _universeSelectionUI;
        public IObservable<int> OnUniverseInputChanged => _universeSelectionUI.OnUniverseInputFieldValueChanged;
        public IObservable<Unit> OnIncrementUniverseButtonClicked => _universeSelectionUI.OnIncrementButtonClicked;
        public IObservable<Unit> OnDecrementUniverseButtonClicked => _universeSelectionUI.OnDecrementButtonClicked;


        private IEnumerable<IDisposable> BindFixturePlacementList(SenderModel model)
        {
            yield return model.FixturePlacementList.Subscribe(dataList =>
            {
                _fixturePlacementListUI.Initialize(dataList);
            });

            yield return model.SelectedFixturePlacementEntity.Subscribe(selectedData =>
            {
                _fixturePlacementListUI.MarkAsSelected(selectedData?.Guid);
            });
            
            
            
            // Universe selection
            yield return model.Universe.Subscribe(dataList =>
            {
                _universeSelectionUI.SetValueWithoutNotify(dataList);
            });
        }
    }


    public partial class SenderPresenter
    {
        
        [SerializeField] private DmxChannelListUI _dmxChannelListUI;
        public IObservable<Guid> OnDmxChannelSelected => _dmxChannelListUI.OnComponentSelected;
        public IObservable<IEnumerable<ReorderableEntity>> OnDmxChannelOrderChanged => _dmxChannelListUI.OnListOrderChanged;
        
        private IEnumerable<IDisposable> BindDmxChannelList(SenderModel model)
        {
            yield return model.DmxChannelList.Subscribe(dataList =>
            {
                _dmxChannelListUI.Initialize(dataList);
            });

            yield return model.SelectedDmxChannelEntity.Subscribe(selectedData =>
            {
                _dmxChannelListUI.MarkAsSelected(selectedData?.Guid);
            });
            
        }
    }

    
}

