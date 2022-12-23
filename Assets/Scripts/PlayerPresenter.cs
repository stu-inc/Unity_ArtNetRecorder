using System;
using System.Collections.Generic;
using System.Net;
using UniRx;
using UnityEngine;

public class PlayerPresenter : Presenter<PlayerModel>
{

    private const int MaxUniverseNum = 32;
    
    [SerializeField] private DataVisualizer visualizer;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private FileDialogUI dmxFileDialogUI;
    [SerializeField] private FileDialogUI audioDialogUI;
    
    [SerializeField] private LoadingUI loadingUI;

    [SerializeField] private ArtNetResendUI artNetResendUI;
    
    private bool initialized;
    
    private double header;
    private double endTime;

    private ArtNetPlayer _artNetPlayer;
    private AudioPlayer _audioPlayer;
    
    private Subject<Unit> _onEndOfTimeline = new();
    public IObservable<Unit> OnEndOfTimeline => _onEndOfTimeline;

    public IObservable<Unit> OnPlayButtonPressed => playerUI.OnPlayButtonPressedAsObservable;
    public IObservable<string> OnDmxFileNameChanged => dmxFileDialogUI.OnFileNameChanged;
    public IObservable<string> OnAudioFileNameChanged => audioDialogUI.OnFileNameChanged;

    public IObservable<Unit> OnToggleIsSending => artNetResendUI.IsEnabled.Select(x => Unit.Default);
    public IObservable<IPAddress> OnIpAddressChanged => artNetResendUI.OnIpChanged;
    public IObservable<int> OnPortChanged => artNetResendUI.OnPortChanged;
    public IObservable<bool> OnLoadingStateChanged => Observable.Merge(_artNetPlayer.OnLoadingStateChanged, _audioPlayer.OnLoadingStateChanged);

    public override IEnumerable<IDisposable> Bind(PlayerModel model)
    {

        _artNetPlayer = new ArtNetPlayer(MaxUniverseNum);
        _audioPlayer = new AudioPlayer();
        
        yield return model.IsLoading.Subscribe(isLoading =>
        {
            if(isLoading)
                loadingUI.Show();
            else
                loadingUI.Hide();
        });
        
        yield return model.IsPlaying.Subscribe(isPlaying =>
        {
            if (!isPlaying)
                Pause();
            else
                Resume();
        });

        yield return model.DmxFilePath.Subscribe(filePath =>
        {
            dmxFileDialogUI.SetValueWithoutNotify(filePath);
        });
        
        yield return model.DmxFilePath.Subscribe(filePath =>
        {
            if (string.IsNullOrEmpty(filePath)) return;
            // TODO: 再生中の読み込みブロックはそもそもFileDialogUI側の入力不可で制限する
            if(!model.IsPlaying.Value)
                Initialize(filePath);
        });

        yield return model.SoundFilePath.Subscribe(filePath =>
        {
            audioDialogUI.SetValueWithoutNotify(filePath);
        });
        
        yield return model.SoundFilePath.Subscribe(filePath =>
        {
            if (string.IsNullOrEmpty(filePath)) return;
            _audioPlayer.LoadClipFromPath(filePath).Forget();
        });

        yield return Observable.EveryUpdate().Where(_ => initialized && model.IsPlaying.Value).Subscribe(_ =>
        {
            if (header > endTime)
            {
                _onEndOfTimeline.OnNext(Unit.Default);
            }
        
            header += Time.deltaTime * 1000;    // millisec

            visualizer.Exec(_artNetPlayer.ReadAndSend(header, model.IsSending.Value));

            playerUI.SetHeader(header);
        });


        yield return model.IsSending.Subscribe(isSend =>
        {
            artNetResendUI.SetToggleWithoutNotify(isSend);
        });

        yield return model.IpAddress.Subscribe(address =>
        {
            artNetResendUI.SetIpWithoutNotify(address);
            _artNetPlayer.SetIp(address);
        });

        yield return model.Port.Subscribe(port =>
        {
            artNetResendUI.SetPortWithoutNotify(port);
            _artNetPlayer.SetPort(port);
        });

    }
    
    private async void Initialize(string path)
    {

        initialized = false;
        
        // read file
        loadingUI.Show();
        
        var data = await _artNetPlayer.Load(path);

        loadingUI.Hide();
        
        endTime = data.Duration;
        
        // initialize visualizer
        // TODO: 今後ファイルに使用Universe数を格納するようにする。
        visualizer.Initialize(MaxUniverseNum);
        
        // initialize player
        playerUI.Initialize(endTime);
        header = 0;
        playerUI.SetHeader(header);
        playerUI.SetAsPlayVisual();

        initialized = true;
    }
    
    public void Resume()
    {
        if (!initialized) return;
        
        // ここでヘッダ読んでくる
        header = playerUI.GetSliderPosition() * endTime;
        endTime = _artNetPlayer.GetDuration();
        
        playerUI.SetAsPauseVisual();
        
        _audioPlayer?.Resume((float)header);
    }

    public void Pause()
    {
        playerUI.SetAsPlayVisual();
        _audioPlayer?.Pause();
    }

    public override void Dispose()
    {
        
        visualizer.Dispose();
        Pause();
        
        base.Dispose();
    }
}
