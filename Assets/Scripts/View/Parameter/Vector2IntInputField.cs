using System;
using UniRx;
using UnityEngine;

namespace inc.stu.SyncArena
{
    public class Vector2IntInputField : MonoBehaviour
    {

        [SerializeField] private IntInputField _xField;
        [SerializeField] private IntInputField _yField;

        private Subject<Vector2Int> _onValueChanged = new();
        public IObservable<Vector2Int> OnValueChanged => _onValueChanged;

        private Vector2Int _value;
        public Vector2Int Value => _value;

        private void Awake()
        {
            _xField.OnValueChanged.Subscribe(value =>
            {
                _value = Composite();
                Debug.Log($"v - {_value}");
                _onValueChanged.OnNext(_value);
            }).AddTo(this);
            
            _yField.OnValueChanged.Subscribe(value =>
            {
                _value = Composite();
                Debug.Log($"v - {_value}");
                _onValueChanged.OnNext(_value);
            }).AddTo(this);
        }

        public void SetValueWithNotify(Vector2Int value)
        {
            SetValueWithoutNotify(value);
            _onValueChanged.OnNext(value);
        }

        public void SetValueWithoutNotify(Vector2Int value)
        {
            _xField.SetValueWithoutNotify(value.x);
            _yField.SetValueWithoutNotify(value.y);
            _value = value;
        }

        // Update is called once per frame
        private Vector2Int Composite() => new(_xField.Value, _yField.Value);
        
    }

}

