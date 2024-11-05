using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgMarker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dmgText;
    [SerializeField] GameObject redMark;
 
    public void Show(int dmg)
    {
        StartCoroutine(ShowDmg(dmg)); // TODO WHY
    }

    IEnumerable ShowDmg(int dmg)
    {
        redMark.SetActive(true);
        dmgText.text = dmg.ToString();
        yield return new WaitForSeconds(1.5f);
        redMark.SetActive(false);
    }
}
