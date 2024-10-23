using System.Collections;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] NexusSpawner humansNexus;
    [SerializeField] NexusSpawner plagueNexus;
    [SerializeField] Material daySkybox;
    [SerializeField] Material nightSkybox;

    [SerializeField] bool isDay = true; // Starts with day
    private float cycleDuration = 180f; // 3 minutes (180 seconds)
    private Coroutine cycleCoroutine;

    void Start()
    {
        // Start the coroutine to handle the cycle
        cycleCoroutine = StartCoroutine(CycleDayNight());
    }

    IEnumerator CycleDayNight()
    {
        while (true) // Infinite loop to keep cycling
        {
            // Wait for the cycle duration (3 minutes)
            yield return new WaitForSeconds(cycleDuration);

            // Toggle day/night state
            ToggleDayNight();
        }
    }

    public void ToggleDayNight()
    {
        // Toggle day/night state
        isDay = !isDay;

        // Execute day/night specific logic
        if (isDay)
        {
            Debug.Log("It's day now!");
            RenderSettings.skybox = daySkybox;
            //humansNexus.DayStarts();
            //plagueNexus.DayStarts();
        }
        else
        {
            Debug.Log("It's night now!");
            RenderSettings.skybox = nightSkybox;
            humansNexus.NightStarts();
            plagueNexus.NightStarts();
        }
    }

    public void SwapCycle()
    {
        // Stop the current cycle and toggle day/night manually
        if (cycleCoroutine != null)
        {
            StopCoroutine(cycleCoroutine);
        }
        ToggleDayNight();
        // Optionally restart the cycle
        cycleCoroutine = StartCoroutine(CycleDayNight());
    }
}
