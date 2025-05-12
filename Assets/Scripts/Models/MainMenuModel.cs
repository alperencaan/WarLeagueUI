namespace WarLeagueUI.Models
{
    public class MainMenuModel
    {
        public bool IsSettingsPanelOpen { get; private set; }
        public bool IsAboutPanelOpen { get; private set; }

        public void OpenSettingsPanel()
        {
            IsSettingsPanelOpen = true;
            IsAboutPanelOpen = false;
        }

        public void OpenAboutPanel()
        {
            IsSettingsPanelOpen = false;
            IsAboutPanelOpen = true;
        }

        public void ClosePanels()
        {
            IsSettingsPanelOpen = false;
            IsAboutPanelOpen = false;
        }
    }
} 