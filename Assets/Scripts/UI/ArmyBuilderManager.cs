using UnityEngine;
using System.Collections.Generic;

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
    }

    void CreateCharacterCard(Character character)
    {
        CharacterCard card = Instantiate(characterCardPrefab, cardContainer);
        card.SetupCard(character.name, character.icon);
    }
} 