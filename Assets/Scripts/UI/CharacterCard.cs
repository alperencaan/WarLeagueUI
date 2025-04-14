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
        }
        
        // Initialize as not selected
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
        Debug.Log($"{nameText.text} {(isSelected ? "selected!" : "deselected!")}");
    }

    private void UpdateVisuals(bool selected)
    {
        if (buttonText != null)
        {
            buttonText.text = selected ? "SELECTED" : "SELECT";
        }
        
        if (selectButton != null)
        {
            selectButton.image.color = selected ? new Color(1f, 0.7f, 0f) : new Color(1f, 0.5f, 0f);
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