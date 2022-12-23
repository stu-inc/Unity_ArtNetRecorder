using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UniRx;
using UnityEngine;

public class PlayerModel : Model
{

    private readonly ReactiveProperty<IPAddress> _ipAddress;
    public IReadOnlyReactiveProperty<IPAddress> IpAddress => _ipAddress;

    private readonly ReactiveProperty<int> _port;
    public IReadOnlyReactiveProperty<int> Port => _port;
    
    private readonly ReactiveProperty<bool> _isPlaying = new(false);
    public IReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;


    private readonly ReactiveProperty<bool> _isSending = new(false);
    public IReadOnlyReactiveProperty<bool> IsSending => _isSending;

    private readonly ReactiveProperty<string> _dmxFilePath = new(null);
    public IReadOnlyReactiveProperty<string> DmxFilePath => _dmxFilePath;

    private readonly ReactiveProperty<string> _soundFilePath = new(null);
    public IReadOnlyReactiveProperty<string> SoundFilePath => _soundFilePath;

    public PlayerModel(ProjectDataManager projectDataManager) : base(projectDataManager)
    {

        _ipAddress = projectDataManager.ArtNetSendIp;
        _port = projectDataManager.ArtNetSendPort;

    }

    public void ChangeIp(IPAddress ip)
    {
        _ipAddress.Value = ip;
    }

    public void ChangePort(int port)
    {
        _port.Value = port;
    }

    public void ToggleIsPlaying()
    {
        _isPlaying.Value = !_isPlaying.Value;
    }

    public void ToggleIsSending()
    {
        _isSending.Value = !_isSending.Value;
    }
    
    public void SetDmxFilePath(string filePath)
    {
        _isPlaying.Value = false;
        _dmxFilePath.Value = File.Exists(filePath) ? filePath : null;
    }
    
    public void SetSoundFilePath(string filePath)
    {
        _isPlaying.Value = false;
        _soundFilePath.Value = File.Exists(filePath) ? filePath : null;
    }

    public void StopPlaying()
    {
        _isPlaying.Value = false;
    }
    
}
