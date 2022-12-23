using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace inc.stu.SyncArena
{
    public class StringInputField : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        public IObservable<string> OnValueChanged => _inputField.onValueChanged.AsObservable();

        public string Value => _inputField.text;
        
        public void SetValueWithoutNotify(string name)
        {
            _inputField.SetTextWithoutNotify(name);
        }

        public void SetValueWithNotify(string name)
        {
            _inputField.text = name;
        }
    }

}

