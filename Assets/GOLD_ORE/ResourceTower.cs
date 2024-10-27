using UnityEngine;
using TMPro;
using System.Collections;

public class ResourceTower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI resourceText;
    [SerializeField] int resourcesPerCycle = 10;
    [SerializeField] int cycleDuration = 6;
    [SerializeField] float fadeDuration = .1f; // Time for fade-out
    [SerializeField] float floatUpDistance = 6f; // Distance to move upward

    private int totalResources;
    private float cycleTimer;

    private void Update()
    {
        cycleTimer += Time.deltaTime;

        if (cycleTimer >= cycleDuration)
            GiveResources();
    }

    void GiveResources()
    {
        cycleTimer = 0f;
        GiveResourcesToTeam(resourcesPerCycle);
        UpdateResourceDisplay(resourcesPerCycle);
        StartCoroutine(FadeText());
    }

    private void GiveResourcesToTeam(int amount)
    {
        // TODO repartir recursos a c/u del team
    }

    private void UpdateResourceDisplay(int amount)
    {
        resourceText.gameObject.SetActive(true);
        resourceText.text = "+ " + amount;
        Color color = resourceText.color;
        color.a = 1f; // Reset alpha to fully visible
        resourceText.color = color;
        resourceText.transform.localPosition = Vector3.zero; // Reset position
    }

    IEnumerator FadeText()
    {
        Color color = resourceText.color;
        Vector3 startPosition = resourceText.transform.localPosition;
        Vector3 endPosition = startPosition + new Vector3(0, floatUpDistance, 0);

        float startAlpha = color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            resourceText.color = color;

            // Move the text upward
            resourceText.transform.localPosition = Vector3.Lerp(startPosition, endPosition, time / fadeDuration);

            yield return null;
        }

        color.a = 0f;
        resourceText.color = color;
        resourceText.gameObject.SetActive(false);
    }
}
