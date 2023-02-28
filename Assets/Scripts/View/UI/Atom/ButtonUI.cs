using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace com.kodai100.ArtNetApp.View
{
    public class ButtonUI : Button
    {
    
        [SerializeField] private TMP_Text _text;
        [SerializeField] private UnityEngine.UI.Button _button;
    
        protected override void SetLabel(string label)
        {
            _text.text = label;
        }
        
    }

    public abstract class Button : MonoBehaviour
    {
        protected readonly Subject<Guid> _onClickSubject = new();
        protected Guid _guid;
        
        public IObservable<Guid> OnClickAsObservable => _onClickSubject;
        public Guid GUID => _guid;
        
        public virtual void Initialize(Guid guid, string label)
        {
            _guid = guid;
            SetLabel(label);
        }
    
        protected virtual void SetLabel(string label)
        {
            
        }
    
        private void OnDestroy()
        {
            _onClickSubject.Dispose();
        }
        
    }
    
   
}

