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
        
        private readonly ReactiveProperty<List<DmxChannelEntity>> _dmxChannelList = new(new List<DmxChannelEntity>());
        public IReadOnlyReactiveProperty<List<DmxChannelEntity>> DmxChannelList => _dmxChannelList;
    
        private readonly ReactiveProperty<DmxChannelEntity> _selectedDmxChannelEntity = new(null);
        public IReadOnlyReactiveProperty<DmxChannelEntity> SelectedDmxChannelEntity => _selectedDmxChannelEntity;

        private void InitializeDmxChannelModel(ProjectDataManager projectDataManager)
        {
        }
        
        public void UpdateDmxChannelSelection(Guid? id)
        {
            var selectedTarget = _dmxChannelList.Value.FirstOrDefault(x => x.Guid == id);
            _selectedDmxChannelEntity.SetValueAndForceNotify(selectedTarget);
        }

        public void UpdateSelectedDmxChannelData(int channelIndex, int channelValue)
        {
            if (_selectedDmxChannelEntity.Value == null) return;

            _selectedDmxChannelEntity.Value.ChannelIndex = channelIndex;
            _selectedDmxChannelEntity.Value.ChannelValue = channelValue;
            _selectedDmxChannelEntity.SetValueAndForceNotify(_selectedDmxChannelEntity.Value);
        }

        public void UpdateDmxChannelOrder(IEnumerable<ReorderableEntity> list)
        {
            foreach (var target in list)
            {
                var t = _dmxChannelList.Value.FirstOrDefault(entity => target.Guid == entity.Guid);
                if(t == null) continue;
                t.OrderIndex = target.OrderIndex;
            }
        
            _dmxChannelList.SetValueAndForceNotify(_dmxChannelList.Value.OrderBy(x => x.OrderIndex).ToList());  // TODO: 全データでソートされてしまうので選択中のFixtureのチャンネルのみ抽出する
        }

        public void AddDmxChannelData(string channelName)
        {
            var guid = Guid.NewGuid();
        
            var data = new DmxChannelEntity()
            {
                Guid = guid,
                OrderIndex = _dmxChannelList.Value.Count,
                ChannelName = channelName,
                ChannelIndex = _dmxChannelList.Value.Max(x => x.OrderIndex) + 1,
                InstancedFixtureReferenceGuid = _selectedFixturePlacementEntity.Value.Guid
            };
        
            _projectDataManager.DmxChannelList.Value.Add(data);
            // 現在選択しているPlacementを選択し直す
            _selectedFixturePlacementEntity.SetValueAndForceNotify(_selectedFixturePlacementEntity.Value);
        }

        public void RemoveDmxChannelData(Guid guid)
        {
            if (_selectedDmxChannelEntity.Value != null)
            {
                if (_selectedDmxChannelEntity.Value.Guid == guid)
                {
                    _selectedDmxChannelEntity.Value = null;
                }
            }
            
            var selectedTarget = _dmxChannelList.Value.FirstOrDefault(x => x.Guid == guid);
            
            _dmxChannelList.Value.Remove(selectedTarget);
            _dmxChannelList.SetValueAndForceNotify(_dmxChannelList.Value);

            _selectedDmxChannelEntity.Value = null;
        }

        public void ClearDmxChannelData()
        {
            _dmxChannelList.Value.Clear();
            _dmxChannelList.SetValueAndForceNotify(_dmxChannelList.Value);

            _dmxChannelList.Value = null;
        }

        public void UpdateDmxChannelValue(Guid guid, int value)
        {
            if (_selectedDmxChannelEntity.Value != null)
            {
                if (_selectedDmxChannelEntity.Value.Guid == guid)
                {
                    UpdateSelectedDmxChannelData(_selectedDmxChannelEntity.Value.ChannelIndex, value);
                    return;
                }
            }
            
            var selectedTarget = _dmxChannelList.Value.FirstOrDefault(x => x.Guid == guid);
            if (selectedTarget != null)
            {
                _selectedDmxChannelEntity.Value = selectedTarget;
                UpdateSelectedDmxChannelData(selectedTarget.ChannelIndex, value);
            }
        }

        public void UpdateDmxChannelIndex(Guid guid, int value)
        {
            if (_selectedDmxChannelEntity.Value != null)
            {
                if (_selectedDmxChannelEntity.Value.Guid == guid)
                {
                    UpdateSelectedDmxChannelData(value, _selectedDmxChannelEntity.Value.ChannelValue);
                    return;
                }
            }

            var selectedTarget = _dmxChannelList.Value.FirstOrDefault(x => x.Guid == guid);
            if (selectedTarget != null)
            {
                _selectedDmxChannelEntity.Value = selectedTarget;
                UpdateSelectedDmxChannelData(value, selectedTarget.ChannelValue);
            }
        }
    }
}