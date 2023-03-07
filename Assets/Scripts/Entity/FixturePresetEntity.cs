using System;

namespace com.kodai100.ArtNetApp.Entities
{
    
    [Serializable]
    public class FixturePresetEntity : Entity
    {
        public string Manufacturer;
        public string FixtureName;
        public string Detail;
        public string Mode;
        public Channel[] Channels;
    }

    [Serializable]
    public class Channel
    {
        public int ChannelIndex;
        public string ChannelName;
    }

}

