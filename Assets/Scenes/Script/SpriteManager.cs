using UnityEngine;
using System.Collections;

public class SaveSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    void Start()
    {
      
        string savedSpriteName = PlayerPrefs.GetString("SavedSprite", "");
        if (savedSpriteName != "")
        {
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == savedSpriteName)
                {
                    spriteRenderer.sprite = sprite;
                    break;
                }
            }
        }
    }

    void OnApplicationQuit()
    {
       
        if (spriteRenderer.sprite != null)
        {
            PlayerPrefs.SetString("SavedSprite", spriteRenderer.sprite.name);
        }
    }
}
