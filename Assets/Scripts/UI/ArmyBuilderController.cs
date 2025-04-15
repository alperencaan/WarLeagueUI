using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ArmyBuilderController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private List<CharacterCard> _characterCards = new List<CharacterCard>();
    [SerializeField] private TextMeshProUGUI _selectionText;
    [SerializeField] private int _maxSelectedCharacters = 1;

    private CharacterCard _selectedCard;
    
    public IReadOnlyList<CharacterCard> CharacterCards => _characterCards;
    public CharacterCard SelectedCard => _selectedCard;
    public int MaxSelectedCharacters => _maxSelectedCharacters;

    private void Start()
    {
        InitializeCharacterCards();
    }

    private void InitializeCharacterCards()
    {
        CharacterCard[] cards = GetComponentsInChildren<CharacterCard>(true);
        _characterCards.AddRange(cards);

        foreach (var card in _characterCards)
        {
            card.OnSelectionChanged += HandleCardSelection;
        }

        UpdateSelectionText();
    }

    private void HandleCardSelection(CharacterCard card, bool isSelected)
    {
        if (isSelected)
        {
            HandleNewSelection(card);
        }
        else
        {
            HandleDeselection(card);
        }

        UpdateSelectionText();
    }

    private void HandleNewSelection(CharacterCard card)
    {
        if (_selectedCard != null && _selectedCard != card)
        {
            _selectedCard.SetSelected(false);
        }
        _selectedCard = card;
    }

    private void HandleDeselection(CharacterCard card)
    {
        if (_selectedCard == card)
        {
            _selectedCard = null;
        }
    }

    private void UpdateSelectionText()
    {
        if (_selectionText == null) return;

        if (_selectedCard != null)
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
        return _selectedCard != null && _selectedCard.Character.Name == characterName;
    }
} 