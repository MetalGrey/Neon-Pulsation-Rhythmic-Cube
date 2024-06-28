using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    public Animator ComboAnimator;
    void Start()
    {
        ComboAnimator = GetComponent<Animator>();
    }
    public void PulseAnim()
    {
        ComboAnimator.SetTrigger("Pulse");
    }
}
