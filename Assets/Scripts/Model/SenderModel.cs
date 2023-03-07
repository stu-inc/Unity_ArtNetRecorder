using System.Linq;
using UniRx;

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
            }).AddTo(_disposables);

            // 保存データがアップデートされたとき
            _projectDataManager.FixturePlacementList.Subscribe(list =>
            {
                var target = projectDataManager.FixturePlacementList.Value.Where(x => x.Universe == _universe.Value)
                    .ToList();
                _fixturePlacementList.SetValueAndForceNotify(target);
            }).AddTo(_disposables);

        }
    }


}
