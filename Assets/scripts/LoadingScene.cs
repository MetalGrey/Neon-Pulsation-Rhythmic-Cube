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
    [SerializeField] float delayBeforeActivation = 1f; // �������� ����� ���������� ����� � ��������

    void Start()
    {
        // �������� �������� �������� �������� ����� ��� ������
        StartCoroutine(LoadAsyncScene("SampleScene")); // �������� "SampleScene" �� ��� ����� �������� �����
    }

    void Update()
    {
        // ��������� �������� ��� � �����
        CircleImg.fillAmount = progress;
        txtProgress.text = Mathf.Floor(progress * 100).ToString() + "%";
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        // �������� ����������� �������� �����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // �������� �������� ���� �� 0 �� 0.9, ������� ����������� ���
            progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            // ���� �������� ��������� (�� ����� ��� �� ������������)
            if (asyncOperation.progress >= 0.9f)
            {
                // ���������� ������ ��������
                progress = 1f;

                // ���� �������� ����� ����� ���������� �����
                yield return new WaitForSeconds(delayBeforeActivation);

                // ���������� ����������� �����
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
