using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private TMP_Text _masterVolumeText;
    [SerializeField] private TMP_Text _musicVolumeText;

    [Header("Display Settings")]
    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private TMP_Text _brightnessText;
    [SerializeField] private Image _brightnessOverlay;

    [Header("Game Settings")]
    [SerializeField] private Toggle _tutorialTipsToggle;
    [SerializeField] private Toggle _autoSaveToggle;
    [SerializeField] private TMP_Dropdown _difficultyDropdown;

    [Header("UI Controls")]
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _backButton;

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
    }

    private void SetupDifficultyDropdown()
    {
        if (_difficultyDropdown != null)
        {
            _difficultyDropdown.ClearOptions();
            _difficultyDropdown.AddOptions(new List<string> { "Easy", "Normal", "Hard" });
        }
    }

    private void LoadSettings()
    {
        if (_masterVolumeSlider != null)
            _masterVolumeSlider.value = AudioManager.Instance.MasterVolume;
        
        if (_musicVolumeSlider != null)
            _musicVolumeSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.5f);
        
        if (_brightnessSlider != null)
        {
            _brightnessSlider.value = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, 1f);
            UpdateBrightness(_brightnessSlider.value);
        }

        if (_tutorialTipsToggle != null)
            _tutorialTipsToggle.isOn = PlayerPrefs.GetInt(TUTORIAL_TIPS_KEY, 1) == 1;
        
        if (_autoSaveToggle != null)
            _autoSaveToggle.isOn = PlayerPrefs.GetInt(AUTO_SAVE_KEY, 1) == 1;
        
        if (_difficultyDropdown != null)
            _difficultyDropdown.value = PlayerPrefs.GetInt(DIFFICULTY_KEY, 1);
    }

    private void SetupEventListeners()
    {
        if (_masterVolumeSlider != null)
            _masterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeChanged);
        
        if (_musicVolumeSlider != null)
            _musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChanged);
        
        if (_brightnessSlider != null)
            _brightnessSlider.onValueChanged.AddListener(HandleBrightnessChanged);
        
        if (_tutorialTipsToggle != null)
            _tutorialTipsToggle.onValueChanged.AddListener(HandleTutorialTipsChanged);
        
        if (_autoSaveToggle != null)
            _autoSaveToggle.onValueChanged.AddListener(HandleAutoSaveChanged);
        
        if (_difficultyDropdown != null)
            _difficultyDropdown.onValueChanged.AddListener(HandleDifficultyChanged);
        
        if (_saveButton != null)
            _saveButton.onClick.AddListener(SaveSettings);
        
        if (_backButton != null)
            _backButton.onClick.AddListener(HandleBack);
    }

    private void HandleMasterVolumeChanged(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
        UpdateUI();
    }

    private void HandleMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
        UpdateUI();
    }

    private void HandleBrightnessChanged(float value)
    {
        PlayerPrefs.SetFloat(BRIGHTNESS_KEY, value);
        UpdateBrightness(value);
        UpdateUI();
    }

    private void UpdateBrightness(float value)
    {
        if (_brightnessOverlay != null)
        {
            var color = _brightnessOverlay.color;
            color.a = 1f - value;
            _brightnessOverlay.color = color;
        }
    }

    private void HandleTutorialTipsChanged(bool enabled)
    {
        PlayerPrefs.SetInt(TUTORIAL_TIPS_KEY, enabled ? 1 : 0);
    }

    private void HandleAutoSaveChanged(bool enabled)
    {
        PlayerPrefs.SetInt(AUTO_SAVE_KEY, enabled ? 1 : 0);
    }

    private void HandleDifficultyChanged(int value)
    {
        PlayerPrefs.SetInt(DIFFICULTY_KEY, value);
    }

    private void SaveSettings()
    {
        PlayerPrefs.Save();
        // TODO: Show save confirmation feedback
    }

    private void HandleBack()
    {
        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (_masterVolumeText != null && _masterVolumeSlider != null)
            _masterVolumeText.text = $"{(_masterVolumeSlider.value * 100):F0}%";
        
        if (_musicVolumeText != null && _musicVolumeSlider != null)
            _musicVolumeText.text = $"{(_musicVolumeSlider.value * 100):F0}%";
        
        if (_brightnessText != null && _brightnessSlider != null)
            _brightnessText.text = $"{(_brightnessSlider.value * 100):F0}%";
    }
} 