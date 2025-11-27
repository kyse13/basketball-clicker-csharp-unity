using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickScript : MonoBehaviour
{
    public GameObject objectPrefab;
    public float objectSpeed = 5f;
    public float buttonScaleIncrease = 1.2f;
    public float buttonClickDuration = 0.2f;
    public AudioClip buttonSound;

    public long startingPointsToAdd = 1;
    public long startingPointsToAddPerSecond = 0;

    private bool isButtonScaling = false;
    private Vector3 originalButtonScale;
    private Button button;
    private AudioSource audioSource;
    public long score = 0;
    public float gameTime = 1f;

    public long Score
    {
        get => score;
        set
        {
            score = value;
            SaveData();
        }
    }

    public long StartingPointsToAdd
    {
        get => startingPointsToAdd;
        set
        {
            startingPointsToAdd = value;
            SaveData();
        }
    }

    public long StartingPointsToAddPerSecond
    {
        get => startingPointsToAddPerSecond;
        set
        {
            startingPointsToAddPerSecond = value;
            SaveData();
        }
    }

    public TextMeshProUGUI textMeshPro;

    private const string PlayerPrefsSpriteKey = "SavedSpriteName";

    private void Start()
    {
        LoadData();
        button = GetComponent<Button>();
        originalButtonScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();

        UpdateText();
        StartCoroutine(AddPointsOverTime());

        if (isButtonScaling)
        {
            isButtonScaling = false;
            transform.localScale = originalButtonScale;
        }
    }

    private void Update()
    {
        if (isButtonScaling)
        {
            float scaleAmount = Time.deltaTime / buttonClickDuration;
            transform.localScale = Vector3.Lerp(transform.localScale, originalButtonScale * buttonScaleIncrease, scaleAmount);
        }
    }

    public void OnButtonClicked()
    {
        SpawnObject();
        ScaleButton();
        PlayButtonSound();
        Score += StartingPointsToAdd;
        UpdateText();
    }

    private void SpawnObject()
    {
        GameObject newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        rb.linearVelocity = Random.onUnitSphere * objectSpeed;

        StartCoroutine(DestroyObjectDelayed(newObject));
    }

    private IEnumerator DestroyObjectDelayed(GameObject obj)
    {
        yield return new WaitForSeconds(0.85f);
        Destroy(obj);
    }

    private void ScaleButton()
    {
        StartCoroutine(ScaleButtonCoroutine());
    }

    private IEnumerator ScaleButtonCoroutine()
    {
        isButtonScaling = true;
        yield return new WaitForSeconds(buttonClickDuration);
        isButtonScaling = false;
    }

    private IEnumerator AddPointsOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Score += StartingPointsToAddPerSecond;
            UpdateText();

            gameTime += 1f;
        }
    }

    public void UpdateText()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = Score.ToString();
        }
    }

    private void PlayButtonSound()
    {
        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("Score", Score.ToString());
        PlayerPrefs.SetString("StartingPointsToAdd", StartingPointsToAdd.ToString());
        PlayerPrefs.SetString("StartingPointsToAddPerSecond", StartingPointsToAddPerSecond.ToString());
        PlayerPrefs.SetString("GameTime", gameTime.ToString());

        if (objectPrefab != null)
        {
            SpriteRenderer spriteRenderer = objectPrefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && spriteRenderer.sprite != null)
            {
                PlayerPrefs.SetString(PlayerPrefsSpriteKey, spriteRenderer.sprite.name);
            }
        }

        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            long.TryParse(PlayerPrefs.GetString("Score"), out score);
        }
        else
        {
            Score = 0;
        }

        if (PlayerPrefs.HasKey("StartingPointsToAdd"))
        {
            long.TryParse(PlayerPrefs.GetString("StartingPointsToAdd"), out startingPointsToAdd);
        }
        else
        {
            StartingPointsToAdd = 1;
        }

        if (PlayerPrefs.HasKey("StartingPointsToAddPerSecond"))
        {
            long.TryParse(PlayerPrefs.GetString("StartingPointsToAddPerSecond"), out startingPointsToAddPerSecond);
        }
        else
        {
            StartingPointsToAddPerSecond = 0;
        }

        if (PlayerPrefs.HasKey("GameTime"))
        {
            float.TryParse(PlayerPrefs.GetString("GameTime"), out gameTime);
        }
        else
        {
            gameTime = 1f;
        }

        if (PlayerPrefs.HasKey(PlayerPrefsSpriteKey))
        {
            string savedSpriteName = PlayerPrefs.GetString(PlayerPrefsSpriteKey);
            Sprite loadedSprite = Resources.Load<Sprite>(savedSpriteName);
            if (loadedSprite != null)
            {
                SpriteRenderer spriteRenderer = objectPrefab.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = loadedSprite;
                }
            }
        }
    }

    public void ResetAllData()
    {
        Score = 0;
        StartingPointsToAdd = 1;
        StartingPointsToAddPerSecond = 0;
        PlayerPrefs.DeleteAll();
        UpdateText();

        if (isButtonScaling)
        {
            isButtonScaling = false;
            transform.localScale = originalButtonScale;
        }

        gameTime = 1f;

        PlayerPrefs.DeleteKey(PlayerPrefsSpriteKey);
    }
}
