using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetAll : MonoBehaviour
{
    public ClickScript clickScript;
   
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    private bool timeContinued = false; 

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnResetButtonClick);
    }

    private void Update()
    {
        
        if (timeContinued)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnResetButtonClick()
    {
        clickScript.ResetAllData();
        

        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }

        
        timeContinued = true;
    }
}
