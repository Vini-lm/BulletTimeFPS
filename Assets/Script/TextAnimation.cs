using System;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{



    private float speed = 1.0f;
    private float tempo = 0.0f;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {

        if (tempo >= 0.3f)
        {
            float r = Mathf.Sin(Time.time * speed) * 0.3f + 0.25f;
            float g = Mathf.Sin(Time.time * speed + 2f) * 0.3f + 0.25f;
            float b = Mathf.Sin(Time.time * speed + 3f) * 0.3f + 0.25f;
            textMeshPro.color = new Color(r, g, b);
        }
        else
            tempo += Time.deltaTime;

    }
}
