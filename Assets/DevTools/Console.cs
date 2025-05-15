using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Console : MonoBehaviour
{
    //this script will put everything from the console into a text object you can put in the AR application
    //used for debugging purposes inside of the headset 
    //use the prefab in the folder to use console

    public TMP_Text logText;
    public List<string> logs;

    public int indexNum;

    private void Update()
    {
        if (logs.Count != 0)
        {
            logText.text = logs[indexNum];
        }
    }

    void OnEnable()
    {
        //get new incomming console Log
        Application.logMessageReceived += LogCallback;
    }

    void LogCallback(string logString, string stackTrace, LogType type)
    {
        //Check if the log is not already in the list
        if (!logs.Contains(logString))
        {
            //If its not in the list add it to the list
            logs.Add(logString);
        }
    }

    public void GoForwardOne()
    {
        indexNum++;
    }

    public void GoBackwardsOne()
    {
        indexNum--;
    }
}
