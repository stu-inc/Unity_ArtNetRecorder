using System;
using UniRx;
using UnityEngine;

namespace inc.stu.SyncArena
{
    public class Vector2InputField : MonoBehaviour
    {

        [SerializeField] private FloatInputField _xField;
        [SerializeField] private FloatInputField _yField;

        private Subject<Vector2> _onValueChanged = new();
        public IObservable<Vector2> OnValueChanged => _onValueChanged;

        private Vector2 _value;
        public Vector2 Value => _value;

        private void Awake()
        {
            _xField.OnValueChanged.Subscribe(value =>
            {
                _value = Composite();
                _onValueChanged.OnNext(_value);
            }).AddTo(this);
            
            _yField.OnValueChanged.Subscribe(value =>
            {
                _value = Composite();
                _onValueChanged.OnNext(_value);
            }).AddTo(this);
        }

        public void SetValueWithNotify(Vector2 value)
        {
            SetValueWithoutNotify(value);
            _onValueChanged.OnNext(value);
        }

        public void SetValueWithoutNotify(Vector2 value)
        {
            _xField.SetValueWithoutNotify(value.x);
            _yField.SetValueWithoutNotify(value.y);
            _value = value;
        }

        // Update is called once per frame
        private Vector2 Composite() => new(_xField.Value, _yField.Value);
        
    }

}

