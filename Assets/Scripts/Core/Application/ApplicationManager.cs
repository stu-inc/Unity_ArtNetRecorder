using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private Tab _applicationTab;

    [SerializeField] private List<App> _applications;

    private ProjectDataManager _projectDataManager = new();

    private void Start()
    {
        _applicationTab.OnSelected.Subscribe(ApplicationChanged).AddTo(this);
    }
    
    private void ApplicationChanged(int index)
    {
        
        _applications.ForEach(app =>
        {
            app.OnClose();
        });

        if (index < _applications.Count)
        {
            _applications[index]?.OnOpen(_projectDataManager);
        }

    }

    public void OnDestroy()
    {
        _applications.ForEach(app =>
        {
            app.OnClose();
        });
    }

}
