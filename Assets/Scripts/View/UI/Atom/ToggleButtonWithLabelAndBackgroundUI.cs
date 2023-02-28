using TMPro;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    
    public abstract class ToggleButton : Button
    {
        private bool _isPressed;

        public bool IsPressed => _isPressed;

        public void Press()
        {
            _isPressed = true;
            Pressed();
        }
    
        public void Release()
        {
            _isPressed = false;
            Released();
        }
    
        protected abstract void Pressed();
    
        protected abstract void Released();
    }

    public abstract class ToggleButtonUGUI : ToggleButton
    {
        [SerializeField] protected UnityEngine.UI.Button _button;

        protected virtual void Awake()
        {
            _button.OnClickAsObservable().Subscribe(_ =>
            {
                _onClickSubject.OnNext(_guid);
            }).AddTo(this);
        }
    }
    
    public class ToggleButtonWithLabelAndBackgroundUI : ToggleButtonUGUI
    {
        [SerializeField] private TMP_Text _text;
    
        [SerializeField] private UnityEngine.UI.Image _backgroundImage;
        [SerializeField] private Color _pressedColor;
        [SerializeField] private Color _releasedColor;
        
        protected override void SetLabel(string label)
        {
            _text.text = label;
        }
    
        protected override void Pressed()
        {
            _backgroundImage.color = _pressedColor;
        }
    
        protected override void Released()
        {
            _backgroundImage.color = _releasedColor;
        }
    }

}

