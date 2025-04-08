using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ArmyBuilderManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform cardContainer;
    [SerializeField] private GameObject characterCardPrefab;
    [SerializeField] private TMP_Text selectionText;
    
    [Header("Characters")]
    [SerializeField] private List<CharacterData> availableCharacters;
    
    private List<CharacterData> selectedCharacters = new List<CharacterData>();
    private const int MAX_SELECTED_CHARACTERS = 3;

    private void Start()
    {
        InitializeCharacterCards();
    }

    private void InitializeCharacterCards()
    {
        foreach (var characterData in availableCharacters)
        {
            GameObject cardObj = Instantiate(characterCardPrefab, cardContainer);
            CharacterCard card = cardObj.GetComponent<CharacterCard>();
            card.Initialize(characterData, this);
        }
    }

    public bool SelectCharacter(CharacterData character)
    {
        if (selectedCharacters.Count >= MAX_SELECTED_CHARACTERS)
        {
            ShowWarning("Maksimum savaşçı sayısına ulaştınız!");
            return false;
        }

        selectedCharacters.Add(character);
        ShowSelectionMessage($"{character.characterName} SEÇİLDİ!");
        return true;
    }

    public void DeselectCharacter(CharacterData character)
    {
        if (selectedCharacters.Remove(character))
        {
            ShowSelectionMessage($"{character.characterName} SEÇİMİ KALDIRILDI!");
        }
    }

    private void ShowSelectionMessage(string message)
    {
        if (selectionText != null)
        {
            selectionText.text = message;
            Invoke("ClearSelectionMessage", 2f);
        }
    }

    private void ClearSelectionMessage()
    {
        if (selectionText != null)
        {
            selectionText.text = "";
        }
    }

    private void ShowWarning(string message)
    {
        Debug.LogWarning(message);
        // TODO: UI'da uyarı göster
    }

    public void SaveSelectedCharacters()
    {
        if (selectedCharacters.Count > 0)
        {
            // TODO: Seçili karakterleri kaydet
            Debug.Log($"Seçilen karakterler kaydedildi: {selectedCharacters.Count} karakter");
        }
        else
        {
            ShowWarning("Lütfen en az bir savaşçı seçin!");
        }
    }
} 