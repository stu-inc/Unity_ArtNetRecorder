using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayToggleButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image pauseImage;
    [SerializeField] private Image playImage;

    [SerializeField] private Color pauseColor = Color.red;
    [SerializeField] private Color playColor = Color.green;

    public IObservable<Unit> OnClickAsObservable => button.OnClickAsObservable();
    
    public void SetAsPauseVisual()
    {
        if(button != null)
            button.image.color = pauseColor;
        
        if(pauseImage != null)
            pauseImage.gameObject.SetActive(true);
        
        if(playImage != null)
            playImage.gameObject.SetActive(false);
    }

    public void SetAsPlayVisual()
    {
        if(button != null)
           button.image.color = playColor;
        
        if(pauseImage != null)
            pauseImage.gameObject.SetActive(false);
        
        if(playImage != null)
            playImage.gameObject.SetActive(true);
    }
}
