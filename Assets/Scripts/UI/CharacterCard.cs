using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCard : MonoBehaviour
{
    [Header("Card Elements")]
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button selectButton;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Image cardBorder;

    private bool isSelected = false;
    private ArmyBuilderManager armyBuilder;
    private CharacterData characterData;

    public void Initialize(CharacterData data, ArmyBuilderManager manager)
    {
        characterData = data;
        armyBuilder = manager;

        characterImage.sprite = data.icon;
        nameText.text = data.characterName;
        buttonText.text = "SAVAŞÇIYI SEÇ";

        selectButton.onClick.AddListener(OnSelectClicked);
    }

    private void OnSelectClicked()
    {
        if (!isSelected)
        {
            if (armyBuilder.SelectCharacter(characterData))
            {
                SetSelected(true);
            }
        }
        else
        {
            armyBuilder.DeselectCharacter(characterData);
            SetSelected(false);
        }
    }

    private void SetSelected(bool selected)
    {
        isSelected = selected;
        buttonText.text = selected ? "SEÇİLDİ!" : "SAVAŞÇIYI SEÇ";
        cardBorder.color = selected ? Color.green : new Color(1f, 0.65f, 0f); // Seçiliyse yeşil, değilse turuncu
    }
} 