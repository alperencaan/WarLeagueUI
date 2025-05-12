using UnityEngine;
using UnityEngine.UI;

namespace WarLeagueUI.Views
{
    public class AudioView : MonoBehaviour
    {
        public Slider MasterVolumeSlider;
        public Slider SFXVolumeSlider;
        public Button TestSoundButton;

        public void SetMasterVolumeSlider(float value)
        {
            if (MasterVolumeSlider != null)
                MasterVolumeSlider.value = value;
        }

        public void SetSFXVolumeSlider(float value)
        {
            if (SFXVolumeSlider != null)
                SFXVolumeSlider.value = value;
        }
    }
} 