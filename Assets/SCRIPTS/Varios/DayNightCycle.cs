using System.Collections;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    [SerializeField] NexusSpawner humansNexus;
    [SerializeField] NexusSpawner plagueNexus;
    [SerializeField] bool isDay = true; // Starts with day
    private float cycleDuration = 180f; // 3 minutes (180 seconds)

    void Start()
    {
        // Start the coroutine to handle the cycle
        StartCoroutine(CycleDayNight());
    }

    IEnumerator CycleDayNight()
    {
        while (true) // Infinite loop to keep cycling
        {
            // Wait for the cycle duration (3 minutes)
            yield return new WaitForSeconds(cycleDuration);

            // Toggle day/night state
            isDay = !isDay;

            // You can also add some actions to happen when day/night changes
            if (isDay)
            {
                Debug.Log("It's day now!");
                // TODO Change skybox/ligthing
                humansNexus.DayStarts();
                plagueNexus.DayStarts();
            }
            else
            {
                Debug.Log("It's night now!");
                // TODO Change skybox/ligthing
                humansNexus.NightStarts();
                plagueNexus.NightStarts();
            }
        }
    }
}
