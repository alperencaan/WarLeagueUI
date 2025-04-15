using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace WarLeague.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Menu Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _armyBuilderButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _aboutButton;

        private const string GAME_SCENE = "GameScene";
        private const string ARMY_BUILDER_SCENE = "ArmyBuilderScene";

        [Header("UI Panels")]
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _aboutPanel;

        private void Start()
        {
            InitializeUI();
            SetupButtonListeners();
        }

        private void InitializeUI()
        {
            if (_settingsPanel != null) _settingsPanel.SetActive(false);
            if (_aboutPanel != null) _aboutPanel.SetActive(false);
        }

        private void SetupButtonListeners()
        {
            if (_startButton != null) _startButton.onClick.AddListener(HandleStartGame);
            if (_armyBuilderButton != null) _armyBuilderButton.onClick.AddListener(HandleArmyBuilder);
            if (_settingsButton != null) _settingsButton.onClick.AddListener(HandleSettings);
            if (_aboutButton != null) _aboutButton.onClick.AddListener(HandleAbout);
        }

        private void HandleStartGame()
        {
            LoadScene(GAME_SCENE);
        }

        private void HandleArmyBuilder()
        {
            LoadScene(ARMY_BUILDER_SCENE);
        }

        private void HandleSettings()
        {
            if (_settingsPanel != null)
            {
                _settingsPanel.SetActive(true);
                if (_aboutPanel != null) _aboutPanel.SetActive(false);
            }
        }

        private void HandleAbout()
        {
            if (_aboutPanel != null)
            {
                _aboutPanel.SetActive(true);
                if (_settingsPanel != null) _settingsPanel.SetActive(false);
            }
        }

        private void LoadScene(string sceneName)
        {
            try
            {
                SceneManager.LoadScene(sceneName);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load {sceneName}: {e.Message}");
            }
        }
    }
} 