using com.kodai100.ArtNetApp.Models;
using com.kodai100.ArtNetApp.Presenter;
using UniRx;

namespace com.kodai100.ArtNetApp.Application
{
    public class DmxRecorderApplication : ApplicationBase<RecorderModel, RecorderPresenter>
    {

        public override void OnOpen(ProjectDataManager projectDataManager)
        {
            base.OnOpen(projectDataManager);
        
            // CreateModel
            _model = new RecorderModel(projectDataManager);
        
            _disposables.AddRange(_presenterInstance.Bind(_model));
        
            SetupPresenter(_model);
            Logger.Log("Changed to ArtNet Recorder");
        }
        
        private void SetupPresenter(RecorderModel model)
        {
        
            _presenterInstance.RecordButtonToggled.Subscribe(_ =>
            {
                model.SetRecording(!model.IsRecording.Value);
            }).AddTo(_disposables);

            _presenterInstance.OnPortValueChanged.Subscribe(port =>
            {
                model.ChangePort(port);
            }).AddTo(_disposables);

        }

    }

}

