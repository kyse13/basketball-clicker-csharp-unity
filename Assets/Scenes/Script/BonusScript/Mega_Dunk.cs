using UnityEngine;
using UnityEngine.UI;

public class Mega_Dunkk : MonoBehaviour
{
    public ClickScript clickScript;
    public Button button;

    public long savedStartingPointsToAdd = 5; 
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
            savedStartingPointsToAdd = 5; 
        }

        
        clickScript.startingPointsToAdd = savedStartingPointsToAdd;
        clickScript.UpdateText();
    }

    private void Update()
    {
        
        button.interactable = clickScript.Score >= 350000;
    }

    private void OnButtonClick()
    {
        if (clickScript.Score >= 350000)
        {
            clickScript.Score -= 350000;
            clickScript.startingPointsToAdd += 100; 
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
