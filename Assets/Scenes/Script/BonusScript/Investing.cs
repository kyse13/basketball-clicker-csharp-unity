using UnityEngine;
using UnityEngine.UI;

public class Investinger : MonoBehaviour
{
    public ClickScript clickScript;
    public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void Update()
    {
        
        button.interactable = clickScript.Score >= 12500000;
    }

    private void OnButtonClick()
    {
        if (clickScript.Score >= 12500000)
        {
            clickScript.Score -= 12500000;
            clickScript.startingPointsToAdd += 25000; 
            clickScript.UpdateText();
        }
    }
}
