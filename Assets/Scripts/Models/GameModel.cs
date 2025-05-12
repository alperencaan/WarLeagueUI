namespace WarLeagueUI.Models
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    public class GameModel
    {
        public GameState CurrentGameState { get; private set; }
        public bool IsGamePaused { get; private set; }

        public GameModel()
        {
            CurrentGameState = GameState.MainMenu;
            IsGamePaused = false;
        }

        public void SetGameState(GameState state)
        {
            CurrentGameState = state;
        }

        public void SetPaused(bool paused)
        {
            IsGamePaused = paused;
        }
    }
} 