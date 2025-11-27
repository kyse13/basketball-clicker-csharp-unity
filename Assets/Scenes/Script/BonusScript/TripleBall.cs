using UnityEngine;
using UnityEngine.UI;

public class TripleBall : MonoBehaviour
{
    public ClickScript clickScript;
    public GameObject[] buttonsToDeactivate; 
    public Button button;
    public GameObject objectToActivate;

    private bool isDoublePointsActive = false;
    private const float DoublePointsDuration = 30f;
    private float doublePointsTimer = 0f;
    private long originalStartingPointsToAdd = 1;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void Update()
    {
        if (isDoublePointsActive)
        {
            doublePointsTimer -= Time.deltaTime;
            if (doublePointsTimer <= 0)
            {
                isDoublePointsActive = false;
                clickScript.startingPointsToAdd = originalStartingPointsToAdd;
                objectToActivate.SetActive(false);

              
                foreach (var btn in buttonsToDeactivate)
                {
                    btn.SetActive(true);
                }
            }
        }

        button.interactable = clickScript.Score >= 2500 && !isDoublePointsActive;
    }

    private void OnButtonClick()
    {
        if (clickScript.Score >= 2500 && !isDoublePointsActive)
        {
            clickScript.Score -= 2500;
            originalStartingPointsToAdd = clickScript.startingPointsToAdd;
            clickScript.startingPointsToAdd *= 3;

            isDoublePointsActive = true;
            doublePointsTimer = DoublePointsDuration;
            objectToActivate.SetActive(true);

            
            foreach (var btn in buttonsToDeactivate)
            {
                btn.SetActive(false);
            }
        }
    }
}
