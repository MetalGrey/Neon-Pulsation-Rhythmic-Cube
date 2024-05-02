using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject right;
    public GameObject left;
    public GameObject GameInspector;


    public void TimeToStartGame()
    {
        GameInspector.SetActive(!GameInspector.activeSelf);
        right.SetActive(!right.activeSelf);
        left.SetActive(!left.activeSelf);
        gameObject.SetActive(false);
    }
}
