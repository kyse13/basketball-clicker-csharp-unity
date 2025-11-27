using UnityEngine;
using UnityEngine.UI;

public class GoldenHand : MonoBehaviour
{
    public ClickScript clickScript;
    public Button button;

    public long savedStartingPointsPerSecond = 0; 
    private const string SavedStartingPointsPerSecondKey = "SavedStartingPointsPerSecond";

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

        
        if (PlayerPrefs.HasKey(SavedStartingPointsPerSecondKey))
        {
            savedStartingPointsPerSecond = PlayerPrefs.GetInt(SavedStartingPointsPerSecondKey);
        }
        else
        {
            savedStartingPointsPerSecond = 0; 
        }

        
        clickScript.startingPointsToAddPerSecond = savedStartingPointsPerSecond;
        clickScript.UpdateText();
    }

    private void Update()
    {
        
        button.interactable = clickScript.Score >= 150000;
    }

    private void OnButtonClick()
    {
        if (clickScript.Score >= 150000)
        {
            clickScript.Score -= 150000;
            clickScript.startingPointsToAddPerSecond += 500; 
            clickScript.UpdateText();

            
            savedStartingPointsPerSecond = clickScript.startingPointsToAddPerSecond;
            PlayerPrefs.SetInt(SavedStartingPointsPerSecondKey, (int)savedStartingPointsPerSecond);
        }
    }

    
    public void ResetSavedValue()
    {
        savedStartingPointsPerSecond = 0;
        PlayerPrefs.SetInt(SavedStartingPointsPerSecondKey, (int)savedStartingPointsPerSecond);
    }
}
