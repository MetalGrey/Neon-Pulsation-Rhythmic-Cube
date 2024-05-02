using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectobalIItem : MonoBehaviour
{
    public Material Ground;
    public Material ThisObjectMaterial;
    private Renderer renderer;
    public float speed;
   // public float lifetime = 3f;
   // public int howmanyweredestroyed;
    void Start()
    {
    //    Destroy(gameObject, lifetime);
    }
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        
        
    }
}
