using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using com.kodai100.ArtNetApp.Entities;

namespace com.kodai100.ArtNetApp.Models
{
    public partial class SenderModel
    {
        // Fixture Presets
        
        private readonly ReactiveProperty<List<FixturePlacementEntity>> _fixturePlacementList = new(new List<FixturePlacementEntity>());
        public IReadOnlyReactiveProperty<List<FixturePlacementEntity>> FixturePlacementList => _fixturePlacementList;
    
        private readonly ReactiveProperty<FixturePlacementEntity> _selectedFixturePlacementEntity = new(null);
        public IReadOnlyReactiveProperty<FixturePlacementEntity> SelectedFixturePlacementEntity => _selectedFixturePlacementEntity;

        private readonly ReactiveProperty<int> _universe = new(1);
        public IReadOnlyReactiveProperty<int> Universe => _universe;

        private void InitializeFixturePlacementModel(ProjectDataManager projectDataManager)
        {
        }
        
        public void UpdateFixturePlacementSelection(Guid? id)
        {
            var selectedTarget = _fixturePlacementList.Value.FirstOrDefault(x => x.Guid == id);
            Debug.Log($"Update Selection : {id}");
            _selectedFixturePlacementEntity.Value = selectedTarget;
        }

        public void UpdateSelectedFixturePlacementData(string name)
        {
            if (_selectedFixturePlacementEntity.Value == null) return;

            _selectedFixturePlacementEntity.Value.Name = name;
            _selectedFixturePlacementEntity.SetValueAndForceNotify(_selectedFixturePlacementEntity.Value);
        }

        public void UpdateFixturePlacementOrder(IEnumerable<ReorderableEntity> list)
        {
            foreach (var target in list)
            {
                var t = _fixturePlacementList.Value.FirstOrDefault(entity => target.Guid == entity.Guid);
                if(t == null) continue;
                t.OrderIndex = target.OrderIndex;
            }
        
            _fixturePlacementList.SetValueAndForceNotify(_fixturePlacementList.Value.OrderBy(x => x.OrderIndex).ToList());
        }

        public void AddFixturePlacementData()
        {
            var guid = Guid.NewGuid();
        
            var data = new FixturePlacementEntity()
            {
                Guid = guid,
                OrderIndex = _fixturePresetList.Value.Count,
                Name = guid.ToString()
            };
        
            _fixturePlacementList.Value.Add(data);
            _fixturePlacementList.SetValueAndForceNotify(_fixturePlacementList.Value);
        }

        public void RemoveFixturePlacementData()
        {
            if(_selectedFixturePlacementEntity.Value == null) return;

            var target = _selectedFixturePlacementEntity.Value;

            _fixturePlacementList.Value.Remove(target);
            _fixturePlacementList.SetValueAndForceNotify(_fixturePlacementList.Value);

            _fixturePlacementList.Value = null;
        }

        // public void ClearFixturePlacementData()
        // {
        //     _data.Value.Clear();
        //     _data.SetValueAndForceNotify(_data.Value);
        //
        //     _selected.Value = null;
        // }


        public void UpdateUniverse(int universe)
        {
            _universe.Value = universe;
        }
        
        // Universe selection
        public void IncrementUniverse()
        {
            _universe.Value += 1;
        }

        public void DecrementUniverse()
        {
            if(_universe.Value <= 0) return;
            
            _universe.Value -= 1;
        }
        
        
    }

}

