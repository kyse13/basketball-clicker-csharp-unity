using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public ClickScript clickScript;
    public Button button;

    public long savedStartingPointsToAdd = 1;
    private const string SavedStartingPointsKey = "SavedStartingPointsToAdd";

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

        if (PlayerPrefs.HasKey(SavedStartingPointsKey))
        {
            savedStartingPointsToAdd = PlayerPrefs.GetInt(SavedStartingPointsKey);
        }
        else
        {
            savedStartingPointsToAdd = 1;
        }

        clickScript.startingPointsToAdd = savedStartingPointsToAdd;
        clickScript.UpdateText();
    }

    private void Update()
    {
        button.interactable = clickScript.Score >= 100;
    }

    private void OnButtonClick()
    {
        if (clickScript.Score >= 100)
        {
            clickScript.Score -= 100;
            clickScript.startingPointsToAdd += 1;
            clickScript.UpdateText();

            savedStartingPointsToAdd = clickScript.startingPointsToAdd;

          
            PlayerPrefs.SetInt(SavedStartingPointsKey, (int)savedStartingPointsToAdd);
        }
    }

    public void ResetSavedValue()
    {
        savedStartingPointsToAdd = 0;
        PlayerPrefs.SetInt(SavedStartingPointsKey, (int)savedStartingPointsToAdd);
    }
}
