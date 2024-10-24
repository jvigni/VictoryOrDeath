using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] KeyCode dayNigthSwapCHEATCODE;
    [SerializeField] KeyCode heroMenuKeyCode;
    [SerializeField] DayNightCycle dayNightCycle;
    [SerializeField] GameObject heroMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] Button startBtn;
    [SerializeField] Camera menuCamera;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject character;
    //[SerializeField] GameObject music;

    void Awake()
    {
        //cameraRotation = mainCamera.GetComponent<CameraRotation>();

        startMenu.gameObject.SetActive(true);
        startBtn.onClick.AddListener(() =>
        {
            //music.gameObject.SetActive(true);
            menuCamera.gameObject.SetActive(false);
            startMenu.gameObject.SetActive(false);
            character.gameObject.SetActive(true);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(dayNigthSwapCHEATCODE))
            dayNightCycle.SwapCycle();

        if (Input.GetKeyDown(heroMenuKeyCode))
        {
            heroMenu.SetActive(!heroMenu.activeSelf);
            // TODO Stop rotation active swap
            //mainCamera..cameraRotation.enabled = !cameraRotation.enabled;
        }
    }
}
