using UnityEngine;
using UnityEngine.UI;

public class DoubleBall : MonoBehaviour
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

                button.interactable = true;
            }
            else
            {
            
                foreach (var btn in buttonsToDeactivate)
                {
                    btn.SetActive(false);
                }

                button.interactable = false;
            }
        }
        else if (clickScript.Score >= 2500 && !isDoublePointsActive)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    private void OnButtonClick()
    {
        if (clickScript.Score >= 2500 && !isDoublePointsActive)
        {
            clickScript.Score -= 2500;

            int intStartingPointsToAdd = (int)clickScript.startingPointsToAdd;
            originalStartingPointsToAdd = clickScript.startingPointsToAdd;

            intStartingPointsToAdd *= 2;

            clickScript.startingPointsToAdd = intStartingPointsToAdd;

            isDoublePointsActive = true;
            doublePointsTimer = DoublePointsDuration;
            objectToActivate.SetActive(true);

            
            foreach (var btn in buttonsToDeactivate)
            {
                btn.SetActive(false);
            }

            button.interactable = false;
        }
    }
}
