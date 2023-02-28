using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace inc.stu.SyncArena
{
    public class DragHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        [SerializeField] private float _amount = 1;
        [SerializeField] private Texture2D _dragMouseCursor;
        
        private Subject<Unit> _onDragStart = new();
        public IObservable<Unit> OnDragStart => _onDragStart;
        
        private Subject<float> _onDrag = new();
        public IObservable<float> OnDragged => _onDrag;


        private Subject<float> _onEndDrag = new();
        public IObservable<float> OnEndDragged => _onEndDrag;

        private float _currentDragAmount;
        private Vector2 _lastMousePosition;

        private bool _isDragging;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastMousePosition = Input.mousePosition;
            SetHoverCursor();
            
            _onDragStart.OnNext(Unit.Default);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _isDragging = true;
            
            _currentDragAmount = (Input.mousePosition.x - _lastMousePosition.x) * _amount;
            
            _onDrag.OnNext(_currentDragAmount);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            
            _onEndDrag.OnNext(_currentDragAmount);

            SetDefaultCursor();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetHoverCursor();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isDragging)
            {
                SetDefaultCursor();
            }
        }
        
        private void SetHoverCursor()
        {
            Cursor.SetCursor(_dragMouseCursor, Vector2.zero, CursorMode.Auto);
        }

        private void SetDefaultCursor()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

}

