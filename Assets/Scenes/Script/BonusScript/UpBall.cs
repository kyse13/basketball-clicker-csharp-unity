using UnityEngine;
using UnityEngine.UI;

public class UpBalll : MonoBehaviour
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

        
        clickScript.startingPointsToAdd = (int)savedStartingPointsToAdd; 
        clickScript.UpdateText();
    }

    private void Update()
    {
        
        button.interactable = clickScript.Score >= 500;
    }

    private void OnButtonClick()
    {
        if (clickScript.Score >= 500)
        {
            clickScript.Score -= 500;
            clickScript.startingPointsToAdd += 5; 
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
