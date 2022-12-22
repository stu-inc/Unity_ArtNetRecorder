using System;
using inc.stu.DmxRecorder;
using UniRx;
using ProjectBlue.ArtNetRecorder;
using UnityEngine;

public class DmxRecorderApplication : ApplicationBase<RecorderModel, RecorderPresenter>
{

    private void SetupPresenter(RecorderModel model)
    {
        
        _presenter.RecordButtonToggled.Subscribe(_ =>
        {
            model.SetRecording(!model.IsRecording.Value);
        }).AddTo(_disposables);
        
    }

    public override void OnOpen(ProjectDataManager projectDataManager)
    {
        // CreateModel
        _model = new RecorderModel(projectDataManager);
        
        _disposables.AddRange(_presenter.Bind(_model));
        
        SetupPresenter(_model);
        Logger.Log("Changed to ArtNet Recorder");
    }


    

}
