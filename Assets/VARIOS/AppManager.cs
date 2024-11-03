using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] AbilityProvider abilityProvider;
    [SerializeField] KeyCode learnFireballCHEATCODE;
    [SerializeField] KeyCode dayNigthSwapCHEATCODE;
    [SerializeField] KeyCode heroMenuKeyCode;
    [SerializeField] KeyCode pauseMenuKeyCode;
    [SerializeField] DayNightCycle dayNightCycle;
    [SerializeField] GameObject heroMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button startBtn;
    [SerializeField] Camera menuCamera;
    //[SerializeField] GameObject music;

    bool isPaused;
    int gameClockSecondsCount;

    void Awake()
    {
        StartCoroutine(Utils.CenterCursor());
        StartCoroutine(StartGameClock());
    }

    private void Update()
    {
        if (Input.GetKeyDown(heroMenuKeyCode))
            heroMenu.SetActive(!heroMenu.activeSelf);

        if (Input.GetKeyDown(pauseMenuKeyCode))
            SwapPause();

        if (Input.GetKeyDown(dayNigthSwapCHEATCODE))
            dayNightCycle.SwapCycle();

        if (Input.GetKeyDown(learnFireballCHEATCODE))
            characterAbilities.LearnAbility(AbilityCode.Fireblast);
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
