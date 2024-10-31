using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject firstMenu;
    [SerializeField] Button saveGame;
    [SerializeField] Button loadGame;
    [SerializeField] Button More;
    [SerializeField] Button returnToGame;

    [Space(10)]
    [SerializeField] GameObject secondMenu;
    [SerializeField] Button options;
    [SerializeField] Button help;
    [SerializeField] Button endGame;
    [SerializeField] Button returnToFirstMenu;
    [SerializeField] GameObject camera3D;

    private void OnEnable()
    {
        Cursor.visible = true;
        StartCoroutine(Utils.CenterCursor());
        camera3D.GetComponent<Lightbug.CharacterControllerPro.Demo.Camera3D>().enabled = false;
    }

    private void OnDisable()
    {
        camera3D.GetComponent<Lightbug.CharacterControllerPro.Demo.Camera3D>().enabled = true;
        Cursor.visible = false;
    }
}
