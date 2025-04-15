using UnityEngine;
using WarLeague.Interfaces;

namespace WarLeague.Models
{
    public class Character : ICharacter
    {
        private readonly int _id;
        private readonly string _name;
        private readonly Sprite _icon;
        private readonly string _description;

        public int Id => _id;
        public string Name => _name;
        public Sprite Icon => _icon;
        public string Description => _description;

        public Character(int id, string name, Sprite icon, string description = "")
        {
            _id = id;
            _name = name;
            _icon = icon;
            _description = description;
        }
    }
} 