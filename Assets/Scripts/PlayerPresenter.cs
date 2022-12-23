using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerPresenter : Presenter<PlayerModel>
{
    
    [SerializeField] private DataVisualizer visualizer;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private FileDialogUI dmxFileDialogUI;
    [SerializeField] private FileDialogUI audioDialogUI;
    [SerializeField] private AudioPlayer audioPlayer;
    [SerializeField] private ArtNetPlayer artNetPlayer;
    [SerializeField] private LoadingUI loadingUI;

    
    private bool initialized;
    
    private double header;
    private double endTime;
    
    public IObservable<Unit> OnPlayButtonPressed => playerUI.OnPlayButtonPressedAsObservable;
    public IObservable<string> OnDmxFileNameChanged => dmxFileDialogUI.OnFileNameChanged;
    public IObservable<string> OnAudioFileNameChanged => audioDialogUI.OnFileNameChanged;

    public override IEnumerable<IDisposable> Bind(PlayerModel model)
    {

        loadingUI.Hide();
        
        yield return model.IsPlaying.Subscribe(isPlaying =>
        {
            if (isPlaying)
                Pause();
            else
                Resume();
        });

        yield return model.DmxFilePath.Subscribe(filePath =>
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                Initialize(filePath);
            }
        });

        yield return model.SoundFilePath.Subscribe(filePath =>
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                audioPlayer.LoadClipFromPath(filePath).Forget();
            }
        });

        yield return Observable.EveryUpdate().Where(_ => initialized && model.IsPlaying.Value).Subscribe(_ =>
        {
            if (header > endTime)
            {
                Pause();
            }
        
            header += Time.deltaTime * 1000;    // millisec

            visualizer.Exec(artNetPlayer.ReadAndSend(header));

            playerUI.SetHeader(header);
        });

        yield return model.IpAddress.Subscribe(address =>
        {
            
        });

    }
    
    private async void Initialize(string path)
    {

        initialized = false;
        
        // read file
        loadingUI.Display();
        
        var data = await artNetPlayer.Load(path);

        loadingUI.Hide();
        
        endTime = data.Duration;
        
        // initialize visualizer
        // TODO: 今後ファイルに使用Universe数を格納するようにする。
        const int maxUniverseNum = 32;
        visualizer.Initialize(maxUniverseNum);
        
        // initialize player
        playerUI.Initialize(endTime);
        playerUI.SetAsPlayVisual();
    
        // initialize buffers
        artNetPlayer.Initialize(maxUniverseNum);

        initialized = true;
    }
    
    public void Resume()
    {
        if (!initialized) return;
        
        // ここでヘッダ読んでくる
        header = playerUI.GetSliderPosition() * endTime;
        endTime = artNetPlayer.GetDuration();
        
        playerUI.SetAsPauseVisual();
        
        audioPlayer.Resume(2566.667f + (float)header);
    }

    public void Pause()
    {
        playerUI.SetAsPlayVisual();
        audioPlayer.Pause();
    }

    public override void Dispose()
    {
        base.Dispose();
        
        Pause();
    }
}
