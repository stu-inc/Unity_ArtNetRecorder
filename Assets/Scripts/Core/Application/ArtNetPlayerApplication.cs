using UniRx;

public class ArtNetPlayerApplication : ApplicationBase<PlayerModel, PlayerPresenter>
{

    public override void OnOpen(ProjectDataManager projectDataManager)
    {
        // throw new NotImplementedException();

        _model = new PlayerModel(projectDataManager);
        
        _disposables.AddRange(_presenter.Bind(_model));
        
        SetupPresenter(_model);
        
        Logger.Log("Changed to ArtNet Sender");
    }

    private void SetupPresenter(PlayerModel model)
    {

        _presenter.OnPlayButtonPressed.Subscribe(_ =>
        {
            model.ToggleIsPlaying();
        }).AddTo(_disposables);

        _presenter.OnDmxFileNameChanged.Subscribe(filePath =>
        {
            model.SetDmxFilePath(filePath);
        }).AddTo(_disposables);

        _presenter.OnAudioFileNameChanged.Subscribe(filePath =>
        {
            model.SetSoundFilePath(filePath);
        }).AddTo(_disposables);


        _presenter.OnToggleIsSending.Subscribe(_ =>
        {
            model.ToggleIsSending();
        }).AddTo(_disposables);
        
        _presenter.OnIpAddressChanged.Subscribe(ip =>
        {
            model.ChangeIp(ip);
        }).AddTo(_disposables);
        
        _presenter.OnPortChanged.Subscribe(port =>
        {
            model.ChangePort(port);
        }).AddTo(_disposables);


        _presenter.OnEndOfTimeline.Subscribe(_ =>
        {
            model.StopPlaying();
        }).AddTo(_disposables);

    }

}
