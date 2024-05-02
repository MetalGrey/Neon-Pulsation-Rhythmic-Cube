using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneColorChange : MonoBehaviour
{
    public Material ThisObjectMaterial;
    private Renderer renderer;
    public Color newColor; // Новый цвет материала, который вы хотите применить
    Color LerpedColor = Color.blue;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = ThisObjectMaterial;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (renderer.sharedMaterial == ThisObjectMaterial)
            {
                LerpedColor = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 1));
                renderer.material.color = LerpedColor;
            }
        }
    }
}