using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject ScrollManager;
    public GameObject SelectMusic;
    

    public void TimeToStartGame()
    {
        SelectMusic.SetActive(true);
        ScrollManager.SetActive(true);
        gameObject.SetActive(false);
    }
}
