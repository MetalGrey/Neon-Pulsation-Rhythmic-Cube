using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelect : MonoBehaviour
{
    public Button[] Buttons;
    public AudioClip[] AllMusic;
    public Text[] ScoreTextSave;
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

    public bool MusicIsPlaying = false;
    public GameObject RepeatButton;
    public GameObject GameOverText;

    private int currentMusicIndex;

    public GameObject CloseSelect;


    private void Start()
    {
        OriginalColor = dimeDelation.color;

       // Buttons = GameObject.FindObjectsOfType<Button>();
        for (int i = 0; i < Buttons.Length; i++)
        {
            int index = i;
            Buttons[i].onClick.AddListener(() => ButtonClicked(index));
        }

        for (int j = 0; j < ScoreTextSave.Length; j++)
        {
            string currentLastScore = "LastScore" + j;
            float gettingFlotForText = PlayerPrefs.GetFloat(currentLastScore, 0); 
            ScoreTextSave[j].text = ((int)gettingFlotForText).ToString() + "%";
        }
    }

    public void ButtonClicked(int buttonIndex)
    {
        Debug.Log(buttonIndex);
        StartCoroutine(PlayEmptyThenMusic(buttonIndex));
    }
    System.Collections.IEnumerator PlayEmptyThenMusic(int musicIndex)
    {
        right.SetActive(!right.activeSelf);
        left.SetActive(!left.activeSelf);
        Scroll.SetActive(false);
        CloseSelect.SetActive(false);
        // gameObject.SetActive(false);

        TimeCounter.SetActive(true);

        int gamePlayed = PlayerPrefs.GetInt("HowManyTimes");
        gamePlayed += 1;
        PlayerPrefs.SetInt("HowManyTimes", gamePlayed);
        PlayerPrefs.Save();
        Debug.LogWarning(gamePlayed);


        dimeDelation.text = "3";
        yield return new WaitForSeconds(0.5f);
        dimeDelation.text = "2";
        yield return new WaitForSeconds(0.5f);
        dimeDelation.text = "1";
        yield return new WaitForSeconds(0.5f);
        dimeDelation.text = "Let's GO!";
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(FadeText());
        NeonDreams.SetActive(true);
        MusicIsPlaying = true;

        MusicPlayer.clip = AllMusic[musicIndex];
        MusicPlayer.Play();
        ScoreText.SetActive(!ScoreText.activeSelf);
        currentMusicIndex = musicIndex;
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
    private void Update()
    {
        if (MusicIsPlaying)
        {
            if (!MusicPlayer.isPlaying)
            {
                Debug.Log("Finish music");

                MusicAnalysis MusicAnalysis = NeonDreams.GetComponent<MusicAnalysis>();
                float currentScore = MusicAnalysis.ScoreP;
                string key = "LastScore" + currentMusicIndex;
                float LodedData = PlayerPrefs.GetFloat(key, 0);

                if (LodedData < currentScore)
                {
                    PlayerPrefs.SetFloat(key, currentScore);
                    PlayerPrefs.Save();
                }

                if (PlayerPrefs.GetInt("HowManyTimes") >= 2)
                {
                    AdsManager.Instance.interstitialAds.ShowInterstitialAd();
                    PlayerPrefs.SetInt("HowManyTimes", 0);
                    PlayerPrefs.Save();
                }

                NeonDreams.SetActive(false);
                RepeatButton.SetActive(true);
                GameOverText.SetActive(true);
                right.SetActive(false);
                left.SetActive(false);
            }
        }
    }
}
