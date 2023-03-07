using System;

namespace com.kodai100.ArtNetApp.Entities
{
    public class DmxChannelEntity : ReorderableEntity
    {
        public string ChannelName;
        public int ChannelIndex;
        public float ChannelValue;

        public Guid InstancedFixtureReferenceGuid;
    }
}