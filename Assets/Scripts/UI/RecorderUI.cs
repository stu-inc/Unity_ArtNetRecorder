using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.ArtNetRecorder
{


    public class RecorderUI : MonoBehaviour
    {

        [SerializeField] private Text timeCodeText;

        [SerializeField] private RecordButton recordButton;
        
        [SerializeField] private IndicatorUI indicatorUI;
        
        public RecordButton RecordButton => recordButton;

        public IndicatorUI IndicatorUI => indicatorUI;

        public Text TimeCodeText => timeCodeText;


        public void Initialize()
        {
            
            recordButton.SetRecord();
            indicatorUI.ResetIndicator();
            timeCodeText.text = "00:00:00:00";
            
        }

    }

}