using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] Button startBtn;
    [SerializeField] Camera menuCamera;
    [SerializeField] Camera mainCamera;
    //[SerializeField] GameObject music;

    void Awake()
    {
        startBtn.onClick.AddListener(() =>
        {
            //music.gameObject.SetActive(true);
            menuCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            startMenu.gameObject.SetActive(false);
        });
    }
}
