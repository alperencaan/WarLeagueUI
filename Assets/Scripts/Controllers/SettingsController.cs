using UnityEngine;
using WarLeagueUI.Models;
using WarLeagueUI.Views;

namespace WarLeague.Controllers
{
    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private SettingsView settingsView;
        private SettingsModel settingsModel;

        private const string MUSIC_VOLUME_KEY = "MusicVolume";
        private const string BRIGHTNESS_KEY = "Brightness";
        private const string TUTORIAL_TIPS_KEY = "TutorialTips";
        private const string AUTO_SAVE_KEY = "AutoSave";
        private const string DIFFICULTY_KEY = "Difficulty";

        private void Awake()
        {
            settingsModel = new SettingsModel();
        }

        private void Start()
        {
            SetupDropdown();
            LoadSettings();
            SetupEventListeners();
            settingsView.HidePanel();
        }

        private void SetupDropdown()
        {
            if (settingsView.DifficultyDropdown != null)
            {
                settingsView.DifficultyDropdown.ClearOptions();
                settingsView.DifficultyDropdown.AddOptions(new System.Collections.Generic.List<string> { "Kolay", "Normal", "Zor" });
            }
        }

        private void SetupEventListeners()
        {
            settingsView.MasterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeChange);
            settingsView.MusicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChange);
            settingsView.BrightnessSlider.onValueChanged.AddListener(HandleBrightnessChange);
            settingsView.TutorialTipsToggle.onValueChanged.AddListener(HandleTutorialTipsChange);
            settingsView.AutoSaveToggle.onValueChanged.AddListener(HandleAutoSaveChange);
            settingsView.DifficultyDropdown.onValueChanged.AddListener(HandleDifficultyChange);
            settingsView.SaveButton.onClick.AddListener(HandleSaveSettings);
            settingsView.BackButton.onClick.AddListener(HandleBack);
        }

        private void LoadSettings()
        {
            settingsView.MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
            settingsView.MusicVolumeSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
            settingsView.BrightnessSlider.value = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, 1f);
            settingsView.TutorialTipsToggle.isOn = PlayerPrefs.GetInt(TUTORIAL_TIPS_KEY, 1) == 1;
            settingsView.AutoSaveToggle.isOn = PlayerPrefs.GetInt(AUTO_SAVE_KEY, 1) == 1;
            settingsView.DifficultyDropdown.value = PlayerPrefs.GetInt(DIFFICULTY_KEY, 1);
            UpdateUI();
        }

        private void UpdateUI()
        {
            settingsView.UpdateVolumeTexts(settingsView.MasterVolumeSlider.value, settingsView.MusicVolumeSlider.value);
            settingsView.UpdateBrightness(settingsView.BrightnessSlider.value);
        }

        private void HandleMasterVolumeChange(float value)
        {
            settingsModel.SetMasterVolume(value);
            AudioController.Instance.SetMasterVolume(value);
            settingsView.UpdateVolumeTexts(value, settingsView.MusicVolumeSlider.value);
        }

        private void HandleMusicVolumeChange(float value)
        {
            settingsModel.SetMusicVolume(value);
            AudioController.Instance.SetSFXVolume(value);
            settingsView.UpdateVolumeTexts(settingsView.MasterVolumeSlider.value, value);
        }

        private void HandleBrightnessChange(float value)
        {
            settingsModel.SetBrightness(value);
            settingsView.UpdateBrightness(value);
        }

        private void HandleTutorialTipsChange(bool value)
        {
            settingsModel.SetTutorialTips(value);
            PlayerPrefs.SetInt(TUTORIAL_TIPS_KEY, value ? 1 : 0);
        }

        private void HandleAutoSaveChange(bool value)
        {
            settingsModel.SetAutoSave(value);
            PlayerPrefs.SetInt(AUTO_SAVE_KEY, value ? 1 : 0);
        }

        private void HandleDifficultyChange(int value)
        {
            settingsModel.SetDifficulty(value);
            PlayerPrefs.SetInt(DIFFICULTY_KEY, value);
        }

        private void HandleSaveSettings()
        {
            PlayerPrefs.SetFloat("MasterVolume", settingsView.MasterVolumeSlider.value);
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, settingsView.MusicVolumeSlider.value);
            PlayerPrefs.SetFloat(BRIGHTNESS_KEY, settingsView.BrightnessSlider.value);
            PlayerPrefs.Save();
            ShowMessage("Ayarlar kaydedildi!");
        }

        private void HandleBack()
        {
            settingsView.HidePanel();
        }

        public void ShowSettings()
        {
            settingsView.ShowPanel();
        }

        private void ShowMessage(string message)
        {
            Debug.Log(message);
        }
    }
} 