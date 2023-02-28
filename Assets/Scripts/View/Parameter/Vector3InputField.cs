using System;
using UniRx;
using UnityEngine;

namespace inc.stu.SyncArena
{
    public class Vector3InputField : MonoBehaviour
    {

        [SerializeField] private FloatInputField _xField;
        [SerializeField] private FloatInputField _yField;
        [SerializeField] private FloatInputField _zField;

        private Subject<Vector3> _onValueChanged = new();
        public IObservable<Vector3> OnValueChanged => _onValueChanged;

        private Vector3 _value;
        public Vector3 Value => _value;

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
            
            _zField.OnValueChanged.Subscribe(value =>
            {
                _value = Composite();
                _onValueChanged.OnNext(_value);
            }).AddTo(this);
        }

        public void SetValueWithNotify(Vector3 value)
        {
            SetValueWithoutNotify(value);
            _onValueChanged.OnNext(value);
        }

        public void SetValueWithoutNotify(Vector3 value)
        {
            _xField.SetValueWithoutNotify(value.x);
            _yField.SetValueWithoutNotify(value.y);
            _zField.SetValueWithoutNotify(value.z);
            _value = value;
        }

        // Update is called once per frame
        private Vector3 Composite() => new Vector3(_xField.Value, _yField.Value, _zField.Value);
        
    }

}

