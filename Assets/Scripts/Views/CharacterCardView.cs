using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using WarLeague.Interfaces;

namespace WarLeague.Views
{
    [RequireComponent(typeof(Button))]
    public class CharacterCardView : MonoBehaviour, ICharacterCard
    {
        [Header("UI Components")]
        [SerializeField] private Image _characterImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Button _selectButton;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Image _cardBorder;

        private ICharacter _character;
        private bool _isSelected;

        public ICharacter Character => _character;
        public bool IsSelected => _isSelected;
        
        public event Action<ICharacterCard, bool> OnSelectionChanged;

        private void Awake()
        {
            ValidateComponents();
        }

        private void ValidateComponents()
        {
            if (_selectButton == null) _selectButton = GetComponent<Button>();
            if (_characterImage == null) _characterImage = GetComponentInChildren<Image>();
            if (_nameText == null) _nameText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_selectButton != null)
            {
                _selectButton.onClick.AddListener(OnSelectButton);
                UpdateButtonText(false);
            }
            UpdateVisuals(false);
        }

        public void Initialize(ICharacter character)
        {
            _character = character;
            UpdateCardInfo();
        }

        private void UpdateCardInfo()
        {
            if (_character == null) return;
            
            if (_nameText != null) _nameText.text = _character.Name;
            if (_characterImage != null) _characterImage.sprite = _character.Icon;
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
            if (_buttonText == null) return;
            
            _buttonText.text = selected ? "SELECTED" : "SELECT";
            _buttonText.fontSize = selected ? 32 : 28;
            _buttonText.fontStyle = FontStyles.Bold;
            _buttonText.color = selected ? new Color(1f, 0.6f, 0f, 1f) : new Color(1f, 1f, 1f, 0.9f);
        }

        private void UpdateVisuals(bool selected)
        {
            if (_selectButton != null)
            {
                _selectButton.image.color = selected ? 
                    new Color(0.8f, 0.4f, 0f, 1f) : 
                    new Color(0.6f, 0.3f, 0f, 1f);
            }

            if (_cardBorder != null)
            {
                _cardBorder.color = selected ? 
                    new Color(1f, 0.7f, 0f, 1f) : 
                    new Color(1f, 0.5f, 0f, 0.5f);
            }
        }

        private void OnDestroy()
        {
            if (_selectButton != null)
            {
                _selectButton.onClick.RemoveListener(OnSelectButton);
            }
        }
    }
} 