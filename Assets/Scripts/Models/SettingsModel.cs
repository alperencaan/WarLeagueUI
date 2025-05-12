namespace WarLeagueUI.Models
{
    public class SettingsModel
    {
        public float MasterVolume { get; private set; }
        public float MusicVolume { get; private set; }
        public float Brightness { get; private set; }
        public bool TutorialTips { get; private set; }
        public bool AutoSave { get; private set; }
        public int Difficulty { get; private set; }

        public void SetMasterVolume(float value) => MasterVolume = value;
        public void SetMusicVolume(float value) => MusicVolume = value;
        public void SetBrightness(float value) => Brightness = value;
        public void SetTutorialTips(bool value) => TutorialTips = value;
        public void SetAutoSave(bool value) => AutoSave = value;
        public void SetDifficulty(int value) => Difficulty = value;
    }
} 