using System;
using com.kodai100.ArtNetApp.Entities;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace com.kodai100.ArtNetApp.View
{

    public class ListComponentView<T> : MonoBehaviour where T : Entity
    {
        [SerializeField] protected UnityEngine.UI.Button _selectButton;
        [SerializeField] protected Image _backgroundImage;
        
        [SerializeField] private Color _selectedColor = Color.black;
        [SerializeField] private Color _unselectedColor = Color.grey;
        
        protected T _data;
        public T Data => _data;
        
        public IObservable<Guid> OnSelect => _selectButton.OnClickAsObservable().Select(_ => _data.Guid);
        
        public virtual void Initialize(T data)
        {
            _data = data;
            name = data.Guid.ToString();
            UnSelect();
        }
        
        public void Select()
        {
            _backgroundImage.color = _selectedColor;
        }

        public void UnSelect()
        {
            _backgroundImage.color = _unselectedColor;
        }
    }

}