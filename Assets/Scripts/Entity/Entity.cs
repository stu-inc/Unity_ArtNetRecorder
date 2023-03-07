using System;
using UnityEngine;

namespace com.kodai100.ArtNetApp.Entities
{
    [Serializable]
    public class Entity
    {
        [SerializeField] private string _guid;

        public Guid Guid
        {
            get => Guid.Parse(_guid);
            set => _guid = value.ToString();
        }
    }

}