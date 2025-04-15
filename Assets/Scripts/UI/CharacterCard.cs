using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterCard : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image cardBorder;
    
    private bool _isSelected = false;
    private Character _character;
    
    public event Action<CharacterCard, bool> OnSelectionChanged;
    
    public Character Character => _character;
    public bool IsSelected => _isSelected;

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        if (selectButton != null)
        {
            selectButton.onClick.AddListener(OnSelectButton);
            UpdateButtonText(false);
        }
        UpdateVisuals(false);
    }

    public void Initialize(Character character)
    {
        _character = character;
        UpdateCardInfo();
    }

    private void UpdateCardInfo()
    {
        if (_character == null) return;
        
        if (nameText != null) nameText.text = _character.Name;
        if (characterImage != null) characterImage.sprite = _character.Icon;
    }

    private void OnSelectButton()
    {
        SetSelected(!_isSelected);
    }

    public void SetSelected(bool selected)
    {
        if (_isSelected == selected) return;
        
        _isSelected = selected;
        UpdateVisuals(selected);
        UpdateButtonText(selected);
        
        OnSelectionChanged?.Invoke(this, _isSelected);
    }

    private void UpdateButtonText(bool selected)
    {
        if (buttonText == null) return;
        
        buttonText.text = selected ? "SELECTED" : "SELECT";
        buttonText.fontSize = selected ? 32 : 28;
        buttonText.fontStyle = FontStyles.Bold;
        buttonText.color = selected ? new Color(1f, 0.6f, 0f, 1f) : new Color(1f, 1f, 1f, 0.9f);
    }

    private void UpdateVisuals(bool selected)
    {
        if (selectButton != null)
        {
            selectButton.image.color = selected ? 
                new Color(0.8f, 0.4f, 0f, 1f) : 
                new Color(0.6f, 0.3f, 0f, 1f);
        }

        if (cardBorder != null)
        {
            cardBorder.color = selected ? 
                new Color(1f, 0.7f, 0f, 1f) : 
                new Color(1f, 0.5f, 0f, 0.5f);
        }
    }
} 