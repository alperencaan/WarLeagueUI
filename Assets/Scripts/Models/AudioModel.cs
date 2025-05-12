namespace WarLeagueUI.Models
{
    public class AudioModel
    {
        public float MasterVolume { get; private set; }
        public float SFXVolume { get; private set; }

        public AudioModel(float masterVolume = 1f, float sfxVolume = 1f)
        {
            MasterVolume = masterVolume;
            SFXVolume = sfxVolume;
        }

        public void SetMasterVolume(float volume)
        {
            MasterVolume = UnityEngine.Mathf.Clamp01(volume);
        }

        public void SetSFXVolume(float volume)
        {
            SFXVolume = UnityEngine.Mathf.Clamp01(volume);
        }
    }
} 