using System;
using System.Collections.Generic;
using com.kodai100.ArtNetApp.Models;
using com.kodai100.ArtNetApp.Presenter;
using UnityEngine;

public abstract class App : MonoBehaviour
{
    public abstract void OnClose();
    public abstract void OnOpen(ProjectDataManager projectDataManager);
}

public abstract class ApplicationBase<TModel, TPresenter> : App where TModel : Model where TPresenter : Presenter<TModel>
{
    
    protected TModel _model;

    [SerializeField] protected RectTransform _presenterParentTransform;
    [SerializeField] protected TPresenter _presenterPrefab;

    protected TPresenter _presenterInstance;

    protected List<IDisposable> _disposables = new();

    private void Awake()
    {
        foreach (Transform child in _presenterParentTransform)
        {
            Destroy(child.gameObject);
        }
    }
    
    public override void OnClose()
    {
        OnDestroy();
    }

    public override void OnOpen(ProjectDataManager projectDataManager)
    {
        _presenterInstance = Instantiate(_presenterPrefab, _presenterParentTransform);
    }
    
    protected void OnDestroy()
    {
        _model?.Dispose();
        
        _disposables.ForEach(disposable =>
        {
            disposable.Dispose();
        });

        if (_presenterInstance != null)
        {
            DestroyImmediate(_presenterInstance.gameObject);
        }
        
    }

}
