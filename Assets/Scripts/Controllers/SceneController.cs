using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        [SerializeField] private GameObject _loadingScreen;

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
        }

        public void LoadLoginScene()
        {
            StartCoroutine(LoadSceneAsync(_loginScene));
        }

        public void LoadMainMenuScene()
        {
            StartCoroutine(LoadSceneAsync(_mainMenuScene));
        }

        public void LoadRegisterScene()
        {
            StartCoroutine(LoadSceneAsync(_registerScene));
        }

        public void LoadArmyBuilderScene()
        {
            StartCoroutine(LoadSceneAsync(_armyBuilderScene));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            if (_loadingScreen != null)
            {
                _loadingScreen.SetActive(true);
            }

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

            if (_loadingScreen != null)
            {
                _loadingScreen.SetActive(false);
            }
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