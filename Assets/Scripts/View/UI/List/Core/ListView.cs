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
        
        protected List<ListComponentView<T>> _listComponents = new();
        
        private List<IDisposable> _disposables = new();

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
                
                RegisterComponentEvent(component, _disposables);
                
                _listComponents.Add(component);
            }
        }

        protected virtual void RegisterComponentEvent(ListComponentView<T> component, List<IDisposable> disposables)
        {
            
        }

        private Guid? _prevSelected;
        
        public virtual void MarkAsSelected(Guid? guid)
        {
            if (_prevSelected == guid) return;
            
            _listComponents.ForEach(c => c.UnSelect());
            
            var target = _listComponents.FirstOrDefault(c => c.Data.Guid == guid);
            if (target != null)
            {
                target.Select();
            }
            
            _prevSelected = guid;
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