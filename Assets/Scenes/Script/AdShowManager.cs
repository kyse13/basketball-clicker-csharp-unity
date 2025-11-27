using UnityEngine;
using UnityEngine.UI;
using YG;

public class AdShowManager : MonoBehaviour
{
    [SerializeField] private Button adButton; 
    private float delay = 0.17f; 

    private void Start()
    {
        adButton.onClick.AddListener(ShowAdWithDelay);
    }

    private void ShowAdWithDelay()
    {
        Invoke("ShowAd", delay);
    }

    private void ShowAd()
    {
        YandexGame.FullscreenShow(); 
    }
}
