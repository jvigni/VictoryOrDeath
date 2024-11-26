using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgMarker : MonoBehaviour
{
    [SerializeField] GameObject redMark;
    [SerializeField] TextMeshProUGUI dmgText;
 
    public void Show(int dmg)
    {
        StartCoroutine(ShowDmg(dmg));
    }

    IEnumerator ShowDmg(int dmg)
    {
        redMark.SetActive(true);
        dmgText.text = dmg.ToString();
        yield return new WaitForSeconds(.6f);
        redMark.SetActive(false);
    }
}
