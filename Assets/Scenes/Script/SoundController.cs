using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public Button muteButton;
    private bool isMuted = false;
    private float savedVolume = 1f; 

    private const string MuteKey = "MuteState";

    private void Start()
    {
        
        isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;

        
        if (isMuted)
        {
            savedVolume = PlayerPrefs.GetFloat("SavedVolume", 1f);
        }

        
        UpdateAudioState();

        muteButton.onClick.AddListener(ToggleMute);
    }

    private void ToggleMute()
    {
        isMuted = !isMuted;

        
        PlayerPrefs.SetInt(MuteKey, isMuted ? 1 : 0);

        if (isMuted)
        {
            savedVolume = audioSource.volume; 
            audioSource.volume = 0f;
        }
        else
        {
            audioSource.volume = savedVolume; 
        }

       
        PlayerPrefs.SetFloat("SavedVolume", savedVolume);
        PlayerPrefs.Save();

        
        UpdateAudioState();
    }

    private void UpdateAudioState()
    {
        if (isMuted)
        {
            audioSource.volume = 0f; 
            muteButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            audioSource.volume = savedVolume; 
            muteButton.GetComponent<Image>().color = Color.white;
        }
    }
}
