using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

public class AudioPlayer : IDisposable
{

    private AudioSource _audioSource;

    private Subject<bool> _onLoadingStateChanged = new();
    public IObservable<bool> OnLoadingStateChanged => _onLoadingStateChanged;

    public AudioPlayer()
    {
        var obj = new GameObject();
        _audioSource = obj.AddComponent<AudioSource>();

        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
    }

    public async void Play(double delayMillisec)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(delayMillisec));

        _audioSource.Play();
    }

    public void Resume(float positionMillisec)
    {
        _audioSource.time = positionMillisec * 0.001f;
        _audioSource.Play();
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public async UniTaskVoid LoadClipFromPath(string path)
    {
        var url = $"file://{path}";

        var r = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);

        _onLoadingStateChanged.OnNext(true);
        await r.SendWebRequest();
        _onLoadingStateChanged.OnNext(false);

        if (r.result == UnityWebRequest.Result.Success)
        {
            _audioSource.clip = DownloadHandlerAudioClip.GetContent(r);
        }
    }

    public void Dispose()
    {
        _onLoadingStateChanged.Dispose();

        Object.Destroy(_audioSource.gameObject);
    }

}
