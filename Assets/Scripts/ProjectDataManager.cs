using System.Net;
using UniRx;

public class ProjectDataManager
{

    public ReactiveProperty<int> ArtNetReceivePort = new(6454);

    public ReactiveProperty<IPAddress> ArtNetSendIp = new(IPAddress.Parse("127.0.0.1"));
    public ReactiveProperty<int> ArtNetSendPort = new(6454);

}
