using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button deusVult;
    [SerializeField] Button about;
    [SerializeField] Button quit;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject hero;

    private void Start()
    {
        deusVult.onClick.AddListener(DeusVult);
        about.onClick.AddListener(About);
        quit.onClick.AddListener(Quit);
    }

    void DeusVult()
    {
        hero.SetActive(true);
        startMenu.SetActive(false);
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
