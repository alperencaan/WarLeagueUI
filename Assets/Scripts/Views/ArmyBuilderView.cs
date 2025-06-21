using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WarLeague.Views;

namespace WarLeagueUI.Views
{
    public class ArmyBuilderView : MonoBehaviour
    {
        [Header("UI References")]
        public List<CharacterCardView> CharacterCards;
        public TextMeshProUGUI SelectionText;

        public void UpdateSelectionText(string text, Color color)
        {
            if (SelectionText != null)
            {
                SelectionText.text = text;
                SelectionText.color = color;
            }
        }
    }
} 