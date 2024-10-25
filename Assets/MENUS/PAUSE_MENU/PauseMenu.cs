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
}
