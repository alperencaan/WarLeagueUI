using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using WarLeagueUI.Models;
using WarLeagueUI.Views;

namespace WarLeague.Controllers
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance { get; private set; }

        [Header("Scene Names")]
        [SerializeField] private string _loginScene = "LoginScene";
        [SerializeField] private string _mainMenuScene = "MainMenuScene";
        [SerializeField] private string _registerScene = "RegisterScene";
        [SerializeField] private string _armyBuilderScene = "ArmyBuilder";

        [Header("Loading Settings")]
        [SerializeField] private float _minLoadingTime = 1f;
        [SerializeField] private LoadingScreenView loadingScreenView;

        private SceneModel sceneModel;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            sceneModel = new SceneModel();
        }

        public void LoadLoginScene()
        {
            StartCoroutine(LoadSceneAsync(sceneModel.LoginScene));
        }

        public void LoadMainMenuScene()
        {
            StartCoroutine(LoadSceneAsync(sceneModel.MainMenuScene));
        }

        public void LoadRegisterScene()
        {
            StartCoroutine(LoadSceneAsync(sceneModel.RegisterScene));
        }

        public void LoadArmyBuilderScene()
        {
            StartCoroutine(LoadSceneAsync(sceneModel.ArmyBuilderScene));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            sceneModel.SetLoading(true);
            loadingScreenView?.Show();

            float startTime = Time.time;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                Debug.Log($"Loading progress: {progress * 100}%");

                if (asyncLoad.progress >= 0.9f)
                {
                    float elapsedTime = Time.time - startTime;
                    if (elapsedTime >= _minLoadingTime)
                    {
                        asyncLoad.allowSceneActivation = true;
                    }
                }

                yield return null;
            }

            sceneModel.SetLoading(false);
            loadingScreenView?.Hide();
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
} 