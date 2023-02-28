using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class ToggleButtonWithImageUI : ToggleButtonUGUI
    {

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _pressedSprite;
        [SerializeField] private Sprite _releasedSprite;

        protected override void Pressed()
        {
            _image.color = _pressedSprite == null ? new Color(0, 0, 0, 0) : new Color(1, 1, 1, 1);
            _image.sprite = _pressedSprite;
        }

        protected override void Released()
        {
            _image.color = _releasedSprite == null ? new Color(0, 0, 0, 0) : new Color(1, 1, 1, 1);
            _image.sprite = _releasedSprite;
        }
    }

}

