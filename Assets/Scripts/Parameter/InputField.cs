using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace inc.stu.SyncArena
{
    public abstract class InputField<T> : MonoBehaviour
    {

        [SerializeField] private TMP_InputField _inputField;

        [SerializeField] private DragHandler _dragHandler;

        protected T _currentValue;

        private Subject<T> _onValueChanged = new();
        public IObservable<T> OnValueChanged => _onValueChanged;

        public T Value => _currentValue;

        protected T _dragStartValue;

        private void Awake()
        {

            _inputField.onEndEdit.AsObservable().Subscribe(value =>
            {
                _currentValue = Parse(value);
                _onValueChanged.OnNext(_currentValue);
            }).AddTo(this);


            if (_dragHandler)
            {
                _dragHandler.OnDragStart.Subscribe(_ =>
                {
                    _dragStartValue = _currentValue;
                }).AddTo(this);
                
                _dragHandler.OnDragged.Subscribe(dragAmount =>
                {
                    var newValue = OnDragged(dragAmount);
                    SetValueWithNotify(newValue);
                }).AddTo(this);
            }

        }

        public void SetValueWithoutNotify(T value)
        {
            _currentValue = value;
            _inputField.SetTextWithoutNotify(Format(value));
        }

        public void SetValueWithNotify(T value)
        {
            SetValueWithoutNotify(value);
            _onValueChanged.OnNext(value);
        }

        protected abstract T OnDragged(float dragAmount);
        protected abstract T Parse(string value);
        protected abstract string Format(T value);

    }

}

