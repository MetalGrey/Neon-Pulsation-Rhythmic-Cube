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

    public Button PlayB;
    public Button SettingsB;
    public Button ImportB;

    public AudioSource SfxSource;

    void Start()
    {
        musicSelect = scriptMusicSelectObject.GetComponent<MusicSelect>();
        loadMusicButton.onClick.AddListener(OpenFileBrowser);
    }

    void OpenFileBrowser()
    {
        ImportB.interactable = false;
        SettingsB.interactable = false;
        PlayB.interactable = false;
        // Устанавливаем фильтр для показа только mp3 файлов
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Audio Files", ".mp3"));
        // Открываем диалоговое окно выбора файла
        StartCoroutine(ShowLoadDialogCoroutine());
        SfxSource.Play();
    }

    IEnumerator ShowLoadDialogCoroutine()
    {

        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select music", "Select");

        if (FileBrowser.Success)
        {

            string musicFilePath = FileBrowser.Result[0];
            StartCoroutine(LoadAudio(musicFilePath));
        }
        else
        {
            ImportB.interactable = true;
            SettingsB.interactable = true;
            PlayB.interactable = true;
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
