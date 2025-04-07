using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [Header("Ses Ayarları")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private TMP_Text masterVolumeText;
    [SerializeField] private TMP_Text musicVolumeText;

    [Header("Parlaklık Ayarları")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TMP_Text brightnessText;
    [SerializeField] private Image brightnessOverlay;

    [Header("Oyun Ayarları")]
    [SerializeField] private Toggle tutorialTipsToggle;
    [SerializeField] private Toggle autoSaveToggle;
    [SerializeField] private TMP_Dropdown difficultyDropdown;

    [Header("UI Butonları")]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button backButton;

    private void Start()
    {
        LoadSettings();
        SetupListeners();
        SetupDifficultyDropdown();
    }

    private void SetupDifficultyDropdown()
    {
        difficultyDropdown.ClearOptions();
        difficultyDropdown.AddOptions(new List<string> { "Kolay", "Orta", "Zor" });
    }

    private void LoadSettings()
    {
        masterVolumeSlider.value = AudioManager.Instance.GetMasterVolume();
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1f);
        UpdateBrightness(brightnessSlider.value);
        UpdateUI();
    }

    private void SetupListeners()
    {
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);

        tutorialTipsToggle.onValueChanged.AddListener(OnTutorialTipsChanged);
        autoSaveToggle.onValueChanged.AddListener(OnAutoSaveChanged);
        difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);

        saveButton.onClick.AddListener(SaveSettings);
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnMasterVolumeChanged(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
        UpdateUI();
    }

    private void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        UpdateUI();
    }

    private void OnBrightnessChanged(float value)
    {
        PlayerPrefs.SetFloat("Brightness", value);
        UpdateBrightness(value);
        UpdateUI();
    }

    private void UpdateBrightness(float brightnessValue)
    {
        if (brightnessOverlay != null)
        {
            Color color = brightnessOverlay.color;
            color.a = 1f - brightnessValue;
            brightnessOverlay.color = color;
        }
    }

    private void OnTutorialTipsChanged(bool enabled)
    {
        PlayerPrefs.SetInt("TutorialTips", enabled ? 1 : 0);
    }

    private void OnAutoSaveChanged(bool enabled)
    {
        PlayerPrefs.SetInt("AutoSave", enabled ? 1 : 0);
    }

    private void OnDifficultyChanged(int value)
    {
        PlayerPrefs.SetInt("Difficulty", value);
    }

    private void SaveSettings()
    {
        PlayerPrefs.Save();
        // TODO: Implement save feedback (örn: "Ayarlar kaydedildi" mesajı)
    }

    private void OnBackButtonClicked()
    {
        // TODO: Implement back button functionality
        // Örneğin: SceneManager.LoadScene("MainMenu");
    }

    private void UpdateUI()
    {
        masterVolumeText.text = $"{(masterVolumeSlider.value * 100):F0}%";
        musicVolumeText.text = $"{(musicVolumeSlider.value * 100):F0}%";
        brightnessText.text = $"{(brightnessSlider.value * 100):F0}%";
    }
} 