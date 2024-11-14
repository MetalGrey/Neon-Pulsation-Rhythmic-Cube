using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;

public class LogSender : MonoBehaviour
{
    private string logServerUrl = ""; //URL
    private Queue<string> logQueue = new Queue<string>();
    private bool isSending = false;

    void Start()
    {
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
        {
            if (logString.Contains("Insecure connection not allowed")) 
            {
                return; 
            }
            string ipAddress = GetLocalIPAddress();
            string DeviceName = GetDeviceName();

            string logMessage = $"[Log] {System.DateTime.Now}: [{type}] {logString}\n{stackTrace}\nIP: {ipAddress}\nDevice name: {DeviceName}";

            logQueue.Enqueue(logMessage);

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

            WWWForm form = new WWWForm();
            form.AddField("log", logMessage);

            using (UnityWebRequest www = UnityWebRequest.Post(logServerUrl, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error log send: {www.error}");
                }
                else
                {
                    Debug.Log("Log sended");
                }
            }
        }

        isSending = false;
    }

    void OnDestroy()
    {
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
