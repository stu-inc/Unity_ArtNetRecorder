using System;
using com.kodai100.ArtNetApp.View;
using UnityEngine;
using UnityEngine.UI;

namespace com.kodai100.ArtNetApp.View
{

    public class RecorderUI : MonoBehaviour
    {

        [SerializeField] private Text timeCodeText;

        [SerializeField] private ToggleButton recordButton;
        
        [SerializeField] private IndicatorUI indicatorUI;
        
        public ToggleButton RecordButton => recordButton;

        public IndicatorUI IndicatorUI => indicatorUI;

        public Text TimeCodeText => timeCodeText;


        public void Initialize()
        {
            
            recordButton.Release();
            indicatorUI.ResetIndicator();
            timeCodeText.text = "00:00:00:00";
            
        }

    }

}