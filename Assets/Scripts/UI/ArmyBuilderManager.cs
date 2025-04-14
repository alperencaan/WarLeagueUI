using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ArmyBuilderManager : MonoBehaviour
{
    [System.Serializable]
    public class Character
    {
        public string name;
        public Sprite icon;
    }

    public List<CharacterCard> characterCards = new List<CharacterCard>();
    public TextMeshProUGUI selectionText;
    public int maxSelectedCharacters = 1;

    private CharacterCard selectedCard;

    void Start()
    {
        // Find all character cards and add to list
        CharacterCard[] cards = GetComponentsInChildren<CharacterCard>(true);
        characterCards.AddRange(cards);

        // Add selection event to each card
        foreach (var card in characterCards)
        {
            card.OnSelectionChanged += HandleCardSelection;
        }

        UpdateSelectionText();
    }

    void HandleCardSelection(CharacterCard card, bool isSelected)
    {
        if (isSelected)
        {
            // If another card is selected, deselect it
            if (selectedCard != null && selectedCard != card)
            {
                selectedCard.SetSelected(false);
            }
            selectedCard = card;
        }
        else
        {
            if (selectedCard == card)
            {
                selectedCard = null;
            }
        }

        UpdateSelectionText();
    }

    void UpdateSelectionText()
    {
        if (selectionText != null)
        {
            if (selectedCard != null)
            {
                string characterName = selectedCard.nameText.text;
                selectionText.text = $"{characterName} SELECTED!";
                selectionText.color = characterName == "CYGO" ? new Color(1f, 0.5f, 0f) : Color.green;
            }
            else
            {
                selectionText.text = "Please select a character";
                selectionText.color = Color.white;
            }
        }
    }

    public bool IsCharacterSelected(string characterName)
    {
        return selectedCard != null && selectedCard.nameText.text == characterName;
    }
} 