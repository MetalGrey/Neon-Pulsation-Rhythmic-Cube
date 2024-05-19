using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject ScrollManager;
    public GameObject SelectMusic;
    public GameObject GameName;

    public void TimeToStartGame()
    {
        GameName.SetActive(false);
        SelectMusic.SetActive(true);
        ScrollManager.SetActive(true);
        gameObject.SetActive(false);
    }
}
