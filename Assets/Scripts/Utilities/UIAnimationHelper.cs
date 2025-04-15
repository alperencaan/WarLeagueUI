using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

namespace WarLeague.Utilities
{
    public class UIAnimationHelper : MonoBehaviour
    {
        public static void FadeIn(CanvasGroup canvasGroup, float duration = 0.3f)
        {
            canvasGroup.DOFade(1f, duration);
        }

        public static void FadeOut(CanvasGroup canvasGroup, float duration = 0.3f)
        {
            canvasGroup.DOFade(0f, duration);
        }

        public static void ScaleIn(Transform transform, float duration = 0.3f)
        {
            transform.DOScale(1f, duration).From(0f).SetEase(Ease.OutBack);
        }

        public static void ScaleOut(Transform transform, float duration = 0.3f)
        {
            transform.DOScale(0f, duration).SetEase(Ease.InBack);
        }

        public static void SlideIn(RectTransform rectTransform, float duration = 0.3f, Direction direction = Direction.Right)
        {
            var startPosition = GetStartPosition(rectTransform, direction);
            rectTransform.DOAnchorPos(Vector2.zero, duration).From(startPosition).SetEase(Ease.OutQuint);
        }

        public static void SlideOut(RectTransform rectTransform, float duration = 0.3f, Direction direction = Direction.Right)
        {
            var endPosition = GetStartPosition(rectTransform, direction);
            rectTransform.DOAnchorPos(endPosition, duration).SetEase(Ease.InQuint);
        }

        private static Vector2 GetStartPosition(RectTransform rectTransform, Direction direction)
        {
            var canvas = rectTransform.GetComponentInParent<Canvas>();
            if (canvas == null) return Vector2.zero;

            var canvasRect = canvas.GetComponent<RectTransform>();
            var canvasWidth = canvasRect.rect.width;
            var canvasHeight = canvasRect.rect.height;

            switch (direction)
            {
                case Direction.Right:
                    return new Vector2(canvasWidth, 0);
                case Direction.Left:
                    return new Vector2(-canvasWidth, 0);
                case Direction.Up:
                    return new Vector2(0, canvasHeight);
                case Direction.Down:
                    return new Vector2(0, -canvasHeight);
                default:
                    return Vector2.zero;
            }
        }

        public enum Direction
        {
            Right,
            Left,
            Up,
            Down
        }
    }
} 