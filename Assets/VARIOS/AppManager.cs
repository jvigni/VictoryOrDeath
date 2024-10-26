using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] KeyCode dayNigthSwapCHEATCODE;
    [SerializeField] KeyCode heroMenuKeyCode;
    [SerializeField] KeyCode pauseMenuKeyCode;
    [SerializeField] DayNightCycle dayNightCycle;
    [SerializeField] GameObject heroMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button startBtn;
    [SerializeField] Camera menuCamera;
    [SerializeField] GameObject character;
    //[SerializeField] GameObject music;

    bool isPaused;
    int gameClockSecondsCount;

    void Awake()
    {
        StartCoroutine(StartGameClock());
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseMenuKeyCode))
            SwapPause();

        if (Input.GetKeyDown(dayNigthSwapCHEATCODE))
            dayNightCycle.SwapCycle();
    }
    public int GetElapsedGameSeconds()
    {
        return gameClockSecondsCount;
    }

    IEnumerator StartGameClock()
    {
        while (!isPaused)
        {
            yield return new WaitForSeconds(1f);
            gameClockSecondsCount++;
        }
    }

    void SwapPause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        isPaused = !isPaused;
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
}
