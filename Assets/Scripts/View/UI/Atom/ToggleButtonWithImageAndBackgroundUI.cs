using UnityEngine.UI;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class ToggleButtonWithImageAndBackgroundUI : ToggleButtonUGUI
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Color _pressedColor;
        [SerializeField] private Color _releasedColor;
        
        [SerializeField] private Image _frontImage;
        [SerializeField] private Sprite _pressedSprite;
        [SerializeField] private Sprite _releasedSprite;
        
        protected override void Pressed()
        {
            _frontImage.sprite = _pressedSprite;
            _backgroundImage.color = _pressedColor;
        }

        protected override void Released()
        {
            _frontImage.sprite = _releasedSprite;
            _backgroundImage.color = _releasedColor;
        }
    }

}

