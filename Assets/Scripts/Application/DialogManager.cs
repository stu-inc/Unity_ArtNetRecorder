using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DialogManager : SingletonMonoBehaviour<DialogManager>
{

    [SerializeField] private DialogUI dialogUiPrefab;

    [SerializeField] private RectTransform dialogParentTransform;

    
    private static SynchronizationContext synchronizationContext;

    private void Awake()
    {
        synchronizationContext = SynchronizationContext.Current;
    }
    
    public static async UniTask<bool> OpenInfo(string message)
    {
        var instance = InstantiateDialog();
        return await instance.OpenInfo(message);
    }

    public static async UniTask<bool> OpenError(string message)
    {
        var instance = InstantiateDialog();
        return await instance.OpenError(message);
    }

    private static DialogUI InstantiateDialog()
    {
        var instance = Instantiate(Instance.dialogUiPrefab, Instance.dialogParentTransform);
        return instance;
    }
    
    public static UniTask<bool> OpenErrorThreadSafe(string message)
    {
        synchronizationContext.Post(__ =>
        {
            OpenError(message).Forget();
        }, null);

        return UniTask.FromResult(true);
    }

}
