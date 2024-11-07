using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] AbilityProvider abilityProvider;
    [SerializeField] CharacterAbilities heroCharacterAbilities;
    [SerializeField] KeyCode learnFireballCHEATCODE;

    [Space]
    [SerializeField] DayNightCycle dayNightCycle;
    [SerializeField] KeyCode dayNigthSwapCHEATCODE;

    [Space]
    [SerializeField] KeyCode heroMenuKeyCode;
    [SerializeField] KeyCode pauseMenuKeyCode;

    [Space]
    [SerializeField] GameObject heroMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject pauseMenu;

    [Space]
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

        //if (Input.GetKeyDown(learnFireballCHEATCODE))
            
            //abilityProvider.TeachAbility(AbilityCode.Fireblast, heroCharacterAbilities);
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
