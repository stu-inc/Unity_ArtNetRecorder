using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

namespace inc.stu.SyncArena
{
    public abstract class DropDownField<T> : MonoBehaviour
    {

        [SerializeField] protected TMP_Dropdown _dropdown;

        protected abstract List<DropDownElement<T>> DropdownElementList { get; }
        protected virtual string None { get; }

        public T Value => DropdownElementList[_dropdown.value].Value;
        
        private Subject<T> _onValueChanged = new();
        public IObservable<T> OnValueChanged => _onValueChanged;

        private void Awake()
        {

            SetupOptions();

            _dropdown.onValueChanged.AsObservable().Subscribe(index =>
            {
                
                // Noneが存在するとき、一番最後の要素はNoneの要素になるので、値を通知しない
                if (!string.IsNullOrEmpty(None))
                {
                    if (index == DropdownElementList.Count)
                    {
                        return;
                    }
                    _onValueChanged.OnNext(DropdownElementList[index].Value);
                } else
                {
                    _onValueChanged.OnNext(DropdownElementList[index].Value);
                }
                
                
            }).AddTo(this);
        }

        private void SetupOptions()
        {
            List<TMP_Dropdown.OptionData> options = new ();
            
            DropdownElementList.ForEach(element =>
            {
                options.Add(new TMP_Dropdown.OptionData(element.Label));
            });

            if (!string.IsNullOrEmpty(None))
            {
                options.Add(new TMP_Dropdown.OptionData(None));
            }
            
            _dropdown.options = options;
        }

        public void SelectNone()
        {
            if (string.IsNullOrEmpty(None)) return;
            
            _dropdown.SetValueWithoutNotify(DropdownElementList.Count);
        }

    }

    public class DropDownElement<T>
    {
        public string Label;
        public T Value;

        public DropDownElement(string label, T value)
        {
            Label = label;
            Value = value;
        }
    }

}

