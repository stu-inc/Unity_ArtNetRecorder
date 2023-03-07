using System;
using System.Collections.Generic;
using System.Linq;
using com.kodai100.ArtNetApp.Entities;
using UniRx;

namespace com.kodai100.ArtNetApp.View
{
    public abstract class ReorderableListView<T> : ListView<T> where T : ReorderableEntity
    {

        protected abstract ReorderableListComponentView<T> ReorderableListComponentViewPrefab { get; }
        protected override ListComponentView<T> ListComponentViewPrefab => ReorderableListComponentViewPrefab;

        private List<ReorderableListComponentView<T>> _reorderableListComponents = new();
        
        public IObservable<IEnumerable<T>> OnListOrderChanged => _onListOrderChanged;
        private Subject<IEnumerable<T>> _onListOrderChanged = new();

        
        public override void Initialize(IEnumerable<T> dataList)
        {
            ClearAll();

            foreach (var data in dataList)
            {
                var component = Instantiate(ReorderableListComponentViewPrefab, _scrollRect.content);
                component.Initialize(data);
                
                component.DragController.OnOrderChanged.Subscribe(_ =>
                {
                    _reorderableListComponents.ForEach(c =>
                    {
                        c.RecalculateIndex();
                    });
            
                    _onListOrderChanged.OnNext(_reorderableListComponents.Select(c => c.Data));
                    
                }).AddTo(_disposables);
                
                component.OnSelect.Subscribe(guid =>
                {
                    _onComponentSelected.OnNext(guid);
                }).AddTo(_disposables);
                
                _reorderableListComponents.Add(component);
            }
        }
        
        public override void MarkAsSelected(Guid? guid)
        {
            _reorderableListComponents.ForEach(c => c.UnSelect());
            
            var target = _reorderableListComponents.FirstOrDefault(c => c.Data.Guid == guid);
            target?.Select();
        }

        protected override void ClearAll()
        {
            _reorderableListComponents.ForEach(component =>
            {
                Destroy(component.gameObject);
            });
            _reorderableListComponents.Clear();
            
            _disposables.ForEach(disposable =>
            {
                disposable.Dispose();
            });
            _disposables.Clear();
        }

    }

}

