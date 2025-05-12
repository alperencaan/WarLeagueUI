using UnityEngine;
using WarLeagueUI.Models;
using WarLeagueUI.Views;

namespace WarLeagueUI.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerModel playerModel;
        private PlayerView playerView;

        private void Start()
        {
            playerModel = new PlayerModel();
            playerView = GetComponent<PlayerView>();
            
            if (playerView != null)
            {
                UpdateView();
            }
        }

        public void UpdatePlayerName(string newName)
        {
            playerModel.PlayerName = newName;
            UpdateView();
        }

        public void AddExperience(int amount)
        {
            playerModel.Experience += amount;
            // Level up logic could be added here
            UpdateView();
        }

        public void AddGold(int amount)
        {
            playerModel.Gold += amount;
            UpdateView();
        }

        private void UpdateView()
        {
            if (playerView != null)
            {
                playerView.UpdateView(playerModel);
            }
        }
    }
} 