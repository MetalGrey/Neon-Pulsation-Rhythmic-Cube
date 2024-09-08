using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public PulseAnimation PulseAnimation;
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
            PulseAnimation.PulseAnim();
            Combo++;
            UpdateComboText();
            StartCoroutine(CheckVibration());
        }
    }
    void UpdateComboText()
    {
        if (Combo >= 1000)
        {
            ComboText.text = Combo.ToString() + "\nHuman?!";
        }
        else if(Combo >= 300)
        {
            ComboText.text = Combo.ToString() + "\nGod like!";
        }
        else if(Combo >= 200)
        {
            ComboText.text = Combo.ToString() + "\nUnbelievable";
        }
        else if(Combo >= 100)
        {
            ComboText.text = Combo.ToString() + "\nMagnificent!";
        }
        else if(Combo >= 60)
        {
            ComboText.text = Combo.ToString() + "\nBreathtaking";
        }
        else if(Combo >= 40)
        {
            ComboText.text = Combo.ToString() + "\nMarvelous!";
        }
        else if (Combo >= 30)
        {
            ComboText.text = Combo.ToString() + "\nIncredible!";
        }
        else if (Combo >= 20)
        {
            ComboText.text = Combo.ToString() + "\nAmazing!";
        }
        else if (Combo >= 10)
        {
            ComboText.text = Combo.ToString() + "\nAwesome!";
        }
        else
        {
            ComboText.text = "";
        }
    }
    IEnumerator CheckVibration()
    {
        yield return new WaitForEndOfFrame(); // Ждем конца текущего кадра
        if (SettingsMenuManager.isVibrate)
        {
            if (Combo == 1000 || Combo == 300 || Combo == 200 || Combo == 100 || Combo == 60 || Combo == 40 || Combo == 30 || Combo == 20 || Combo == 11)
            {
                VibrationManager.Vibrate(50);
            }
        }
    }
    private void Update()
    {
        if (checkMiss)
        {  
            FadeText();
        }

        if (Combo >= 11) //11
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
