using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer _audioMixer;
    
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string MASTER_VOLUME_MIXER = "MasterVolume";
    private const string SFX_VOLUME_MIXER = "SFXVolume";
    
    private float _masterVolume = 1f;
    private float _sfxVolume = 1f;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<AudioManager>();
                if (_instance == null)
                {
                    var go = new GameObject("AudioManager");
                    _instance = go.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    public float MasterVolume => _masterVolume;
    public float SFXVolume => _sfxVolume;

    private void Awake()
    {
        InitializeSingleton();
        LoadAudioSettings();
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

    private void LoadAudioSettings()
    {
        _masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f);
        _sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
        
        SetMasterVolume(_masterVolume);
        SetSFXVolume(_sfxVolume);
    }

    public void SetMasterVolume(float volume)
    {
        _masterVolume = Mathf.Clamp01(volume);
        float dB = ConvertToDecibels(_masterVolume);
        _audioMixer.SetFloat(MASTER_VOLUME_MIXER, dB);
        SaveVolumeSettings(MASTER_VOLUME_KEY, _masterVolume);
    }

    public void SetSFXVolume(float volume)
    {
        _sfxVolume = Mathf.Clamp01(volume);
        float dB = ConvertToDecibels(_sfxVolume);
        _audioMixer.SetFloat(SFX_VOLUME_MIXER, dB);
        SaveVolumeSettings(SFX_VOLUME_KEY, _sfxVolume);
    }

    private float ConvertToDecibels(float volume)
    {
        return Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20f;
    }

    private void SaveVolumeSettings(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume * _masterVolume);
        }
    }

    // Test için örnek ses çalma metodu
    public void PlayTestSound()
    {
        // Burada test sesi çalabilirsiniz
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>()?.clip, Camera.main.transform.position);
    }
} 