using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ArmyBuilderController : MonoBehaviour
{
    [System.Serializable]
    public class Character
    {
        private string _name;        
        private Sprite _icon;      
        
        
        public string Name { get; set; }
        public Sprite Icon { get; set; }
    }

    public List<CharacterCard> characterCards = new List<CharacterCard>();
    public TextMeshProUGUI selectionText;
    public int maxSelectedCharacters = 1;

    private CharacterCard selectedCard;

    void Start()
    {
        
        CharacterCard[] cards = GetComponentsInChildren<CharacterCard>(true);
        characterCards.AddRange(cards);

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