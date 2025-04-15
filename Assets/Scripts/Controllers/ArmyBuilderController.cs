using UnityEngine;
using System.Collections.Generic;
using TMPro;
using WarLeague.Interfaces;
using WarLeague.Views;

namespace WarLeague.Controllers
{
    public class ArmyBuilderController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private List<CharacterCardView> _characterCards = new List<CharacterCardView>();
        [SerializeField] private TextMeshProUGUI _selectionText;
        [SerializeField] private int _maxSelectedCharacters = 1;

        private ICharacterCard _selectedCard;
        
        public IReadOnlyList<ICharacterCard> CharacterCards => _characterCards.ConvertAll(card => card as ICharacterCard);
        public ICharacterCard SelectedCard => _selectedCard;
        public int MaxSelectedCharacters => _maxSelectedCharacters;

        private void Start()
        {
            InitializeCharacterCards();
        }

        private void InitializeCharacterCards()
        {
            var cards = GetComponentsInChildren<CharacterCardView>(true);
            _characterCards.AddRange(cards);

            foreach (var card in _characterCards)
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
                HandleNewSelection(card);
            }
            else
            {
                HandleDeselection(card);
            }

            UpdateSelectionText();
        }

        private void HandleNewSelection(ICharacterCard card)
        {
            if (_selectedCard != null && _selectedCard != card)
            {
                _selectedCard.SetSelected(false);
            }
            _selectedCard = card;
        }

        private void HandleDeselection(ICharacterCard card)
        {
            if (_selectedCard == card)
            {
                _selectedCard = null;
            }
        }

        private void UpdateSelectionText()
        {
            if (_selectionText == null) return;

            if (_selectedCard != null && _selectedCard.Character != null)
            {
                string characterName = _selectedCard.Character.Name;
                _selectionText.text = $"{characterName} SELECTED!";
                _selectionText.color = characterName == "CYGO" ? new Color(1f, 0.5f, 0f) : Color.green;
            }
            else
            {
                _selectionText.text = "Please select a character";
                _selectionText.color = Color.white;
            }
        }

        public bool IsCharacterSelected(string characterName)
        {
            return _selectedCard?.Character != null && 
                   _selectedCard.Character.Name == characterName;
        }

        private void OnDestroy()
        {
            foreach (var card in _characterCards)
            {
                if (card != null)
                {
                    card.OnSelectionChanged -= HandleCardSelection;
                }
            }
        }
    }
} 