using UnityEngine;
using UnityEngine.SceneManagement;
using WarLeagueUI.Models;
using WarLeagueUI.Views;

namespace WarLeague.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainMenuView mainMenuView;
        private MainMenuModel mainMenuModel;

        private const string GAME_SCENE = "GameScene";
        private const string ARMY_BUILDER_SCENE = "ArmyBuilderScene";

        private void Awake()
        {
            mainMenuModel = new MainMenuModel();
        }

        private void Start()
        {
            mainMenuView.StartButton.onClick.AddListener(HandleStartGame);
            mainMenuView.ArmyBuilderButton.onClick.AddListener(HandleArmyBuilder);
            mainMenuView.SettingsButton.onClick.AddListener(HandleSettings);
            mainMenuView.AboutButton.onClick.AddListener(HandleAbout);
            mainMenuView.HidePanels();
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
            mainMenuModel.OpenSettingsPanel();
            mainMenuView.ShowSettingsPanel();
        }

        private void HandleAbout()
        {
            mainMenuModel.OpenAboutPanel();
            mainMenuView.ShowAboutPanel();
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