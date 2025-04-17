using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace WarLeague.Controllers
{
    public class SettingsController : MonoBehaviour
    {
        [Header("Ses Ayarları")]
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private TMP_Text _masterVolumeText;
        [SerializeField] private TMP_Text _musicVolumeText;

        [Header("Görüntü Ayarları")]
        [SerializeField] private Slider _brightnessSlider;
        [SerializeField] private TMP_Text _brightnessText;
        [SerializeField] private Image _brightnessOverlay;

        [Header("Oyun Ayarları")]
        [SerializeField] private Toggle _tutorialTipsToggle;
        [SerializeField] private Toggle _autoSaveToggle;
        [SerializeField] private TMP_Dropdown _difficultyDropdown;

        [Header("UI Kontrolleri")]
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private CanvasGroup _settingsPanel;

        private const string MUSIC_VOLUME_KEY = "MusicVolume";
        private const string BRIGHTNESS_KEY = "Brightness";
        private const string TUTORIAL_TIPS_KEY = "TutorialTips";
        private const string AUTO_SAVE_KEY = "AutoSave";
        private const string DIFFICULTY_KEY = "Difficulty";

        private void Start()
        {
            InitializeUI();
            LoadSettings();
            SetupEventListeners();
        }

        private void InitializeUI()
        {
            SetupDifficultyDropdown();
            UpdateUI();
            if (_settingsPanel != null)
            {
                _settingsPanel.alpha = 0;
                _settingsPanel.interactable = false;
                _settingsPanel.blocksRaycasts = false;
            }
        }

        private void SetupDifficultyDropdown()
        {
            if (_difficultyDropdown != null)
            {
                _difficultyDropdown.ClearOptions();
                _difficultyDropdown.AddOptions(new List<string> { "Kolay", "Normal", "Zor" });
            }
        }

        private void SetupEventListeners()
        {
            if (_masterVolumeSlider != null)
                _masterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeChange);
            if (_musicVolumeSlider != null)
                _musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChange);
            if (_brightnessSlider != null)
                _brightnessSlider.onValueChanged.AddListener(HandleBrightnessChange);
            if (_tutorialTipsToggle != null)
                _tutorialTipsToggle.onValueChanged.AddListener(HandleTutorialTipsChange);
            if (_autoSaveToggle != null)
                _autoSaveToggle.onValueChanged.AddListener(HandleAutoSaveChange);
            if (_difficultyDropdown != null)
                _difficultyDropdown.onValueChanged.AddListener(HandleDifficultyChange);
            if (_saveButton != null)
                _saveButton.onClick.AddListener(HandleSaveSettings);
            if (_backButton != null)
                _backButton.onClick.AddListener(HandleBack);
        }

        private void LoadSettings()
        {
            if (_masterVolumeSlider != null)
                _masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
            if (_musicVolumeSlider != null)
                _musicVolumeSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
            if (_brightnessSlider != null)
                _brightnessSlider.value = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, 1f);
            if (_tutorialTipsToggle != null)
                _tutorialTipsToggle.isOn = PlayerPrefs.GetInt(TUTORIAL_TIPS_KEY, 1) == 1;
            if (_autoSaveToggle != null)
                _autoSaveToggle.isOn = PlayerPrefs.GetInt(AUTO_SAVE_KEY, 1) == 1;
            if (_difficultyDropdown != null)
                _difficultyDropdown.value = PlayerPrefs.GetInt(DIFFICULTY_KEY, 1);
        }

        private void UpdateUI()
        {
            UpdateVolumeTexts();
            UpdateBrightness();
        }

        private void UpdateVolumeTexts()
        {
            if (_masterVolumeText != null)
                _masterVolumeText.text = $"Ses: {(_masterVolumeSlider.value * 100):F0}%";
            if (_musicVolumeText != null)
                _musicVolumeText.text = $"Müzik: {(_musicVolumeSlider.value * 100):F0}%";
        }

        private void UpdateBrightness()
        {
            if (_brightnessText != null)
                _brightnessText.text = $"Parlaklık: {(_brightnessSlider.value * 100):F0}%";
            if (_brightnessOverlay != null)
            {
                var color = _brightnessOverlay.color;
                color.a = 1 - _brightnessSlider.value;
                _brightnessOverlay.color = color;
            }
        }

        private void HandleMasterVolumeChange(float value)
        {
            AudioController.Instance.SetMasterVolume(value);
            UpdateVolumeTexts();
        }

        private void HandleMusicVolumeChange(float value)
        {
            AudioController.Instance.SetSFXVolume(value);
            UpdateVolumeTexts();
        }

        private void HandleBrightnessChange(float value)
        {
            UpdateBrightness();
        }

        private void HandleTutorialTipsChange(bool value)
        {
            PlayerPrefs.SetInt(TUTORIAL_TIPS_KEY, value ? 1 : 0);
        }

        private void HandleAutoSaveChange(bool value)
        {
            PlayerPrefs.SetInt(AUTO_SAVE_KEY, value ? 1 : 0);
        }

        private void HandleDifficultyChange(int value)
        {
            PlayerPrefs.SetInt(DIFFICULTY_KEY, value);
        }

        private void HandleSaveSettings()
        {
            PlayerPrefs.Save();
            ShowMessage("Ayarlar kaydedildi!");
        }

        private void HandleBack()
        {
            if (_settingsPanel != null)
            {
                _settingsPanel.alpha = 0;
                _settingsPanel.interactable = false;
                _settingsPanel.blocksRaycasts = false;
            }
        }

        public void ShowSettings()
        {
            if (_settingsPanel != null)
            {
                _settingsPanel.alpha = 1;
                _settingsPanel.interactable = true;
                _settingsPanel.blocksRaycasts = true;
            }
        }

        private void ShowMessage(string message)
        {
            // TODO: Implement message display
            Debug.Log(message);
        }
    }
} 