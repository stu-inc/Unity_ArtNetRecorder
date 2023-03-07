using System;

namespace com.kodai100.ArtNetApp.Entities
{
    public class FixturePlacementEntity : ReorderableEntity
    {
        public string Name;
        public int Universe;
        public int ChannelOffset;

        // どのプリセットを使っているのかのリファレンス
        // TODO: プリセットが消されたときはNullで残すか、プリセットが消されたらPlacementも消すか → 消さないのが妥当
        public Guid PresetReferenceGuid;
    }
}