﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения объекта
    public Animator anim;
    public bool left = false;
    public bool right = false;


    public Material Ground1;
    public Material Ground2;
    public Material Ground3;
    private Renderer renderer;

    public GameObject CubeParticle1;
    public GameObject CubeParticle2;
    public GameObject CubeParticle3;
    public void Start()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        renderer.material = Ground2;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collect")
        {
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        // Проверяем нажатие клавиш влево и вправо для перемещения объекта
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RightSide();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            LeftSide();
        }
    }
    public void RightSide()
    {
        if (anim.GetBool("right") == false)
        {
            anim.SetBool("left", true);
            renderer.material = Ground1;
            CubeParticle1.SetActive(true);
            CubeParticle2.SetActive(false);
            CubeParticle3.SetActive(false);
        }
        else if (anim.GetBool("right") == true)
        {
            anim.SetBool("right", false);
            renderer.material = Ground2;
            CubeParticle1.SetActive(false);
            CubeParticle2.SetActive(true);
            CubeParticle3.SetActive(false);
        }
    }
    public void LeftSide()
    {
        if (anim.GetBool("left") == false)
        {
            anim.SetBool("right", true);
            renderer.material = Ground3;
            CubeParticle1.SetActive(false);
            CubeParticle2.SetActive(false);
            CubeParticle3.SetActive(true);
        }
        else if (anim.GetBool("left") == true)
        {
            anim.SetBool("left", false);
            renderer.material = Ground2;
            CubeParticle1.SetActive(false);
            CubeParticle2.SetActive(true);
            CubeParticle3.SetActive(false);
        }
    }
    /*void Update()
    {
        // Проверяем нажатие клавиш влево и вправо для перемещения объекта
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if ((right == false) && (left == false))
            {
                anim.SetInteger("Position", 1);
                center = false;
                left = true;
                right = false;
            }
       
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if ((right == false) && (left == false))
            {
                anim.SetInteger("Position", 3);
                center = false;
                left = false;
                right = true;
            }
            else if (left == true)
            {
                anim.SetInteger("Position", 2);
                
            }

        }
    }*/

}