using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        Coroutine currentActiveFade;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }

        // target = alpha target
        public Coroutine Fade(float target, float time)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine (currentActiveFade);
            }

            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                // to handle [0 to 1] or [1 to 0]
                // to visible or invisible
                canvasGroup.alpha =
                    Mathf
                        .MoveTowards(canvasGroup.alpha,
                        target,
                        Time.deltaTime / time);

                yield return null;
            }
        }
    }
}
