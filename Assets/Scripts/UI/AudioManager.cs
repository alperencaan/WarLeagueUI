using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<AudioManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("AudioManager");
                    instance = go.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer audioMixer;
    
    private const string MASTER_VOLUME = "MasterVolume";
    private const string SFX_VOLUME = "SFXVolume";
    
    private float masterVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        
        SetMasterVolume(masterVolume);
        SetSFXVolume(sfxVolume);
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        // Convert to decibels (dB) for AudioMixer
        float dB = Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20f;
        audioMixer.SetFloat(MASTER_VOLUME, dB);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        // Convert to decibels (dB) for AudioMixer
        float dB = Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20f;
        audioMixer.SetFloat(SFX_VOLUME, dB);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    // Test için örnek ses çalma metodu
    public void PlayTestSound()
    {
        // Burada test sesi çalabilirsiniz
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>()?.clip, Camera.main.transform.position);
    }
} 