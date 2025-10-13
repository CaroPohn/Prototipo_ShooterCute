using System.Collections;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TextWriter : MonoBehaviour
{
    char fullBlockChar = '\u2588';
    [SerializeField] string stringToShow = "";
    [SerializeField] TextMeshProUGUI textMP;
    [SerializeField] UnityEngine.Color blockTextColor = UnityEngine.Color.cyan;
    [SerializeField] UnityEngine.Color textColor = UnityEngine.Color.white;
    [SerializeField] float timeToShowText = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine("WriteDescriptionCoroutine");
    }


    string TextColorToString()
    {
        return "<color=#" + textColor.ToHexString() + ">";
    }
    string blockTextColorToString()
    {
        return "<color=#" + blockTextColor.ToHexString() + ">";
    }

    IEnumerator WriteDescriptionCoroutine()
    {
        float timeForEachCharacter = (timeToShowText / stringToShow.Length)/2f;
        int stringLength = stringToShow.Length;
        string blockString = string.Empty;
        for (int i = 0; i < stringLength; i++)
        {
            blockString += fullBlockChar;
            textMP.text = blockTextColorToString() + blockString;
            yield return new WaitForSeconds(timeForEachCharacter);
        }
        string descriptionString = string.Empty;
        for (int i = 0; i < stringLength; i++)
        {
            blockString = blockString.Remove(blockString.Length - 1);
            descriptionString += stringToShow[i];
            textMP.text = TextColorToString() + descriptionString + blockTextColorToString() + blockString;
            yield return new WaitForSeconds(timeForEachCharacter);
        }
    }
}
