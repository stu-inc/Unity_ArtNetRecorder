using System;
using System.Collections.Generic;
using System.Linq;
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
            _selectedFixturePlacementEntity.Value = selectedTarget;
        }

        // 使わないと思う
        public void UpdateSelectedFixturePlacementData(string name)
        {
            if (_selectedFixturePlacementEntity.Value == null) return;

            _selectedFixturePlacementEntity.Value.Name = name;
            _selectedFixturePlacementEntity.SetValueAndForceNotify(_selectedFixturePlacementEntity.Value);
        }

        public void UpdateChannelOffset(Guid guid, int offset)
        {
            var selectedTarget = _fixturePlacementList.Value.FirstOrDefault(x => x.Guid == guid);
            if (selectedTarget != null)
            {
                selectedTarget.ChannelOffset = offset;
                _selectedFixturePlacementEntity.Value = selectedTarget; // TODO: 要精査
                
                // 値が変更されたので、Placementに所属するChannelのChannelOffsetを更新する
                _projectDataManager.DmxChannelList.Value.Where(x => x.InstancedFixtureReferenceGuid == selectedTarget.Guid).ToList().ForEach(x =>
                {
                    x.ChannelOffset = offset;
                });
            }
        }

        public void UpdateFixturePlacementOrder(IEnumerable<ReorderableEntity> list)
        {
            foreach (var target in list)
            {
                var t = _fixturePlacementList.Value.FirstOrDefault(entity => target.Guid == entity.Guid);
                if(t == null) continue;
                t.OrderIndex = target.OrderIndex;
            }
            
            // TODO: projectDataManagerに反映していないので、順序を変えてUniverse変更すると元に戻ってしまう
            // TODO: ProjectDataManagerで差分検知したいところ
        
            _fixturePlacementList.SetValueAndForceNotify(_fixturePlacementList.Value.OrderBy(x => x.OrderIndex).ToList());

            _selectedFixturePlacementEntity.Value = null;
        }

        public void AddFixturePlacementData(string fixtureName)
        {
            var guid = Guid.NewGuid();
        
            var data = new FixturePlacementEntity()
            {
                Guid = guid,
                OrderIndex = _fixturePresetList.Value.Count,
                Name = fixtureName
            };
        
            _projectDataManager.FixturePlacementList.Value.Add(data);
            // 現在選択しているuniverseを選択し直す
            _universe.SetValueAndForceNotify(_universe.Value);
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


        /// <summary>
        /// ワンショットトリガー
        /// </summary>
        /// <param name="guid"></param>
        public void PlaceFixtureFromPreset(Guid presetGuid)
        {

            var preset = _projectDataManager.FixturePresetList.Value.FirstOrDefault(x => x.Guid == presetGuid);
            
            if(preset == null) return;
            
            // チャンネルオフセット用
            var channelCount = 0;
            _fixturePlacementList.Value.ForEach(x =>
            {
                channelCount += _projectDataManager.DmxChannelList.Value.Count(a => a.InstancedFixtureReferenceGuid == x.Guid);
            });
            
            var data = new FixturePlacementEntity()
            {
                Guid = Guid.NewGuid(),
                OrderIndex = _fixturePlacementList.Value.Count == 0 ? 0 : _fixturePlacementList.Value.Max(x => x.OrderIndex) + 1,    // 最大値を求める
                Name = $"{preset.FixtureName} [{preset.Channels.Length}ch]",
                Universe = _universe.Value,
                ChannelOffset = channelCount,
                ChannelNum = preset.Channels.Length,
                PresetReferenceGuid = presetGuid
            };

            preset.Channels.ToList().ForEach(c =>
            {
                _projectDataManager.DmxChannelList.Value.Add(new DmxChannelEntity
                {
                    Guid = Guid.NewGuid(),
                    OrderIndex = c.ChannelIndex,
                    InstancedFixtureReferenceGuid = data.Guid,
                    Universe = data.Universe,
                    ChannelOffset = channelCount,
                    ChannelName = c.ChannelName,
                    ChannelIndex = c.ChannelIndex,
                    ChannelValue = 0
                });
            });
        
            // TODO: プロジェクトデータに追加して
            // 現在選択中のUniverseでフィルタして表示する
            _projectDataManager.FixturePlacementList.Value.Add(data);

            var target = 
                _projectDataManager
                    .FixturePlacementList.Value.Where(x => x.Universe == _universe.Value)
                    .OrderBy(x => x.OrderIndex).ToList();
            
            _fixturePlacementList.SetValueAndForceNotify(target);

            UpdateFixturePlacementSelection(data.Guid);
        }
        
    }

}

