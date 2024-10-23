using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] KeyCode dayNigthSwapCHEATCODE;
    [SerializeField] DayNightCycle dayNightCycle;
    [SerializeField] GameObject tabMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] Button startBtn;
    [SerializeField] Camera menuCamera;
    [SerializeField] Camera mainCamera;
    //[SerializeField] GameObject music;

    void Awake()
    {
        startMenu.gameObject.SetActive(true);
        startBtn.onClick.AddListener(() =>
        {
            //music.gameObject.SetActive(true);
            menuCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            startMenu.gameObject.SetActive(false);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(dayNigthSwapCHEATCODE))
            dayNightCycle.SwapCycle();

        if (Input.GetKeyDown(KeyCode.Tab))
            tabMenu.SetActive(!tabMenu.activeSelf);
    }
}
