using UniRx;

public class DmxRecorderApplication : ApplicationBase<RecorderModel, RecorderPresenter>
{

    private void SetupPresenter(RecorderModel model)
    {
        
        _presenter.RecordButtonToggled.Subscribe(_ =>
        {
            model.SetRecording(!model.IsRecording.Value);
        }).AddTo(_disposables);

        _presenter.OnPortValueChanged.Subscribe(port =>
        {
            model.ChangePort(port);
        }).AddTo(_disposables);

    }

    public override void OnOpen(ProjectDataManager projectDataManager)
    {
        base.OnOpen(projectDataManager);
        
        // CreateModel
        _model = new RecorderModel(projectDataManager);
        
        _disposables.AddRange(_presenter.Bind(_model));
        
        SetupPresenter(_model);
        Logger.Log("Changed to ArtNet Recorder");
    }
    
}
