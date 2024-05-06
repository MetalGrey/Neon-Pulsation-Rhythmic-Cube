using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelect : MonoBehaviour
{
    public Button[] Buttons;
    public AudioClip[] AllMusic;
    public AudioSource MusicPlayer;
    public Text dimeDelation;
    public GameObject Scroll;
    public GameObject right;
    public GameObject left;
    public GameObject ScoreText;
    public GameObject NeonDreams;
    public GameObject TimeCounter;

    public GameObject dimeDelationAsAnObject;
    public float FadeDuration = 1f;
    private Color OriginalColor;
    private void Start()
    {
        OriginalColor = dimeDelation.color;

        Buttons = GameObject.FindObjectsOfType<Button>();
        for (int i = 0; i < Buttons.Length; i++)
        {
            int index = i;
            Buttons[i].onClick.AddListener(() => ButtonClicked(index));
        }
    }

    void ButtonClicked(int buttonIndex)
    {
        Debug.Log(buttonIndex);
        StartCoroutine(PlayEmptyThenMusic(buttonIndex));
    }
    System.Collections.IEnumerator PlayEmptyThenMusic(int musicIndex)
    {
        right.SetActive(!right.activeSelf);
        left.SetActive(!left.activeSelf);
        Scroll.SetActive(false);
        // gameObject.SetActive(false);

        TimeCounter.SetActive(true);

        dimeDelation.text = "3";
        yield return new WaitForSeconds(0.5f);
        dimeDelation.text = "2";
        yield return new WaitForSeconds(0.5f);
        dimeDelation.text = "1";
        yield return new WaitForSeconds(0.5f);
        dimeDelation.text = "Let's GO!";
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(FadeText());
       


        MusicPlayer.clip = AllMusic[musicIndex];
        MusicPlayer.Play();


        ScoreText.SetActive(!ScoreText.activeSelf);
        NeonDreams.SetActive(true);
    }
    IEnumerator FadeText()
    {
        float CurrentTime = 0f;
        while (CurrentTime < FadeDuration)
        {
            CurrentTime += Time.deltaTime;
            float FadeProgress = CurrentTime / FadeDuration;
            Color NewColor = dimeDelation.color;
            NewColor.a = Mathf.Lerp(OriginalColor.a, 0f, FadeProgress);
            dimeDelation.color = NewColor;
            yield return null;
        }
        dimeDelationAsAnObject.SetActive(false);
    }
}
