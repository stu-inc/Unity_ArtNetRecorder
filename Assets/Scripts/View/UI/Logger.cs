using System.Threading;
using UnityEngine;


// Thread safe
public class Logger : SingletonMonoBehaviour<Logger>
{

    [SerializeField] private StatusTextUI statusTextUI;

    
    private static SynchronizationContext synchronizationContext;
    
    private void Awake()
    {
        synchronizationContext = SynchronizationContext.Current;
    }

    public static void Log(string message)
    {
        Debug.Log(message);
        
        synchronizationContext.Post(__ =>
        {
            Instance.statusTextUI.Log(message);
        }, null);
        
        
    }

    public static void Error(string message)
    {
        Debug.LogError(message);
        
        synchronizationContext.Post(__ =>
        {
            Instance.statusTextUI.Error(message);
        }, null);
    }
    

}
