using UnityEngine;

namespace WarLeagueUI.Models
{
    public class PlayerModel
    {
        public string PlayerName { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public int Gold { get; private set; }

        public PlayerModel()
        {
            PlayerName = "Default Player";
            Level = 1;
            Experience = 0;
            Gold = 0;
        }

        public void SetPlayerName(string name)
        {
            PlayerName = name;
        }

        public void AddExperience(int amount)
        {
            Experience += amount;
            // Level up logic can be added here
        }

        public void AddGold(int amount)
        {
            Gold += amount;
        }
    }
} 