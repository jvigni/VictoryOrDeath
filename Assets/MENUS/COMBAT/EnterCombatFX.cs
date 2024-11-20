using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

public class EnterCombatFX : MonoBehaviour
{
    [SerializeField] GameObject blackPanel;
    [SerializeField] float fadeDuration = 1f;

    private Image panelImage;

    void OnEnable()
    {
        panelImage = blackPanel.GetComponent<Image>();
        StartFading();
    }

    async void StartFading()
    {
        await FadeIn();
        await FadeOut();
    }

    async Task FadeIn()
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        StartCoroutine(FadePanel(0f, 1f, () =>
        {
            tcs.SetResult(true);
        }));

        await tcs.Task;
    }

    async Task FadeOut()
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        StartCoroutine(FadePanel(1f, 0f, () =>
        {
            tcs.SetResult(true);
        }));

        await tcs.Task;
    }

    IEnumerator FadePanel(float startAlpha, float targetAlpha, Action onComplete)
    {
        Color color = panelImage.color;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            panelImage.color = color;
            yield return null;
        }

        // Ensure the panel returns to fully opaque if fading out (targetAlpha = 0)
        if (targetAlpha == 0f)
        {
            color.a = 0f;
        }
        else if (targetAlpha == 1f)
        {
            color.a = 1f;
        }

        panelImage.color = color;

        onComplete?.Invoke();
    }
}
