using UnityEngine;
using UnityEngine.Audio;
using WarLeagueUI.Models;
using WarLeagueUI.Views;

namespace WarLeague.Controllers
{
    public class AudioController : MonoBehaviour
    {
        private static AudioController _instance;
        
        [Header("Ses Ayarları")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioView audioView;
        
        private const string MASTER_VOLUME_KEY = "MasterVolume";
        private const string SFX_VOLUME_KEY = "SFXVolume";
        private const string MASTER_VOLUME_MIXER = "MasterVolume";
        private const string SFX_VOLUME_MIXER = "SFXVolume";
        
        private AudioModel audioModel;

        public static AudioController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<AudioController>();
                    if (_instance == null)
                    {
                        var go = new GameObject("AudioController");
                        _instance = go.AddComponent<AudioController>();
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            InitializeSingleton();
            float master = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f);
            float sfx = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
            audioModel = new AudioModel(master, sfx);
        }

        private void Start()
        {
            ApplyVolumesToMixer();
            if (audioView != null)
            {
                audioView.SetMasterVolumeSlider(audioModel.MasterVolume);
                audioView.SetSFXVolumeSlider(audioModel.SFXVolume);
                audioView.MasterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
                audioView.SFXVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
                audioView.TestSoundButton.onClick.AddListener(PlayTestSound);
            }
        }

        private void InitializeSingleton()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void SetMasterVolume(float volume)
        {
            audioModel.SetMasterVolume(volume);
            float dB = ConvertToDecibels(audioModel.MasterVolume);
            audioMixer.SetFloat(MASTER_VOLUME_MIXER, dB);
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, audioModel.MasterVolume);
            PlayerPrefs.Save();
        }

        public void SetSFXVolume(float volume)
        {
            audioModel.SetSFXVolume(volume);
            float dB = ConvertToDecibels(audioModel.SFXVolume);
            audioMixer.SetFloat(SFX_VOLUME_MIXER, dB);
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, audioModel.SFXVolume);
            PlayerPrefs.Save();
        }

        private void ApplyVolumesToMixer()
        {
            audioMixer.SetFloat(MASTER_VOLUME_MIXER, ConvertToDecibels(audioModel.MasterVolume));
            audioMixer.SetFloat(SFX_VOLUME_MIXER, ConvertToDecibels(audioModel.SFXVolume));
        }

        private float ConvertToDecibels(float volume)
        {
            return Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20f;
        }

        public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
        {
            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, position, volume * audioModel.MasterVolume);
            }
        }

        public void PlayTestSound()
        {
            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>()?.clip, Camera.main.transform.position);
        }
    }
} 