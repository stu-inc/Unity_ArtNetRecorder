using com.kodai100.ArtNetApp.Presenter;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.Application
{
    public class ArtNetPlayerApplication : ApplicationBase<PlayerModel, PlayerPresenter>
    {

        public override void OnOpen(ProjectDataManager projectDataManager)
        {
            base.OnOpen(projectDataManager);

            _model = new PlayerModel(projectDataManager);
        
            _disposables.AddRange(_presenterInstance.Bind(_model));
        
            SetupPresenter(_model);
        
            // Logger.Log("Changed to ArtNet Sender");
        }

        private void SetupPresenter(PlayerModel model)
        {

            _presenterInstance.OnPlayButtonPressed.Subscribe(_ =>
            {
                model.ToggleIsPlaying();
            }).AddTo(_disposables);

            _presenterInstance.OnDmxFileNameChanged.Subscribe(filePath =>
            {
                model.SetDmxFilePath(filePath);
            }).AddTo(_disposables);

            _presenterInstance.OnAudioFileNameChanged.Subscribe(filePath =>
            {
                model.SetSoundFilePath(filePath);
            }).AddTo(_disposables);


            _presenterInstance.OnToggleIsSending.Subscribe(_ =>
            {
                model.ToggleIsSending();
            }).AddTo(_disposables);
        
            _presenterInstance.OnIpAddressChanged.Subscribe(ip =>
            {
                model.ChangeIp(ip);
            }).AddTo(_disposables);
        
            _presenterInstance.OnPortChanged.Subscribe(port =>
            {
                model.ChangePort(port);
            }).AddTo(_disposables);


            _presenterInstance.OnEndOfTimeline.Subscribe(_ =>
            {
                model.StopPlaying();
            }).AddTo(_disposables);

            _presenterInstance.OnLoadingStateChanged.Subscribe(isLoading =>
            {
                model.ChangeIsLoading(isLoading);
            }).AddTo(_disposables);

        }

    }

}

