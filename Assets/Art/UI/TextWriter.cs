using System.Collections;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TextWriter : MonoBehaviour
{
    char fullBlockChar = '\u2588';
    public string description;
    public string title;
    [SerializeField] TextMeshProUGUI descriptionTMP;
    [SerializeField] TextMeshProUGUI titleTMP;
    [SerializeField] UnityEngine.Color blockTextColor = UnityEngine.Color.cyan;
    [SerializeField] UnityEngine.Color textColor = UnityEngine.Color.white;
    [SerializeField] float timeToShowText = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    string TextColorToString()
    {
        return "<color=#" + textColor.ToHexString() + ">";
    }
    string blockTextColorToString()
    {
        return "<color=#" + blockTextColor.ToHexString() + ">";
    }

    void UpdateTexts()
    {
        StartCoroutine(WriteCoroutine(description, descriptionTMP));
        StartCoroutine(WriteCoroutine(title, titleTMP));
    }

    IEnumerator WriteCoroutine(string textToShow, TextMeshProUGUI tmpToWriteTo)
    {
        float timeForEachCharacter = (timeToShowText / textToShow.Length)/2f;
        int stringLength = textToShow.Length;
        string blockString = string.Empty;
        for (int i = 0; i < stringLength; i++)
        {
            blockString += fullBlockChar;
            tmpToWriteTo.text = blockTextColorToString() + blockString;
            yield return new WaitForSeconds(timeForEachCharacter);
        }
        string newTextString = string.Empty;
        for (int i = 0; i < stringLength; i++)
        {
            blockString = blockString.Remove(blockString.Length - 1);
            newTextString += textToShow[i];
            tmpToWriteTo.text = TextColorToString() + newTextString + blockTextColorToString() + blockString;
            yield return new WaitForSeconds(timeForEachCharacter);
        }
    }
}
