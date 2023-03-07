using System.IO;
using System.Net;
using UniRx;

namespace com.kodai100.ArtNetApp.Models
{
    
    public class PlayerModel : Model
    {

        private readonly ReactiveProperty<IPAddress> _ipAddress;
        private readonly ReactiveProperty<int> _port;
        
        private readonly ReactiveProperty<bool> _isPlaying = new(false);
        private readonly ReactiveProperty<bool> _isSending = new(false);
        
        private readonly ReactiveProperty<string> _dmxFilePath = new(null);
        private readonly ReactiveProperty<string> _soundFilePath = new(null);

        private readonly ReactiveProperty<bool> _isLoading = new(false);

        public IReadOnlyReactiveProperty<IPAddress> IpAddress => _ipAddress;
        public IReadOnlyReactiveProperty<int> Port => _port;
        public IReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;
        public IReadOnlyReactiveProperty<bool> IsSending => _isSending;
        public IReadOnlyReactiveProperty<string> DmxFilePath => _dmxFilePath;
        public IReadOnlyReactiveProperty<string> SoundFilePath => _soundFilePath;
        public IReadOnlyReactiveProperty<bool> IsLoading => _isLoading;

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

        public void ChangeIsLoading(bool isLoading)
        {
            _isLoading.Value = isLoading;
        }

        public override void Dispose()
        {
            base.Dispose();

            _dmxFilePath.Value = null;
            _soundFilePath.Value = null;
        }
    }

}
