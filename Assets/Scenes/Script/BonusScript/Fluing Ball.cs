using UnityEngine;
using UnityEngine.UI;

public class FluingBalll : MonoBehaviour
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
        
        button.interactable = clickScript.Score >= 1500000;
    }

    private void OnButtonClick()
    {
        if (clickScript.Score >= 1500000)
        {
            clickScript.Score -= 1500000;
            clickScript.startingPointsToAddPerSecond += 5000; 
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
