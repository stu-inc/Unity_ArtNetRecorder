using System;
using UnityEngine;

namespace com.kodai100.ArtNetApp.Entities
{
    [Serializable]
    public class FixturePlacementEntity : ReorderableEntity
    {
        public string Name;
        public int Universe;
        public int ChannelOffset;

        // どのプリセットを使っているのかのリファレンス
        // TODO: プリセットが消されたときはNullで残すか、プリセットが消されたらPlacementも消すか → 消さないのが妥当
        [SerializeField]
        private string _presetReferenceGuid;
        public Guid PresetReferenceGuid
        {
            get => Guid.Parse(_presetReferenceGuid);
            set => _presetReferenceGuid = value.ToString();
        }
    }
}