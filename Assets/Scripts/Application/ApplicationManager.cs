using System;
using System.Collections.Generic;
using System.Linq;
using com.kodai100.ArtNetApp.Models;
using UnityEngine;
using UniRx;

namespace com.kodai100.ArtNetApp
{

    [Serializable]
    public class LabelAndAppPair
    {
        public string Guid = System.Guid.NewGuid().ToString();
        public string Label;
        public App App;
    }
    
    // Entry point of this application
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private TabUI _applicationTab;

        [SerializeField] private List<LabelAndAppPair> _applications;

        private ProjectDataManager _projectDataManager = new();

        private void Start()
        {

            // TODO: Load Data
            _projectDataManager.MockupProjectData();
            
            var dict = _applications.ToDictionary(x => Guid.Parse(x.Guid), x => x.Label);
            _applicationTab.Initialize(dict);
            
            _applicationTab.OnSelected.Subscribe(ApplicationChanged).AddTo(this);
            
            ApplicationChanged(Guid.Parse(_applications.First().Guid));
        }
    
        private void ApplicationChanged(Guid guid)
        {
        
            _applications.ForEach(app =>
            {
                app.App.OnClose();
            });

            var target = _applications.FirstOrDefault(x => Guid.Parse(x.Guid) == guid);

            if (target != null)
            {
                target.App.OnOpen(_projectDataManager);
                _applicationTab.Select(Guid.Parse(target.Guid));
            }
            
        }

        public void OnDestroy()
        {
            _applications.ForEach(app =>
            {
                app.App.OnClose();
            });
        }

    }

}

