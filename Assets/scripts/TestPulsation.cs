using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPulsation : MonoBehaviour
{
    public Text TextComponent;
    public float PulseSpeed = 6f;
    public float MinSize = 1f;
    public float MaxSize = 1.05f;

    private void Update()
    {
        float ScaleFactor = Mathf.PingPong(Time.time * PulseSpeed, 1.0f) * (MaxSize - MinSize) + MinSize;
        TextComponent.transform.localScale = new Vector3(ScaleFactor, ScaleFactor, 1.0f);
    }
}
