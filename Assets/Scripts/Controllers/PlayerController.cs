using UnityEngine;
using WarLeagueUI.Models;
using WarLeagueUI.Views;

namespace WarLeagueUI.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerModel playerModel;
        [SerializeField] private PlayerView playerView;

        private void Awake()
        {
            playerModel = new PlayerModel();
        }

        private void Start()
        {
            UpdateView();
        }

        public void UpdatePlayerName(string newName)
        {
            playerModel.SetPlayerName(newName);
            UpdateView();
        }

        public void AddExperience(int amount)
        {
            playerModel.AddExperience(amount);
            UpdateView();
        }

        public void AddGold(int amount)
        {
            playerModel.AddGold(amount);
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