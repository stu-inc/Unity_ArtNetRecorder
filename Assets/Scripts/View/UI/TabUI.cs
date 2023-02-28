using System;
using System.Collections.Generic;
using com.kodai100.ArtNetApp.View;
using UniRx;
using UnityEngine;

public class TabUI : MonoBehaviour
{

    [SerializeField] private ToggleButtonWithLabelAndBackgroundUI _tabButtonPrefab;
    [SerializeField] private RectTransform _tabButtonParent;

    private Dictionary<Guid, ToggleButtonWithLabelAndBackgroundUI> _tabButtonDictionary = new();

    private List<IDisposable> _disposables = new();
    
    private readonly Subject<Guid> _onSelectedSubject = new();
    public IObservable<Guid> OnSelected => _onSelectedSubject;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    public void Initialize(IReadOnlyDictionary<Guid, string> tabButtons)
    {
        Dispose();
        
        foreach (var (guid, label) in tabButtons)
        {
            var instance = Instantiate(_tabButtonPrefab, _tabButtonParent);
            _tabButtonDictionary.Add(guid, instance);

            instance.Initialize(guid, label);
            instance.OnClickAsObservable.Subscribe(_onSelectedSubject.OnNext).AddTo(_disposables);
        }
    }

    public void Select(Guid guid)
    {
        if (!_tabButtonDictionary.ContainsKey(guid)) return;
        
        ReleaseAll();
        _tabButtonDictionary[guid].Press();
    }
    
    private void ReleaseAll()
    {
        foreach (var (guid, instance) in _tabButtonDictionary)
        {
            instance.Release();
        }
    }

    private void Dispose()
    {
        _disposables.ForEach(d =>
        {
            d.Dispose();
        });
        
        _disposables.Clear();
        
        foreach (var (guid, instance) in _tabButtonDictionary)
        {
            DestroyImmediate(instance);
        }
        
        _tabButtonDictionary.Clear();
    }
}
