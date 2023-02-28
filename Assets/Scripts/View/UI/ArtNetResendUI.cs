using System;
using System.Net;
using inc.stu.SyncArena;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.View
{
    public class ArtNetResendUI : MonoBehaviour
    {

        [SerializeField] private ToggleButton enableToggle;
        [SerializeField] private StringInputField ipInputField;
        [SerializeField] private IntInputField portInputField;

        public IObservable<Unit> OnToggleClickedAsObservable => enableToggle.OnClickAsObservable.Select(_ => Unit.Default);
        public IObservable<int> OnPortChanged => portInputField.OnValueChanged;
        public IObservable<IPAddress> OnIpChanged => ipInputField.OnEndEdit.Select(IPAddress.Parse);

        public void SetToggleWithoutNotify(bool isOn)
        {
            switch (isOn)
            {
                case true:
                    enableToggle.Press();
                    break;
                case false:
                    enableToggle.Release();
                    break;
            }
        }
    
        public void SetIpWithoutNotify(IPAddress ip)
        {
            ipInputField.SetValueWithoutNotify(ip.ToString());
        }

        public void SetPortWithoutNotify(int port)
        {
            portInputField.SetValueWithoutNotify(port);
        }

    }

}

