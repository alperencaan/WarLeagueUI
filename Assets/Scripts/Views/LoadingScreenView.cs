using UnityEngine;

namespace WarLeagueUI.Views
{
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;

        public void Show()
        {
            if (loadingScreen != null)
                loadingScreen.SetActive(true);
        }

        public void Hide()
        {
            if (loadingScreen != null)
                loadingScreen.SetActive(false);
        }
    }
} 