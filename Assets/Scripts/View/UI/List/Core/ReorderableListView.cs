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
        
        public IObservable<IEnumerable<T>> OnListOrderChanged => _onListOrderChanged;
        protected Subject<IEnumerable<T>> _onListOrderChanged = new();


        protected override void RegisterComponentEvent(ListComponentView<T> component, List<IDisposable> disposables)
        {
            var converted = component as ReorderableListComponentView<T>;
            if (converted != null)
            {
                converted.DragController.OnOrderChanged.Subscribe(_ =>
                {
                    _listComponents.ForEach(c =>
                    {
                        var cc = c as ReorderableListComponentView<T>;
                        if (cc != null) cc.RecalculateIndex();
                    });

                    _onListOrderChanged.OnNext(_listComponents.Select(c => c.Data));
                }).AddTo(disposables);
            }
        }

    }

}

