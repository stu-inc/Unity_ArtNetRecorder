using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Timers;
using Assets.Scripts.View;
using com.kodai100.ArtNetApp.Entities;
using com.kodai100.ArtNetApp.Models;
using com.kodai100.ArtNetApp.View;
using UnityEngine;
using UniRx;

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
            list.AddRange(InitializeDmxSender(model));
            return list;
        }
    }

    // DMX Sender
    public partial class SenderPresenter
    {

        private ArtNetSender _sender;

        private Timer _timer;
        
        private IEnumerable<IDisposable> InitializeDmxSender(SenderModel model)
        {
            yield return _sender = new ArtNetSender();

            // TODO: 軽量バッファ作成？
            yield return Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1000f/30f))
                .Subscribe(x =>
                    {
                        if(model.AllDmxChannels.Value.Count == 0) return;
                        
                        // universe最大値を取得
                        var group = model.AllDmxChannels.Value.GroupBy(x => x.Universe).ToDictionary(g => g.Key, g => g.ToList());;
                        foreach (var (universe, channels) in group)
                        {
                            var buffer = new byte[512];
                            foreach (var c in channels)
                            {
                                buffer[c.ChannelIndex + c.ChannelOffset] = (byte) c.ChannelValue;
                            }
                            _sender.SendUniverse(IPAddress.Loopback, 6454, (ushort)universe, buffer);
                        }
                    }
                ).AddTo(this);

            yield return _timer;
        }

    }

    // Fixture Preset
    public partial class SenderPresenter
    {

        [SerializeField] private FixtureManufacturerListUI _fixtureManufacturerListUI;
        [SerializeField] private FixturePresetListUI _fixturePresetListUI;

        public IObservable<Guid> OnFixtureManufacturerSelected => _fixtureManufacturerListUI.OnComponentSelected;

        public IObservable<Guid> OnFixturePresetSelected => _fixturePresetListUI.OnComponentSelected;
        public IObservable<Guid> OnEditButtonClicked => _fixturePresetListUI.OnComponentEditButtonClicked;

        private IEnumerable<IDisposable> BindFixturePresetList(SenderModel model)
        {
            yield return model.FixtureManufacturerList.Subscribe(manufacturerList =>
            {
                _fixtureManufacturerListUI.Initialize(manufacturerList);
            });
            
            yield return model.SelectedFixtureManufacturerEntity.Subscribe(selectedData =>
            {
                _fixtureManufacturerListUI.MarkAsSelected(selectedData?.Guid);
            });
            
            
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

    // Placement
    public partial class SenderPresenter
    {
        
        [SerializeField] private FixturePlacementListUI _fixturePlacementListUI;
        public IObservable<Guid> OnFixturePlacementSelected => _fixturePlacementListUI.OnComponentSelected;
        public IObservable<IEnumerable<ReorderableEntity>> OnFixturePlacementOrderChanged => _fixturePlacementListUI.OnListOrderChanged;
        
        
        [SerializeField] private UniverseSelectionUI _universeSelectionUI;
        public IObservable<int> OnUniverseInputChanged => _universeSelectionUI.OnUniverseInputFieldValueChanged;
        public IObservable<Unit> OnIncrementUniverseButtonClicked => _universeSelectionUI.OnIncrementButtonClicked;
        public IObservable<Unit> OnDecrementUniverseButtonClicked => _universeSelectionUI.OnDecrementButtonClicked;
        
        public IObservable<(Guid, int)> OnChannelOffsetChanged => _fixturePlacementListUI.OnChannelOffsetChanged;

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
            
            yield return model.SelectedFixturePlacementEntity.Subscribe(selectedData =>
            {
                if (selectedData == null) return;
                _fixturePlacementListUI.SetChannelOffset(selectedData.Guid, selectedData.ChannelOffset);
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
        [SerializeField] private AddChannelUI _addChannelUI;
        public IObservable<Guid> OnDmxChannelSelected => _dmxChannelListUI.OnComponentSelected;
        public IObservable<IEnumerable<ReorderableEntity>> OnDmxChannelOrderChanged => _dmxChannelListUI.OnListOrderChanged;

        public IObservable<(Guid, int)> OnChannelIndexChanged => _dmxChannelListUI.OnChannelIndexChanged;
        public IObservable<(Guid, int)> OnChannelValueChanged => _dmxChannelListUI.OnChannelValueChanged;
        public IObservable<Guid> OnDmxChannelDeleted => _dmxChannelListUI.OnComponentDeleted;

        public IObservable<string> OnAddChannelButtonClicked => _addChannelUI.OnAddChannelButtonClicked;
        
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

            yield return model.SelectedDmxChannelEntity.Subscribe(selectedData =>
            {
                if (selectedData == null) return;
                _dmxChannelListUI.SetChannelValue(selectedData.Guid, selectedData.ChannelValue);
            });
            
            yield return model.SelectedDmxChannelEntity.Subscribe(selectedData =>
            {
                if (selectedData == null) return;
                _dmxChannelListUI.SetChannelIndex(selectedData.Guid, selectedData.ChannelIndex);
            });

        }
    }

    
}

