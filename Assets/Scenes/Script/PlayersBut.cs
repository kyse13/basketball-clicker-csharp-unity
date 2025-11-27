using UnityEngine;
using UnityEngine.UI;

public class PlayersBut : MonoBehaviour
{
    public GameObject panel; 
    public Button showPanelButton; 
    public Button hidePanelButton; 

    private bool isPanelVisible = false;

    private void Start()
    {
        showPanelButton.onClick.AddListener(ShowPanel);
        hidePanelButton.onClick.AddListener(HidePanel);

        panel.SetActive(false); 
    }

    private void ShowPanel()
    {
        panel.SetActive(true);
        Time.timeScale = 0f; 
        isPanelVisible = true;
    }

    private void HidePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f; 
        isPanelVisible = false;
    }

    private void Update()
    {
        if (isPanelVisible)
        {
           
        }
        else
        {
            
        }
    }
}
