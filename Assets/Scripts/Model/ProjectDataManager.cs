using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using com.kodai100.ArtNetApp.Entities;
using UniRx;

namespace com.kodai100.ArtNetApp.Models
{
    public class ProjectDataManager
    {

        public ReactiveProperty<int> ArtNetReceivePort = new(6454);

        public ReactiveProperty<IPAddress> ArtNetSendIp = new(IPAddress.Parse("127.0.0.1"));
        public ReactiveProperty<int> ArtNetSendPort = new(6454);
    
        public ReactiveProperty<List<FixturePresetEntity>> FixturePresetList = new();
        public ReactiveProperty<List<FixturePlacementEntity>> FixturePlacementList = new();
        public ReactiveProperty<List<DmxChannelEntity>> DmxChannelList = new();

        public void MockupProjectData()
        {
            FixturePresetList.Value = MockupData.FixturePresetMockData.ToList();
            FixturePlacementList.Value = MockupData.FixturePlacementMockData.ToList();
            DmxChannelList.Value = MockupData.DmxChannelMockData.ToList();
        }
    
  
    }

}

