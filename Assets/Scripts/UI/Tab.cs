using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{

    [SerializeField] private Color disabledBackgroundColor;
    [SerializeField] private Color enabledBackgroundColor;

    [SerializeField] private List<Button> buttons;
    
    private Subject<int> onSelected = new();
    public IObservable<int> OnSelected => onSelected;

    // TODO: Tabの中で切り替え処理までやってしまって良いものか迷い中
    private async void Start()
    {
        
        for (var i = 0; i < buttons.Count; i++)
        {
            var i1 = i;    // capture
            buttons[i1].OnClickAsObservable().Subscribe(_ =>
            {
                onSelected.OnNext(i1);
                buttons.ForEach(pair =>
                {
                    if (pair != null)
                    {
                        pair.targetGraphic.color = disabledBackgroundColor;
                    }
                });
                
                if (buttons[i1] != null)
                {
                    buttons[i1].targetGraphic.color = enabledBackgroundColor;
                }

            }).AddTo(this);
        }

        await Task.Delay(TimeSpan.FromSeconds(0.1f));
        
        buttons[0].onClick.Invoke();
        
    }


    public void Disable()
    {
        buttons.ForEach(button =>
        {
            button.interactable = false;
        });
    }

    public void Enable()
    {
        buttons.ForEach(button =>
        {
            button.interactable = true;
        });
    }
    
    private void OnDestroy()
    {
        onSelected.Dispose();
    }
}
