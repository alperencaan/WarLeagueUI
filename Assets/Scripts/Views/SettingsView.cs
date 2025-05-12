using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WarLeagueUI.Views
{
    public class SettingsView : MonoBehaviour
    {
        public Slider MasterVolumeSlider;
        public Slider MusicVolumeSlider;
        public TMP_Text MasterVolumeText;
        public TMP_Text MusicVolumeText;
        public Slider BrightnessSlider;
        public TMP_Text BrightnessText;
        public Image BrightnessOverlay;
        public Toggle TutorialTipsToggle;
        public Toggle AutoSaveToggle;
        public TMP_Dropdown DifficultyDropdown;
        public Button SaveButton;
        public Button BackButton;
        public CanvasGroup SettingsPanel;

        public void UpdateVolumeTexts(float master, float music)
        {
            if (MasterVolumeText != null)
                MasterVolumeText.text = $"Ses: {(master * 100):F0}%";
            if (MusicVolumeText != null)
                MusicVolumeText.text = $"Müzik: {(music * 100):F0}%";
        }

        public void UpdateBrightness(float value)
        {
            if (BrightnessText != null)
                BrightnessText.text = $"Parlaklık: {(value * 100):F0}%";
            if (BrightnessOverlay != null)
            {
                var color = BrightnessOverlay.color;
                color.a = 1 - value;
                BrightnessOverlay.color = color;
            }
        }

        public void ShowPanel()
        {
            if (SettingsPanel != null)
            {
                SettingsPanel.alpha = 1;
                SettingsPanel.interactable = true;
                SettingsPanel.blocksRaycasts = true;
            }
        }

        public void HidePanel()
        {
            if (SettingsPanel != null)
            {
                SettingsPanel.alpha = 0;
                SettingsPanel.interactable = false;
                SettingsPanel.blocksRaycasts = false;
            }
        }
    }
} 