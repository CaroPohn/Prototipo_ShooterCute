using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComicManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] string[] texts;
    [SerializeField] float timeForEachCharacter;
    [SerializeField] float delayBetweenTextAndImages;
    [SerializeField] Sprite[] comicSprites;
    [SerializeField] float timeForEachImageToBeDisplayed;
    [SerializeField] Image image;
    int ammountOfTexts = 3;

    private string wiseEventName = "UI_Button_Normal";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShowComic());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) 
        {
            Skip();
        }
    }
    public void Skip()
    {
        StopAllCoroutines();
        GoToNextLevel();
    }
    void GoToNextLevel()
    {
        AkUnitySoundEngine.PostEvent(wiseEventName, gameObject);
        SceneLoader.Instance.ChangeScene("Spaceship_Interior");
    }
    IEnumerator ShowComic()
    {
        tmp.fontMaterial.EnableKeyword("GLOW_ON");
        int i = 0;
        while (i < ammountOfTexts) 
        {
            tmp.text = texts[i];
            tmp.maxVisibleCharacters = 0;
            tmp.ForceMeshUpdate();
            int totalCharacters = tmp.textInfo.characterCount;

            for (int j = 0; j <= totalCharacters; j++)
            {
                tmp.maxVisibleCharacters = j;
                yield return new WaitForSeconds(timeForEachCharacter);
            }

            yield return new WaitForSeconds(delayBetweenTextAndImages);
            image.gameObject.SetActive(true);
            image.sprite = comicSprites[i];
            yield return new WaitForSeconds(timeForEachImageToBeDisplayed);
            image.gameObject.SetActive(false);
            i++;
            tmp.text = "";
        }
        tmp.fontMaterial.DisableKeyword("GLOW_ON");
        image.gameObject.SetActive(true);
        image.sprite = comicSprites[i];
        yield return new WaitForSeconds(timeForEachImageToBeDisplayed);
        image.gameObject.SetActive(false);
        GoToNextLevel();

    }
}
