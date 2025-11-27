using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Luka : MonoBehaviour
{
    public ClickScript clickScript;
    public Button targetObject;
    public Image targetPanel;
    public TextMeshProUGUI targetText;
    public Sprite newSprite;
    public Color originalPanelColor = Color.black;
    public Color newPanelColor = Color.white;

    public Button executeButton;

    private const string PlayerPrefsKey = "c";
    private const string PlayerPrefsColorKey = "c";
    private const string PlayerPrefsExecuteButtonKey = "c";

    private bool hasExecuted = false;

    private void Awake()
    {
        LoadPlayerPrefs();
        UpdateButtonInteractivity();
        UpdateExecuteButtonInteractivity();
        UpdateTextInteractivity();
    }

    private void LoadPlayerPrefs()
    {
        if (targetPanel != null)
        {
            Color savedColor = originalPanelColor;

            if (PlayerPrefs.HasKey(PlayerPrefsColorKey))
            {
                string colorString = PlayerPrefs.GetString(PlayerPrefsColorKey);
                if (ColorUtility.TryParseHtmlString(colorString, out savedColor))
                {
                    newPanelColor = savedColor;
                }
            }

            targetPanel.color = savedColor;
        }

        if (PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            bool isButtonActive = PlayerPrefs.GetInt(PlayerPrefsKey) == 1;
            executeButton.gameObject.SetActive(isButtonActive);
        }

        if (PlayerPrefs.HasKey(PlayerPrefsExecuteButtonKey))
        {
            hasExecuted = PlayerPrefs.GetInt(PlayerPrefsExecuteButtonKey) == 1;
            executeButton.gameObject.SetActive(!hasExecuted);
        }
        else
        {
            executeButton.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        UpdateButtonInteractivity();
        UpdateExecuteButtonInteractivity();
        UpdateTextInteractivity();
    }

    private void UpdateButtonInteractivity()
    {
        executeButton.interactable = clickScript.Score >= 9000000 && !hasExecuted;
    }

    private void UpdateExecuteButtonInteractivity()
    {
        executeButton.interactable = clickScript.Score >= 9000000 && !hasExecuted;
    }

    private void UpdateTextInteractivity()
    {
        if (targetText != null)
        {
            targetText.gameObject.SetActive(!hasExecuted);
        }
    }

    public void OnExecuteButtonClick()
    {
        if (clickScript.Score >= 9000000 && !hasExecuted)
        {
            clickScript.Score -= 9000000;
            UpdateButtonInteractivity();
            hasExecuted = true;
            UpdateExecuteButtonInteractivity();
            UpdateTextInteractivity();

            if (targetPanel != null)
            {
                targetPanel.color = newPanelColor;
                PlayerPrefs.SetString(PlayerPrefsColorKey, ColorUtility.ToHtmlStringRGB(newPanelColor));
            }

            if (targetObject != null && newSprite != null)
            {
                SpriteRenderer spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = newSprite;
                }
            }

            executeButton.gameObject.SetActive(false);
            PlayerPrefs.SetInt(PlayerPrefsKey, 0);
            PlayerPrefs.SetInt(PlayerPrefsExecuteButtonKey, 1);
            PlayerPrefs.Save();
        }
    }

    public void ResetPlayerData()
    {
        PlayerPrefs.DeleteKey(PlayerPrefsKey);
        PlayerPrefs.DeleteKey(PlayerPrefsColorKey);
        PlayerPrefs.DeleteKey(PlayerPrefsExecuteButtonKey);

        if (targetPanel != null)
        {
            targetPanel.color = originalPanelColor;
            newPanelColor = originalPanelColor;
        }

        hasExecuted = false;
        executeButton.gameObject.SetActive(true);
        PlayerPrefs.SetInt(PlayerPrefsExecuteButtonKey, 0);
        PlayerPrefs.Save();

        UpdateButtonInteractivity();
        UpdateTextInteractivity();
    }
}
