using UnityEngine;
using WarLeagueUI.Models;
using WarLeagueUI.Views;

namespace WarLeagueUI.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameView gameView;

        private GameModel gameModel;

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
            gameModel = new GameModel();
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            gameModel.SetGameState(GameState.MainMenu);
            gameModel.SetPaused(false);
            Time.timeScale = 1f;
        }

        public void StartGame()
        {
            gameModel.SetGameState(GameState.Playing);
            // Oyun başlangıç mantığı
        }

        public void PauseGame()
        {
            gameModel.SetPaused(true);
            Time.timeScale = 0f;
            gameModel.SetGameState(GameState.Paused);
        }

        public void ResumeGame()
        {
            gameModel.SetPaused(false);
            Time.timeScale = 1f;
            gameModel.SetGameState(GameState.Playing);
        }

        public void EndGame()
        {
            gameModel.SetGameState(GameState.GameOver);
            // Oyun sonu mantığı
        }
    }

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }
} 