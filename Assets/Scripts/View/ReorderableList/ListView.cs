using System;
using System.Collections.Generic;
using System.Linq;
using com.kodai100.ArtNetApp.Entities;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace com.kodai100.ArtNetApp.View
{
    public abstract class ListView<T> : MonoBehaviour where T : Entity 
    {
        [SerializeField] protected ScrollRect _scrollRect;

        protected abstract ListComponentView<T> ListComponentViewPrefab { get; }
        
        protected Subject<Guid> _onComponentSelected = new();
        public IObservable<Guid> OnComponentSelected => _onComponentSelected;
        
        private List<ListComponentView<T>> _listComponents = new();
        
        protected List<IDisposable> _disposables = new();
        
        
        protected virtual void Awake()
        {
            foreach (Transform t in _scrollRect.content)
            {
                Destroy(t.gameObject);
            }
        }
        
        
        public virtual void Initialize(IEnumerable<T> dataList)
        {
            ClearAll();
            
            foreach (var data in dataList)
            {
                var component = Instantiate(ListComponentViewPrefab, _scrollRect.content);
                component.Initialize(data);
                
                component.OnSelect.Subscribe(guid =>
                {
                    _onComponentSelected.OnNext(guid);
                }).AddTo(_disposables);
                
                _listComponents.Add(component);
            }
        }
        
        public virtual void MarkAsSelected(Guid? guid)
        {
            _listComponents.ForEach(c => c.UnSelect());
            
            var target = _listComponents.FirstOrDefault(c => c.Data.Guid == guid);
            target?.Select();
        }

        protected virtual void ClearAll()
        {
            _listComponents.ForEach(component =>
            {
                Destroy(component.gameObject);
            });
            _listComponents.Clear();
            
            _disposables.ForEach(disposable =>
            {
                disposable.Dispose();
            });
            _disposables.Clear();
        }
    }
    
}