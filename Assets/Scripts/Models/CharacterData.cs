using UnityEngine;

namespace WarLeague.Models
{
    [System.Serializable]
    public class CharacterData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Sprite Icon { get; set; }
        public int Level { get; set; }
        public float Health { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float Speed { get; set; }

        public CharacterData(int id, string name, string description = "", Sprite icon = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Icon = icon;
            Level = 1;
            Health = 100;
            Attack = 10;
            Defense = 5;
            Speed = 5;
        }
    }
} 