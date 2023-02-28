using System;
using System.Collections;
using System.Collections.Generic;
using com.kodai100.ArtNetApp.View;
using inc.stu.DmxRecorder;
using inc.stu.SyncArena;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace com.kodai100.ArtNetApp.Presenter
{
    public class RecorderPresenter : Presenter<RecorderModel>
    {
        
        [SerializeField] private RecorderUI _recorderUI;
        [SerializeField] private IntInputField _portInputField;
        
        private ArtNetRecorder.ArtNetRecorder _artNetRecorder;

        public IObservable<Unit> RecordButtonToggled => _recorderUI.RecordButton.OnClickAsObservable.Select(_ => Unit.Default);
        public IObservable<int> OnPortValueChanged => _portInputField.OnValueChanged;
        
        public override IEnumerable<IDisposable> Bind(RecorderModel model)
        {
            
            _recorderUI.Initialize();


            yield return model.IsRecording.Subscribe(isRecording =>
            {

                if (isRecording)
                {
                    _recorderUI.RecordButton.Press();
                    Logger.Log("Recording...");
                    _recorderUI.TimeCodeText.color = Color.red;
                    _artNetRecorder?.RecordStart();
                }
                else
                {
                    _recorderUI.RecordButton.Release();
                    _artNetRecorder?.RecordEnd();

                    _recorderUI.TimeCodeText.color = Color.white;
                }
                
            });

            yield return model.ReceivePort.Subscribe(port =>
            {
                _portInputField.SetValueWithoutNotify(port);
            });

            yield return model.ReceivePort.Subscribe(port =>
            {
                _artNetRecorder?.Dispose();
                _artNetRecorder = null;
                
                _artNetRecorder = new ArtNetRecorder.ArtNetRecorder(port);
            
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
                    if (!UnityEngine.Application.isPlaying) return;
                
                    var size = BytesCalculator.GetBytesReadable(result.Size);

                    Logger.Log($"Saved - Packets: {result.PacketNum}, DataSize: {size} : {result.DataPath}");
                };
            });

        }

        public void OnDestroy()
        {
            _artNetRecorder?.Dispose();
            _artNetRecorder = null;
        }
        
    }
}

