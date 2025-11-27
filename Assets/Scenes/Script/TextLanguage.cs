using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TextLanguagee : MonoBehaviour
{
    public string language;
    TextMeshProUGUI text; 

    public string textRu;
    public string textEng;
    public string textTr;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        language = PlayerPrefs.GetString("Language");

        if (language == "" || language == "Eng")
        {
            text.text = textEng;
        }
        else if (language == "Ru")
        {
            text.text = textRu;
        }
        else if (language == "Türkiye")
        {
            text.text = textTr;
        }
    }
}
