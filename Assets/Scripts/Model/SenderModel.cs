namespace com.kodai100.ArtNetApp.Models
{

    public partial class SenderModel : Model
    {

        public SenderModel(ProjectDataManager projectDataManager) : base(projectDataManager)
        {
            InitializeFixturePresetModel(projectDataManager);
            InitializeFixturePlacementModel(projectDataManager);
            InitializeDmxChannelModel(projectDataManager);
        }
    }


}
