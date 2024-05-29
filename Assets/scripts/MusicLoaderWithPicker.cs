using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;
using System.Collections;

public class MusicLoaderWithPicker : MonoBehaviour
{
    public Button loadMusicButton;
    public AudioSource audioSource;
    public GameObject scriptMusicSelectObject;
    private MusicSelect musicSelect;

    public GameObject GameName;
    public GameObject Settings;
    public GameObject ImportButton;
    public GameObject Score;
    public GameObject Play;
    void Start()
    {
        musicSelect = scriptMusicSelectObject.GetComponent<MusicSelect>();
        loadMusicButton.onClick.AddListener(OpenFileBrowser);
    }

    void OpenFileBrowser()
    {
        // ������������� ������ ��� ������ ������ mp3 ������
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Audio Files", ".mp3"));
        // ��������� ���������� ���� ������ �����
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        // ���������� ���������� ���� ������ ����� � ���� ���� ������������ �� ������� ����
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "�������� ����������� ����", "�������");

        if (FileBrowser.Success)
        {
            // �������� ���� � ���������� �����
            string musicFilePath = FileBrowser.Result[0];
            StartCoroutine(LoadAudio(musicFilePath));
        }
    }

    IEnumerator LoadAudio(string filePath)
    {
        using (WWW www = new WWW("file://" + filePath))
        {
            yield return www;
            audioSource.clip = www.GetAudioClip();
            
            GameName.SetActive(false);
            Settings.SetActive(false);
            ImportButton.SetActive(false);
            Play.SetActive(false);
            Score.SetActive(true);

            audioSource.Play();
            scriptMusicSelectObject.SetActive(true);        
            musicSelect.ButtonClicked(9999);
        }
    }
}
