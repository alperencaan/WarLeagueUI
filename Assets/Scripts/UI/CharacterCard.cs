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
    public Image cardBorder;
    
    private bool isSelected = false;
    public event Action<CharacterCard, bool> OnSelectionChanged;

    void Start()
    {
        if (selectButton != null)
        {
            selectButton.onClick.AddListener(OnSelectButton);
            buttonText.text = "SELECT";
            buttonText.fontSize = 28;
            buttonText.fontStyle = FontStyles.Bold;
        }

        if (nameText != null && string.IsNullOrEmpty(nameText.text))
        {
            nameText.text = "CYGO";
        }
        
        UpdateVisuals(false);
    }

    public void OnSelectButton()
    {
        SetSelected(!isSelected);
    }

    public void SetSelected(bool selected)
    {
        if (isSelected == selected) return;
        
        isSelected = selected;
        UpdateVisuals(selected);
        
        OnSelectionChanged?.Invoke(this, isSelected);
    }

    private void UpdateVisuals(bool selected)
    {
        if (buttonText != null)
        {
            buttonText.text = selected ? "SELECTED" : "SELECT";
            buttonText.fontSize = selected ? 32 : 28;
            buttonText.fontStyle = FontStyles.Bold;
            
            // Seçili durumda daha parlak turuncu, normal durumda beyaz
            Color selectedColor = new Color(1f, 0.6f, 0f, 1f);
            Color normalColor = new Color(1f, 1f, 1f, 0.9f);
            buttonText.color = selected ? selectedColor : normalColor;
        }
        
        if (selectButton != null)
        {
            // Buton arka planı için daha koyu renkler
            Color selectedBgColor = new Color(0.8f, 0.4f, 0f, 1f);
            Color normalBgColor = new Color(0.6f, 0.3f, 0f, 1f);
            selectButton.image.color = selected ? selectedBgColor : normalBgColor;
        }

        if (cardBorder != null)
        {
            cardBorder.color = selected ? new Color(1f, 0.7f, 0f, 1f) : new Color(1f, 0.5f, 0f, 0.5f);
        }
    }

    public void SetupCard(string name, Sprite icon)
    {
        if (nameText != null) nameText.text = name;
        if (characterImage != null) characterImage.sprite = icon;
        UpdateVisuals(false);
    }

    public bool IsSelected()
    {
        return isSelected;
    }
} 