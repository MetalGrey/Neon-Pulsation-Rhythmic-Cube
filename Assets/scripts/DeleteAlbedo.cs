using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAlbedo : MonoBehaviour
{
    public Material material; // Ссылка на материал с шейдером, который нужно изменить

    void Start()
    {
        material.color = Color.black;
    }
}