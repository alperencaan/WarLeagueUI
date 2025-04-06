using UnityEngine;
using System.Collections;

public class UIAnimationHelper : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float fadeOutDuration = 0.3f;
    [SerializeField] private float scaleInDuration = 0.3f;
    
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void ShowWithAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(ShowAnimation());
    }

    public void HideWithAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(HideAnimation());
    }

    private IEnumerator ShowAnimation()
    {
        // Başlangıç değerleri
        canvasGroup.alpha = 0f;
        rectTransform.localScale = Vector3.zero;

        // Fade in ve scale animasyonu
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeInDuration;
            
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, normalizedTime);
            rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, normalizedTime);
            
            yield return null;
        }

        // Son değerleri ayarla
        canvasGroup.alpha = 1f;
        rectTransform.localScale = Vector3.one;
    }

    private IEnumerator HideAnimation()
    {
        float elapsedTime = 0f;
        Vector3 startScale = rectTransform.localScale;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeOutDuration;
            
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, normalizedTime);
            rectTransform.localScale = Vector3.Lerp(startScale, Vector3.zero, normalizedTime);
            
            yield return null;
        }

        // Son değerleri ayarla
        canvasGroup.alpha = 0f;
        rectTransform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void PunchScale()
    {
        StopAllCoroutines();
        StartCoroutine(PunchScaleAnimation());
    }

    private IEnumerator PunchScaleAnimation()
    {
        Vector3 originalScale = rectTransform.localScale;
        Vector3 targetScale = originalScale * 1.2f;

        // Scale up
        float elapsedTime = 0f;
        while (elapsedTime < scaleInDuration * 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / (scaleInDuration * 0.5f);
            
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, normalizedTime);
            yield return null;
        }

        // Scale back
        elapsedTime = 0f;
        while (elapsedTime < scaleInDuration * 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / (scaleInDuration * 0.5f);
            
            rectTransform.localScale = Vector3.Lerp(targetScale, originalScale, normalizedTime);
            yield return null;
        }

        rectTransform.localScale = originalScale;
    }
} 