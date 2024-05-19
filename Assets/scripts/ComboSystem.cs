using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public int Combo = 0;
    public Text ComboText;
    public GameObject ComboTextAsGameObject;
    public bool checkMiss = false;

    public float fadeDuration = 0.2f;
    private float timer = 0f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collect")
        {
            if (Combo > 21)
            {
                ComboText.text = Combo.ToString() + "\nWOW!";
            }
            else if (Combo > 11)
            {
                ComboText.text = Combo.ToString() + "\nGJ!!!!!";
            }
            else
            {
                ComboText.text = Combo.ToString();
            }
            Combo++;
        }
    }
    private void Update()
    {
        if (checkMiss)
        {  
            FadeText();
        }

        if (Combo >= 11)
        {
            ComboTextAsGameObject.SetActive(true);
            timer = 0f;
        }
    }
    public void FadeText()
    {
        Color PreviousColor = ComboText.color;
        float previousAlpha = ComboText.color.a;
        timer += Time.deltaTime;
        float alpha = 1f - (timer / fadeDuration);
        ComboText.color = new Color(ComboText.color.r, ComboText.color.g, ComboText.color.b, alpha);

        if (timer >= fadeDuration)
        {
            ComboTextAsGameObject.SetActive(false);
            checkMiss = false;
            ComboText.color = new Color(PreviousColor.r, PreviousColor.g, PreviousColor.b, 255);
        }
    }
}
