using com.kodai100.ArtNetApp.Models;
using com.kodai100.ArtNetApp.Presenter;
using UniRx;

namespace com.kodai100.ArtNetApp.Application
{
    public class ArtNetSenderApplication : ApplicationBase<SenderModel, SenderPresenter>
    {
            
            public override void OnOpen(ProjectDataManager projectDataManager)
            {
                base.OnOpen(projectDataManager);
    
                _model = new SenderModel(projectDataManager);
            
                _disposables.AddRange(_presenterInstance.Bind(_model));
            
                SetupPresenter(_model);
            
                Logger.Log("Changed to ArtNet Sender");
                
                // TODO: Load data
            }

            private void SetupPresenter(SenderModel model)
            {

                SetupFixturePresetPresenter(model);
                SetupFixturePlacementPresenter(model);
                SetupDmxChannelPresenter(model);
            }

            private void SetupFixturePresetPresenter(SenderModel model)
            {

                _presenterInstance.OnFixtureManufacturerSelected.Subscribe(guid =>
                {
                    model.UpdateManufacturerSelection(guid);
                }).AddTo(_disposables);
                
                _presenterInstance.OnFixturePresetSelected.Subscribe(guid =>
                {
                    model.PlaceFixtureFromPreset(guid);  // 選択するとインスタンス化を想定しているので、記録はしない
                }).AddTo(_disposables);

                _presenterInstance.OnEditButtonClicked.Subscribe(guid =>
                {
                    model.UpdateFixturePresetSelection(guid);
                }).AddTo(_disposables);

            }

            private void SetupFixturePlacementPresenter(SenderModel model)
            {
                _presenterInstance.OnFixturePlacementSelected.Subscribe(guid =>
                {
                    model.UpdateFixturePlacementSelection(guid);
                }).AddTo(_disposables);
                
                _presenterInstance.OnFixturePlacementOrderChanged.Subscribe(list =>
                {
                    model.UpdateFixturePlacementOrder(list);
                }).AddTo(_disposables);
                
                
                // Universe Selection
                _presenterInstance.OnUniverseInputChanged.Subscribe(universe =>
                {
                    model.UpdateUniverse(universe);
                }).AddTo(_disposables);

                _presenterInstance.OnIncrementUniverseButtonClicked.Subscribe(_ =>
                {
                    model.IncrementUniverse();
                }).AddTo(_disposables);
                
                _presenterInstance.OnDecrementUniverseButtonClicked.Subscribe(_ =>
                {
                    model.DecrementUniverse();
                }).AddTo(_disposables);
            }
            
            private void SetupDmxChannelPresenter(SenderModel model)
            {
                _presenterInstance.OnDmxChannelSelected.Subscribe(guid =>
                {
                    model.UpdateDmxChannelSelection(guid);
                }).AddTo(_disposables);

                _presenterInstance.OnDmxChannelDeleted.Subscribe(guid =>
                {
                    model.RemoveDmxChannelData(guid);
                }).AddTo(_disposables);
                
                _presenterInstance.OnDmxChannelOrderChanged.Subscribe(list =>
                {
                    model.UpdateDmxChannelOrder(list);
                }).AddTo(_disposables);


                _presenterInstance.OnAddChannelButtonClicked.Subscribe(channelName =>
                {
                    model.AddDmxChannelData(channelName);
                }).AddTo(_disposables);
            }
            
    }

}

