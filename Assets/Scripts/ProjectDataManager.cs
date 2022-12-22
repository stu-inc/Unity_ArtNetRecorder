using UniRx;

public class ProjectDataManager
{

    public ReactiveProperty<int> ArtNetReceivePort = new(6454);

    public ReactiveProperty<string> ArtNetSendIp = new("127.0.0.1");
    public ReactiveProperty<int> ArtNetSendPort = new(6454);

}
