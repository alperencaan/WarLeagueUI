using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCard : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI nameText;
    public Button selectButton;
    public TextMeshProUGUI buttonText;
    
    private bool isSelected = false;

    void Start()
    {
        if (selectButton != null)
        {
            selectButton.onClick.AddListener(OnSelectButton);
        }
    }

    public void OnSelectButton()
    {
        if (buttonText == null) return;
        
        isSelected = !isSelected;
        buttonText.text = isSelected ? "SEÇİLDİ" : "SEÇ";
        
        if (selectButton != null)
        {
            selectButton.image.color = isSelected ? new Color(1f, 0.7f, 0f) : new Color(1f, 0.5f, 0f);
        }
        
        Debug.Log($"{nameText.text} {(isSelected ? "seçildi!" : "seçimi kaldırıldı!")}");
    }

    public void SetupCard(string name, Sprite icon)
    {
        if (nameText != null) nameText.text = name;
        if (characterImage != null) characterImage.sprite = icon;
    }
} 