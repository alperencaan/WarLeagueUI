using UnityEngine;
using TMPro;
using WarLeagueUI.Models;

namespace WarLeagueUI.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerNameText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private TextMeshProUGUI goldText;

        public void UpdateView(PlayerModel playerModel)
        {
            if (playerNameText != null) playerNameText.text = $"Player: {playerModel.PlayerName}";
            if (levelText != null) levelText.text = $"Level: {playerModel.Level}";
            if (experienceText != null) experienceText.text = $"XP: {playerModel.Experience}";
            if (goldText != null) goldText.text = $"Gold: {playerModel.Gold}";
        }
    }
} 