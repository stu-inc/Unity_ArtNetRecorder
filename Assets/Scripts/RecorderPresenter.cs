using System;
using System.Collections;
using System.Collections.Generic;
using inc.stu.DmxRecorder;
using ProjectBlue.ArtNetRecorder;
using UniRx;
using UnityEngine;


public abstract class Presenter<T> : MonoBehaviour, IDisposable where T : Model
{
    public abstract IEnumerable<IDisposable> Bind(T model);

    public virtual void Dispose()
    {
        
    }
}

public class RecorderPresenter : Presenter<RecorderModel>
{
    
    [SerializeField] private RecorderUI _recorderUI;
    
    private ArtNetRecorder _artNetRecorder;

    public IObservable<Unit> RecordButtonToggled => _recorderUI.RecordButton.Button.OnClickAsObservable();
    
    public override IEnumerable<IDisposable> Bind(RecorderModel model)
    {
        
        _recorderUI.Initialize();


        yield return model.IsRecording.Subscribe(isRecording =>
        {

            if (isRecording)
            {
                _recorderUI.RecordButton.SetRecord();
                Logger.Log("Recording...");
                _recorderUI.TimeCodeText.color = Color.red;
                _artNetRecorder?.RecordStart();
            }
            else
            {
                _recorderUI.RecordButton.SetStop();
                _artNetRecorder?.RecordEnd();

                _recorderUI.TimeCodeText.color = Color.white;
            }
            
        });
        

        SetupArtNetRecorder();
        
    }

    private void SetupArtNetRecorder()
    {
        _artNetRecorder = new ArtNetRecorder(6454);
        
        _artNetRecorder.OnIndicatorUpdate = tuple =>
        {
            _recorderUI.IndicatorUI.SetScale(tuple.Item2);
            _recorderUI.IndicatorUI.Set(tuple.Item1, tuple.Item3);
        };

        _artNetRecorder.OnUpdateTime = (ms) =>
        {
            var t = TimeSpan.FromMilliseconds(ms);
            _recorderUI.TimeCodeText.text = $"{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2};{t.Milliseconds:D3}";
        };

        _artNetRecorder.OnSaved = (result) =>
        {
            // Record中にQuitするとTextがDestroy済なのにアクセスしてしまうのを防止
            if (!Application.isPlaying) return;
            
            var size = BytesCalculator.GetBytesReadable(result.Size);

            Logger.Log($"Saved - Packets: {result.PacketNum}, DataSize: {size} : {result.DataPath}");
        };
    }

    public override void Dispose()
    {
        base.Dispose();
        
        _artNetRecorder?.Dispose();
        _artNetRecorder = null;
    }
    
}
