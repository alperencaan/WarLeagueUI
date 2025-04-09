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

    public List<Character> availableCharacters = new List<Character>();
    public CharacterCard characterCardPrefab;
    public Transform cardContainer;
    public TextMeshProUGUI selectionText;
    public int maxSelectedCharacters = 1; // 1 karakter seçilebilir

    private CharacterCard selectedCard;

    void Start()
    {
        if (characterCardPrefab == null || cardContainer == null)
        {
            Debug.LogError("Lütfen CharacterCard prefab ve CardContainer referanslarını atayın!");
            return;
        }

        foreach (var character in availableCharacters)
        {
            CreateCharacterCard(character);
        }

        UpdateSelectionText();
    }

    void CreateCharacterCard(Character character)
    {
        CharacterCard card = Instantiate(characterCardPrefab, cardContainer);
        card.SetupCard(character.name, character.icon);
        card.OnSelectionChanged += HandleCardSelection;
    }

    void HandleCardSelection(CharacterCard card, bool isSelected)
    {
        if (isSelected)
        {
            // Eğer başka bir kart seçiliyse, onu seçimden çıkar
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
            string selectedName = selectedCard != null ? selectedCard.nameText.text : "Yok";
            selectionText.text = "Seçilen Karakter: " + selectedName;
        }
    }

    // Karakter seçimini kontrol etmek için
    public bool IsCharacterSelected(string characterName)
    {
        return selectedCard != null && selectedCard.nameText.text == characterName;
    }
} 