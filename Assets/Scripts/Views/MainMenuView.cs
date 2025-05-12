using UnityEngine;
using UnityEngine.UI;

namespace WarLeagueUI.Views
{
    public class MainMenuView : MonoBehaviour
    {
        public Button StartButton;
        public Button ArmyBuilderButton;
        public Button SettingsButton;
        public Button AboutButton;
        public GameObject SettingsPanel;
        public GameObject AboutPanel;

        public void ShowSettingsPanel()
        {
            if (SettingsPanel != null) SettingsPanel.SetActive(true);
            if (AboutPanel != null) AboutPanel.SetActive(false);
        }

        public void ShowAboutPanel()
        {
            if (SettingsPanel != null) SettingsPanel.SetActive(false);
            if (AboutPanel != null) AboutPanel.SetActive(true);
        }

        public void HidePanels()
        {
            if (SettingsPanel != null) SettingsPanel.SetActive(false);
            if (AboutPanel != null) AboutPanel.SetActive(false);
        }
    }
} 