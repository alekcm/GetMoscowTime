using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public class TimeController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowTimeAllert(string time);
    struct TimeData
    {
        public string datetime;
    }
    const string Api = "https://worldtimeapi.org/api/timezone/Europe/Moscow";
    public async void GetMoscowTime()
    {

        UnityWebRequest webRequest = UnityWebRequest.Get(Api);
        var operation = webRequest.SendWebRequest();
        while (!operation.isDone)
            await Task.Yield();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);
            string time = Regex.Match(timeData.datetime, @"\d{2}:\d{2}:\d{2}").ToString();
            ShowTimeAllert(time);

        }
        else
        {
            Debug.Log("Error: " + webRequest.error);
        }

    }
}
