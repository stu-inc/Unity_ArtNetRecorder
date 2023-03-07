using System;

namespace com.kodai100.ArtNetApp.Entities
{
    [Serializable]
    public class ReorderableEntity : Entity
    {
        public int OrderIndex;
    }
}