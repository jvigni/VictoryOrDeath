using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button startBtn;
    [SerializeField] Button aboutBtn;
    [SerializeField] Button quitBtn;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject hero;
    [SerializeField] GameObject spawnFX;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip spawnMusicClip;

    private void Start()
    {
        startBtn.onClick.AddListener(StartDEUSVULT);
        aboutBtn.onClick.AddListener(About);
        quitBtn.onClick.AddListener(Quit);
    }

    void StartDEUSVULT()
    {
        //hero.SetActive(true);
        //spawnFX.SetActive(true);
        startMenu.SetActive(false);
        Cursor.visible = false;

        audioSource.clip = spawnMusicClip;
        audioSource.Play();
    }

    void About()
    {
        // TODO
    }

    void Quit()
    {
        Application.Quit();
    }
}
