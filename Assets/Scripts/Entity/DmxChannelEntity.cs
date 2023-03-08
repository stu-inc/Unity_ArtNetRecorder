using System;
using UnityEngine;

namespace com.kodai100.ArtNetApp.Entities
{
    [Serializable]
    public class DmxChannelEntity : ReorderableEntity
    {
        public string ChannelName;
        public int ChannelIndex;
        public int ChannelValue;

        [SerializeField]
        private string _instancedFixtureReferenceGuid;
        
        public Guid InstancedFixtureReferenceGuid
        {
            get => Guid.Parse(_instancedFixtureReferenceGuid);
            set => _instancedFixtureReferenceGuid = value.ToString();
        }
    }
}