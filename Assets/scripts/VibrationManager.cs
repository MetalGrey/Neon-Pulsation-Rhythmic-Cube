using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static void Vibrate(long milliseconds)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // Get the UnityPlayer class
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            // Get the current activity
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            // Get the vibration effect class
            AndroidJavaClass vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");

            // Get the default amplitude
            int defaultAmplitude = vibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE");

            // Create a vibration effect
            AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>(
                "createOneShot", milliseconds, defaultAmplitude);

            // Get the vibrator service
            AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

            // Vibrate the device
            vibrator.Call("vibrate", vibrationEffect);
        }
        else
        {
            // Vibration is not supported or not implemented for this platform (e.g., iOS)
            Debug.LogWarning("Vibration not supported on this platform.");
        }
    }
}
