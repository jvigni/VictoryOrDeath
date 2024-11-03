using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Utils : MonoBehaviour
{
    public static IEnumerator CenterCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yield return null;
        Cursor.lockState = CursorLockMode.None;
    }

    public static List<T> EnumValues<T>()
    {
        T[] genericArray = (T[])Enum.GetValues(typeof(T)); ;
        return genericArray.ToList();
    }

    public static T RandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }

    public static Sprite LoadImage(string id)
    {
        var imageSprite = Resources.Load<Sprite>($"images/{id}");
        if (imageSprite == null)
            Debug.LogError($"Image not found [id: {id}]");

        return imageSprite;
    }

    public static void ChangeAlpha(float amount, Image image)
    {
        Color newColor = image.color;
        newColor.a += amount;
        image.color = newColor;
    }

    /* return true if a 0-100 rnd roll is lower or equal than passed percentaje */
    public static bool RndPercentCheck(int percentaje)
    {
        var rndNumber = UnityEngine.Random.Range(0, 100);
        return rndNumber < percentaje;
    }
}
