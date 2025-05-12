namespace WarLeagueUI.Models
{
    public class SceneModel
    {
        public string LoginScene { get; set; } = "LoginScene";
        public string MainMenuScene { get; set; } = "MainMenuScene";
        public string RegisterScene { get; set; } = "RegisterScene";
        public string ArmyBuilderScene { get; set; } = "ArmyBuilder";
        public bool IsLoading { get; private set; }

        public void SetLoading(bool loading)
        {
            IsLoading = loading;
        }
    }
} 