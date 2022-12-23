using System;
using System.Net;
using inc.stu.SyncArena;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ArtNetResendUI : MonoBehaviour
{

    [SerializeField] private Toggle enableToggle;
    [SerializeField] private StringInputField ipInputField;
    [SerializeField] private IntInputField portInputField;

    public IObservable<bool> IsEnabled => enableToggle.OnValueChangedAsObservable();
    public IObservable<int> OnPortChanged => portInputField.OnValueChanged;
    public IObservable<IPAddress> OnIpChanged => ipInputField.OnValueChanged.Select(IPAddress.Parse);

    public void SetToggleWithoutNotify(bool isOn)
    {
        enableToggle.SetIsOnWithoutNotify(isOn);
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
