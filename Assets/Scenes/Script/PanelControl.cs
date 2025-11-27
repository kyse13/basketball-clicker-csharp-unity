using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject panel;
    public Button closeButton;

    private bool isActive = false;
    private bool isPanelOpen = false; 
    private float timer = 0f;
    private const float interval = 90f; 

    private void Start()
    {
        closeButton.onClick.AddListener(ClosePanel);
        InvokeRepeating("ShowPanel", interval, interval);
    }

    private void Update()
    {
        if (isActive)
        {
            Time.timeScale = 0f; 

            
            if (!isPanelOpen)
            {
                
            }
        }
        else
        {
            Time.timeScale = 1f; 
        }
    }

    private void ShowPanel()
    {
        isActive = true;
        panel.SetActive(true);
    }

    private void ClosePanel()
    {
        isActive = false;
        panel.SetActive(false);
        isPanelOpen = false; 
    }
}
