using System;
using System.Collections;
using System.Collections.Generic;
using SFB;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class FileDialogUI : MonoBehaviour
{

    [SerializeField] private InputField inputField;
    [SerializeField] private Button browseButton;
    [SerializeField] private string extensionFilter = "jpg";

    public IObservable<string> OnFileNameChanged => onFileNameChanged;

    private Subject<string> onFileNameChanged = new();

    private void Awake()
    {

        inputField.onEndEdit.AsObservable().Subscribe(text =>
        {
            onFileNameChanged.OnNext(text);
        }).AddTo(this);

        browseButton.OnClickAsObservable().Subscribe(_ =>
        {
            Open();
        }).AddTo(this);
    }

    private void Open()
    {
        var extensions = new[] {
            new ExtensionFilter("DMX Record Files", extensionFilter.Split(',') ),
        };

        StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", extensions, false, (string[] paths) =>
        {
            inputField.text = paths[0];
            onFileNameChanged.OnNext(paths[0]);
        });
    }

    public void SetValueWithoutNotify(string value)
    {
        inputField.SetTextWithoutNotify(value);
    }

    private void OnDestroy()
    {
        onFileNameChanged.Dispose();
    }
}
