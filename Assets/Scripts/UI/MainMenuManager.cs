using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button armyBuilderButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button aboutButton;

    private void Start()
    {
        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        // Her butona tıklama fonksiyonlarını ekliyoruz
        startButton.onClick.AddListener(OnStartClicked);
        armyBuilderButton.onClick.AddListener(OnArmyBuilderClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        aboutButton.onClick.AddListener(OnAboutClicked);
    }

    private void OnStartClicked()
    {
        Debug.Log("Start Game clicked");
        // TODO: Oyun sahnesine geçiş
        // SceneManager.LoadScene("GameScene");
    }

    private void OnArmyBuilderClicked()
    {
        Debug.Log("Army Builder clicked");
        // TODO: Army Builder sahnesine geçiş
        // SceneManager.LoadScene("ArmyBuilderScene");
    }

    private void OnSettingsClicked()
    {
        Debug.Log("Settings clicked");
        // TODO: Ayarlar panelini aç
    }

    private void OnAboutClicked()
    {
        Debug.Log("About clicked");
        // TODO: Hakkında panelini aç
    }
} 