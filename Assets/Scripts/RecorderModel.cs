using System;
using UniRx;


public abstract class Model : IDisposable
{
    public virtual void Dispose()
    {
        
    }
}

public class RecorderModel : Model
{

    private ProjectDataManager _projectDataManager;

    private ReactiveProperty<int> _receivePort;
    public IReadOnlyReactiveProperty<int> ReceivePort => _receivePort;

    private ReactiveProperty<bool> _isRecording = new(false);
    public IReadOnlyReactiveProperty<bool> IsRecording => _isRecording;

    public RecorderModel(ProjectDataManager projectDataManager)
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
