using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;

public class LogSender : MonoBehaviour
{
    private string logServerUrl = ""; // URL сервера для отправки логов
    private Queue<string> logQueue = new Queue<string>();
    private bool isSending = false;

    void Start()
    {
        // Подписываемся на обработчик логов Unity
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Отправляем только ошибки, исключения и assert'ы
        if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
        {
            if (logString.Contains("Insecure connection not allowed")) 
            {
                return; // Просто выходим из метода, не добавляя эту ошибку в лог
            }
            // Получаем IP-адрес и MAC-адрес
            string ipAddress = GetLocalIPAddress();
            string DeviceName = GetDeviceName();

            // Формируем лог-сообщение
            string logMessage = $"[Log] {System.DateTime.Now}: [{type}] {logString}\n{stackTrace}\nIP: {ipAddress}\nDevice name: {DeviceName}";
            // Добавляем лог в очередь
            logQueue.Enqueue(logMessage);

            // Если отправка логов не идет, запускаем процесс отправки
            if (!isSending)
            {
                StartCoroutine(SendLogs());
            }
        }
    }

    IEnumerator SendLogs()
    {
        isSending = true;

        while (logQueue.Count > 0)
        {
            string logMessage = logQueue.Dequeue();

            // Создаем форму для отправки данных
            WWWForm form = new WWWForm();
            form.AddField("log", logMessage);

            using (UnityWebRequest www = UnityWebRequest.Post(logServerUrl, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Ошибка отправки логов: {www.error}");
                }
                else
                {
                    Debug.Log("Лог успешно отправлен");
                }
            }
        }

        isSending = false;
    }

    void OnDestroy()
    {
        // Отписываемся от обработчика логов Unity
        Application.logMessageReceived -= HandleLog;
    }

    private string GetLocalIPAddress()
    {
        string localIP = "";
        foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
    private string GetDeviceName()
    {
        string deviceName = System.Environment.MachineName;
        return deviceName;
    }
}
