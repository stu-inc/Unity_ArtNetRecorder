using System.Collections.Generic;
using System.Linq;
using com.kodai100.ArtNetApp.Entities;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.Models
{

    public partial class SenderModel : Model
    {

        public SenderModel(ProjectDataManager projectDataManager) : base(projectDataManager)
        {
            InitializeFixturePresetModel(projectDataManager);
            InitializeFixturePlacementModel(projectDataManager);
            InitializeDmxChannelModel(projectDataManager);
            
            
            UniverseSelection(projectDataManager);
        }

        private void UniverseSelection(ProjectDataManager projectDataManager)
        {

            // Universeの選択が更新されたとき
            _universe.Subscribe(u =>
            {
                var target = projectDataManager.FixturePlacementList.Value.Where(x => x.Universe == u).ToList();
                _fixturePlacementList.SetValueAndForceNotify(target);

                _selectedFixturePlacementEntity.Value = null;
            }).AddTo(_disposables);

            // 保存データがアップデートされたとき
            _projectDataManager.FixturePlacementList.Subscribe(list =>
            {
                var filtered = projectDataManager.FixturePlacementList.Value.Where(x => x.Universe == _universe.Value)
                    .ToList();
                _fixturePlacementList.SetValueAndForceNotify(filtered);
            }).AddTo(_disposables);
            
            
            // Placementの選択が変わったとき
            _selectedFixturePlacementEntity.Subscribe(s =>
            {
                if (s == null)
                {
                    _dmxChannelList.SetValueAndForceNotify(new List<DmxChannelEntity>());
                    return;
                }

                var filtered = projectDataManager.DmxChannelList.Value.Where(x => x.InstancedFixtureReferenceGuid.Equals(s.Guid)).ToList();
                _dmxChannelList.SetValueAndForceNotify(filtered);
            }).AddTo(_disposables);

        }
    }


}
