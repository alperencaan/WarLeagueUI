using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WarLeague.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private Image _loadingBar;
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private GameObject _loadingPanel;

        private void Awake()
        {
            if (_loadingPanel != null)
            {
                _loadingPanel.SetActive(false);
            }
        }

        public void ShowLoadingScreen()
        {
            if (_loadingPanel != null)
            {
                _loadingPanel.SetActive(true);
            }
        }

        public void HideLoadingScreen()
        {
            if (_loadingPanel != null)
            {
                _loadingPanel.SetActive(false);
            }
        }

        public void UpdateLoadingProgress(float progress)
        {
            if (_loadingBar != null)
            {
                _loadingBar.fillAmount = progress;
            }

            if (_loadingText != null)
            {
                _loadingText.text = $"Loading... {Mathf.RoundToInt(progress * 100)}%";
            }
        }
    }
} 