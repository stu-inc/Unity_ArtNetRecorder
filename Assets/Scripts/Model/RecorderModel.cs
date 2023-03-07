using System;
using System.Collections.Generic;
using UniRx;


namespace com.kodai100.ArtNetApp.Models
{

    public abstract class Model : IDisposable
    {

        protected ProjectDataManager _projectDataManager;
        
        protected List<IDisposable> _disposables = new();

        public Model(ProjectDataManager projectDataManager)
        {
            _projectDataManager = projectDataManager;
        }

        public virtual void Dispose()
        {
            _disposables.ForEach(d =>
            {
                d.Dispose();
            });
        }
    }

    public class RecorderModel : Model
    {

        private ReactiveProperty<int> _receivePort;
        public IReadOnlyReactiveProperty<int> ReceivePort => _receivePort;

        private ReactiveProperty<bool> _isRecording = new(false);
        public IReadOnlyReactiveProperty<bool> IsRecording => _isRecording;



        public RecorderModel(ProjectDataManager projectDataManager) : base(projectDataManager)
        {
            _receivePort = projectDataManager.ArtNetReceivePort;
        }

        public void ChangePort(int port)
        {
            _receivePort.Value = port;
        }

        public void SetRecording(bool isRecording)
        {
            _isRecording.Value = isRecording;
        }



    }

}