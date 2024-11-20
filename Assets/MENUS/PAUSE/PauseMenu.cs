using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject firstMenu;
    [SerializeField] Button saveGameBtn;
    [SerializeField] Button loadGameBtn;
    [SerializeField] Button MoreBtn;
    [SerializeField] Button returnToGameBtn;

    [Space(10)]
    [SerializeField] GameObject secondMenu;
    [SerializeField] Button optionsBtn;
    [SerializeField] Button helpBtn;
    [SerializeField] Button endGameBtn;
    [SerializeField] Button returnToFirstMenuBtn;
    [SerializeField] GameObject camera3D;

    private void Start()
    {
        returnToGameBtn.onClick.AddListener(() => ReturnToGame());
        // TODO BTNS
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        StartCoroutine(Utils.CenterCursor());
        camera3D.GetComponent<Lightbug.CharacterControllerPro.Demo.Camera3D>().enabled = false;
    }

    private void OnDisable()
    {
        ReturnToGame();
    }

    void ReturnToGame()
    {
        camera3D.GetComponent<Lightbug.CharacterControllerPro.Demo.Camera3D>().enabled = true;
        Cursor.visible = false;
    }
}
