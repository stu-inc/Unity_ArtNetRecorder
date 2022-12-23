using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class App : MonoBehaviour
{
    public abstract void OnClose();
    public abstract void OnOpen(ProjectDataManager projectDataManager);
}

public abstract class ApplicationBase<TModel, TPresenter> : App where TModel : Model where TPresenter : Presenter<TModel>
{
    
    protected TModel _model;
   
    [SerializeField] protected TPresenter _presenter;

    protected List<IDisposable> _disposables = new();

    public override void OnClose()
    {
        OnDestroy();
    }

    public override void OnOpen(ProjectDataManager projectDataManager)
    {
        _presenter?.Initialize();
    }
    
    protected void OnDestroy()
    {
        _model?.Dispose();
        
        _disposables.ForEach(disposable =>
        {
            disposable.Dispose();
        });
        
        _presenter?.Dispose();
    }

}
