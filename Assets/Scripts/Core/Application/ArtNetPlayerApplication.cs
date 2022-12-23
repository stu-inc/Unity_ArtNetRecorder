using UniRx;

public class ArtNetPlayerApplication : ApplicationBase<PlayerModel, PlayerPresenter>
{

    public override void OnOpen(ProjectDataManager projectDataManager)
    {
        // throw new NotImplementedException();

        _model = new PlayerModel(projectDataManager);
        
        SetupPresenter(_model);
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

    }

}
