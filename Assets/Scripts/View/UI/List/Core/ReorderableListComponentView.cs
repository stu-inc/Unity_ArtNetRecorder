using com.kodai100.ArtNetApp.Entities;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public abstract class ReorderableListComponentView<T> : ListComponentView<T> where T : ReorderableEntity
    {
        [SerializeField] private DragController _dragController;
        public DragController DragController => _dragController;

        public void RecalculateIndex()
        {
            _data.OrderIndex = transform.GetSiblingIndex();
        }
    }

}

