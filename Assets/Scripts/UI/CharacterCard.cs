using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterCard : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI nameText;
    public Button selectButton;
    public TextMeshProUGUI buttonText;
    
    private bool isSelected = false;
    public event Action<CharacterCard, bool> OnSelectionChanged;

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
        
        SetSelected(!isSelected);
    }

    public void SetSelected(bool selected)
    {
        if (isSelected == selected) return;
        
        isSelected = selected;
        buttonText.text = isSelected ? "SEÇİLDİ" : "SEÇ";
        
        if (selectButton != null)
        {
            selectButton.image.color = isSelected ? new Color(1f, 0.7f, 0f) : new Color(1f, 0.5f, 0f);
        }
        
        OnSelectionChanged?.Invoke(this, isSelected);
        Debug.Log($"{nameText.text} {(isSelected ? "seçildi!" : "seçimi kaldırıldı!")}");
    }

    public void SetupCard(string name, Sprite icon)
    {
        if (nameText != null) nameText.text = name;
        if (characterImage != null) characterImage.sprite = icon;
    }

    public bool IsSelected()
    {
        return isSelected;
    }
} 