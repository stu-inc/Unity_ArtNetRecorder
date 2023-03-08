using System;
using System.Collections.Generic;
using System.Linq;
using com.kodai100.ArtNetApp.Entities;
using UniRx;
using UnityEngine;

namespace com.kodai100.ArtNetApp.Models
{
    
    public partial class SenderModel
    {
        // Fixture Presets
        
        private ReactiveProperty<List<FixtureManufacturerEntity>> _fixtureManufacturerList = new(new List<FixtureManufacturerEntity>());
        public IReadOnlyReactiveProperty<List<FixtureManufacturerEntity>> FixtureManufacturerList => _fixtureManufacturerList;

        private readonly ReactiveProperty<FixtureManufacturerEntity> _selectedFixtureManufacturerEntity = new(null);
        public IReadOnlyReactiveProperty<FixtureManufacturerEntity> SelectedFixtureManufacturerEntity => _selectedFixtureManufacturerEntity;

        
        private ReactiveProperty<List<FixturePresetEntity>> _fixturePresetList = new(new List<FixturePresetEntity>());
        public IReadOnlyReactiveProperty<List<FixturePresetEntity>> FixturePresetList => _fixturePresetList;
    
        private readonly ReactiveProperty<FixturePresetEntity> _selectedFixturePresetEntity = new(null);
        public IReadOnlyReactiveProperty<FixturePresetEntity> SelectedFixturePresetEntity => _selectedFixturePresetEntity;

        private void InitializeFixturePresetModel(ProjectDataManager projectDataManager)
        {
            _fixtureManufacturerList = projectDataManager.FixtureManufacturerList;
            // _fixturePresetList = projectDataManager.FixturePresetList;
        }

        public void UpdateManufacturerSelection(Guid? guid)
        {
            var selectedTarget = _fixtureManufacturerList.Value.FirstOrDefault(x => x.Guid == guid);
            _selectedFixtureManufacturerEntity.SetValueAndForceNotify(selectedTarget);

            if (selectedTarget != null)
            {
                _fixturePresetList.Value = _projectDataManager.FixturePresetList.Value
                    .Where(x => x.Manufacturer == selectedTarget.ManufacturerName).ToList();
            }
        }
        
        public void UpdateFixturePresetSelection(Guid? id)
        {
            var selectedTarget = _fixturePresetList.Value.FirstOrDefault(x => x.Guid == id);
            Debug.Log($"Update Selection : {id}");
            _selectedFixturePresetEntity.SetValueAndForceNotify(selectedTarget);
        }

        public void UpdateSelectedFixturePresetData(string name)
        {
            if (_selectedFixturePresetEntity.Value == null) return;

            _selectedFixturePresetEntity.Value.FixtureName = name;
            _selectedFixturePresetEntity.SetValueAndForceNotify(_selectedFixturePresetEntity.Value);
        }
        

        public void AddFixturePresetData()
        {
            var guid = Guid.NewGuid();
        
            var data = new FixturePresetEntity
            {
                Guid = guid,
                FixtureName = guid.ToString()
            };
        
            _fixturePresetList.Value.Add(data);
            _fixturePresetList.SetValueAndForceNotify(_fixturePresetList.Value);
        }

        public void RemoveFixturePresetData()
        {
            if(_selectedFixturePresetEntity.Value == null) return;

            var target = _selectedFixturePresetEntity.Value;

            _fixturePresetList.Value.Remove(target);
            _fixturePresetList.SetValueAndForceNotify(_fixturePresetList.Value);

            _selectedFixturePresetEntity.Value = null;
        }

        public void ClearFixturePresetData()
        {
            _fixturePresetList.Value.Clear();
            _fixturePresetList.SetValueAndForceNotify(_fixturePresetList.Value);

            _selectedFixturePresetEntity.Value = null;
        }
        
    }
}
