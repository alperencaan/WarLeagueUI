using UnityEngine;
using WarLeague.Interfaces;
using WarLeague.Views;
using WarLeagueUI.Models;
using WarLeagueUI.Views;

namespace WarLeague.Controllers
{
    public class ArmyBuilderController : MonoBehaviour
    {
        [SerializeField] private ArmyBuilderView armyBuilderView;
        [SerializeField] private int maxSelectedCharacters = 1;

        private ArmyBuilderModel armyBuilderModel;

        private void Awake()
        {
            armyBuilderModel = new ArmyBuilderModel(maxSelectedCharacters);
        }

        private void Start()
        {
            foreach (var card in armyBuilderView.CharacterCards)
            {
                if (card != null)
                {
                    card.OnSelectionChanged += HandleCardSelection;
                }
            }
            UpdateSelectionText();
        }

        private void HandleCardSelection(ICharacterCard card, bool isSelected)
        {
            if (isSelected && card != null)
            {
                if (armyBuilderModel.SelectedCard != null && armyBuilderModel.SelectedCard != card)
                {
                    armyBuilderModel.SelectedCard.SetSelected(false);
                }
                armyBuilderModel.SelectCard(card);
            }
            else
            {
                armyBuilderModel.DeselectCard(card);
            }
            UpdateSelectionText();
        }

        private void UpdateSelectionText()
        {
            var selectedCard = armyBuilderModel.SelectedCard;
            if (selectedCard != null && selectedCard.Character != null)
            {
                string characterName = selectedCard.Character.Name;
                armyBuilderView.UpdateSelectionText($"{characterName} SELECTED!", characterName == "CYGO" ? new Color(1f, 0.5f, 0f) : Color.green);
            }
            else
            {
                armyBuilderView.UpdateSelectionText("Please select a character", Color.white);
            }
        }

        private void OnDestroy()
        {
            foreach (var card in armyBuilderView.CharacterCards)
            {
                if (card != null)
                {
                    card.OnSelectionChanged -= HandleCardSelection;
                }
            }
        }
    }
} 