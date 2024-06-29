using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] Image CircleImg;
    [SerializeField] TMP_Text txtProgress;

    [SerializeField] [Range(0, 1)] float progress = 0f;
    [SerializeField] float delayBeforeActivation = 1f; // Задержка перед активацией сцены в секундах

    void Start()
    {
        // Начинаем корутину загрузки основной сцены при старте
        StartCoroutine(LoadAsyncScene("SampleScene")); // Замените "SampleScene" на имя вашей основной сцены
    }

    void Update()
    {
        // Обновляем прогресс бар и текст
        CircleImg.fillAmount = progress;
        txtProgress.text = Mathf.Floor(progress * 100).ToString() + "%";
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        // Начинаем асинхронную загрузку сцены
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Прогресс загрузки идет от 0 до 0.9, поэтому нормализуем его
            progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            // Если загрузка завершена (но сцена ещё не активирована)
            if (asyncOperation.progress >= 0.9f)
            {
                // Показываем полный прогресс
                progress = 1f;

                // Ждем заданное время перед активацией сцены
                yield return new WaitForSeconds(delayBeforeActivation);

                // Активируем загруженную сцену
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
