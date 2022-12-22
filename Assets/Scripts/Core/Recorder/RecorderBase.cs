using System;

namespace ProjectBlue.ArtNetRecorder
{

    public struct SaveResult
    {
        public string DataPath;
        public long PacketNum;
        public long Size;
    }

    public abstract class RecorderBase : IDisposable
    {
        public Action<double> OnUpdateTime;
        public Action<(int, int, int)> OnIndicatorUpdate;
        public Action<SaveResult> OnSaved;
        
        public int Port { get; private set; }
        
        public bool IsRecording { get; protected set; }

        public RecorderBase(int port)
        {
            ChangePort(port);
        }

        public void ChangePort(int port)
        {
            Port = port;
            OnChangePort(port);
        }

        protected abstract void OnChangePort(int port);
        public abstract void RecordStart();
        public abstract void RecordEnd();

        public virtual void Dispose()
        {
            OnUpdateTime = null;
            OnIndicatorUpdate = null;
            OnSaved = null;
        }
    }
    
}
