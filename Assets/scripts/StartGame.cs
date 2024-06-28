using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject ScrollManager;
    public GameObject CloseScroll;
    public GameObject SelectMusic;
    public GameObject GameName;
    public GameObject Settings;
    public GameObject ImportButton;

    private void Start() //Add Vibration to cash memory (Bag: if add this in settings script, it will not work before yiu open settings panel, so the best way to put it in the button you need yo push before game starts)
    {
        if (!PlayerPrefs.HasKey("Vibration"))
        {
            PlayerPrefs.SetInt("Vibration", 1);
            PlayerPrefs.Save();
        }
    }

    public void TimeToStartGame()
    {
        GameName.SetActive(false);
        SelectMusic.SetActive(true);
        ScrollManager.SetActive(true);
        CloseScroll.SetActive(true);
        gameObject.SetActive(false);
        Settings.SetActive(false);
        ImportButton.SetActive(false);
    }
    public void BackToStart()
    {
        GameName.SetActive(true);
        SelectMusic.SetActive(false);
        ScrollManager.SetActive(false);
        CloseScroll.SetActive(false);
        gameObject.SetActive(true);
        Settings.SetActive(true);
        ImportButton.SetActive(true);
    }
}
