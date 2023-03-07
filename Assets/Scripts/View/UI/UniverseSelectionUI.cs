using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class UniverseSelectionUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private UnityEngine.UI.Button _incrementButton;
        [SerializeField] private UnityEngine.UI.Button _decrementButton;
        
        public IObservable<Unit> OnIncrementButtonClicked => _incrementButton.OnClickAsObservable();
        public IObservable<Unit> OnDecrementButtonClicked => _decrementButton.OnClickAsObservable();

        public IObservable<int> OnUniverseInputFieldValueChanged => _onUniverseInputFieldValueChanged;
        private Subject<int> _onUniverseInputFieldValueChanged = new();

        private void Awake()
        {

            _inputField.onEndEdit.AsObservable().Subscribe(text =>
            {

                if (int.TryParse(text, out var result))
                {
                    _onUniverseInputFieldValueChanged.OnNext(result);
                }
                else
                {
                    _inputField.text = "0";
                    _onUniverseInputFieldValueChanged.OnNext(0);
                }
                
            }).AddTo(this);

        }
        
        public void SetValueWithNotify(int value)
        {
            _inputField.SetTextWithoutNotify(value.ToString());
            _onUniverseInputFieldValueChanged.OnNext(value);
        }
        
        public void SetValueWithoutNotify(int value)
        {
            _inputField.SetTextWithoutNotify(value.ToString());
        }
    }
}