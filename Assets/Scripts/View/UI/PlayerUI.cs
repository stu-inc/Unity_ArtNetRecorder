using System;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace com.kodai100.ArtNetApp.View
{
    public class PlayerUI : MonoBehaviour
    {
    
        [SerializeField] private ToggleButton playButton;
        [SerializeField] private Slider slider;
        [SerializeField] private Text headerText;
        [SerializeField] private Text endText;
    
        private double endTimeMillisec; 
        
        public IObservable<Unit> OnPlayButtonPressedAsObservable => playButton.OnClickAsObservable.Select(_ => Unit.Default);
    
        private void Awake()
        {
            Release();
    
            slider.OnValueChangedAsObservable().Subscribe(value =>
            {
    
                var headerMillisec = value * endTimeMillisec;
                
                var sec = headerMillisec * 0.001d;
                var min = (int) (sec / 60);
                sec = sec - (min * 60);
    
                headerText.text = $"{min:D2}:{(int)sec:D2};{(int)headerMillisec % 1000:D3}";
            }).AddTo(this);
        }
        
        public void Release()
        {
            playButton.Release();
        }
    
        public void Press()
        {
            playButton.Press();
        }
    
        public float GetSliderPosition()
        {
            return slider.value;
        }
        
        public void Initialize(double endTimeMillisec)
        {
            var sec = endTimeMillisec / 1000d;
            var min = (int) (sec / 60);
            sec = sec - (min * 60);
    
            endText.text = $"{min:D2}:{(int)sec:D2}";
    
            this.endTimeMillisec = endTimeMillisec;
            
            slider.value = 0;
        }
        
        public void SetHeader(double headerMillisec)
        {
            slider.value = (float)(headerMillisec / endTimeMillisec);
        }
        
    }

}

