using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class MusicAnalysis : MonoBehaviour
{
    public AudioSource audioSource;

    // Параметры для анализа тембра
    public int sampleSize = 1024; // Размер выборки для анализа
    public float[] spectrumData; // Массив для хранения спектральных данных
    private int level = 1;
    public Transform Spawn1;
    public Transform Spawn2;
    public Transform Spawn3;
    public GameObject PrefabGround1;
    public GameObject PrefabGround2;
    public GameObject PrefabGround3;
    public PostProcessVolume postProcessVolume; // Ссылка на компонент Post Process Volume
    Bloom bloomLayer; // Ссылка на настройки bloom
    public float minIntensity = 20.0f; // Минимальная интенсивность bloom
    public float maxIntensity = 28.0f; // Максимальная интенсивность bloom
    public float pulseSpeed = 1.0f; // Скорость пульсации
    private float currentIntensity; // Текущая интенсивность bloom

    private int Score = 0;
    private int Missed = 0;
    private float ScoreP;
    public Text ScoreText;
    void Start()
    {
        spectrumData = new float[sampleSize];
        StartCoroutine(GenerateCoroutine());
        postProcessVolume.profile.TryGetSettings(out bloomLayer);
    }

    void Update()
    {
        // Получаем спектральные данные из AudioSource
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // Вычисляем среднее значение амплитуды по всем частотам
        float averageLevel = 0f;
        for (int i = 0; i < sampleSize; i++)
        {
            averageLevel += spectrumData[i];
        }
        averageLevel /= sampleSize;

        if (averageLevel > 0.0065)
        {
        //    Debug.Log("Average Level: MAX " + averageLevel);
            level = 3;
            // Изменяем текущую интенсивность с использованием линейной интерполяции
            currentIntensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * pulseSpeed, 0.7f));

            // Устанавливаем новое значение интенсивности bloom
            bloomLayer.intensity.value = currentIntensity;
        }
        else if (averageLevel > 0.003)
        {
        //    Debug.Log("Average Level: OK " + averageLevel);
            level = 2;
            currentIntensity = 20f;
            bloomLayer.intensity.value = currentIntensity;
        }
        else
        {
        //    Debug.Log("Average Level: LOW " + averageLevel);
            level = 1;
            currentIntensity = 20f;
            bloomLayer.intensity.value = currentIntensity;
        }
        //Вывод % 
        if (Missed != 0)
        {
            ScoreP = (int)(100 - (((float)Missed / (float)Score) * 100f));
            ScoreText.text = ScoreP.ToString() + "%";
            Debug.Log(ScoreP + " " + Missed + " " + Score);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collect")
        {
            Destroy(other.gameObject);
            Missed++;
        }
    }

    IEnumerator GenerateCoroutine()
    {
        while (true)
        {
            if (level == 3)
            {
                Instantiate(PrefabGround3, Spawn3.position, Quaternion.identity);
                Score++;
                yield return new WaitForSeconds(0.2f);
            }
            else if (level == 2)
            {
                Instantiate(PrefabGround2, Spawn2.position, Quaternion.identity);
                Score++;
                yield return new WaitForSeconds(0.4f);
            }
            else if (level == 1)
            {
                Instantiate(PrefabGround1, Spawn1.position, Quaternion.identity);
                Score++;
                yield return new WaitForSeconds(0.65f);
            }
            
        }
    }
}