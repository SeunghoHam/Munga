using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEditor;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;
    private void Awake()
    {
        instance = this;
    }

    [HideInInspector]
    public enum TextColor
    {
        White,
        Red,
        Yellow,
        Blue,
        Pink,
    }
    
    [HideInInspector]
    public TextColor textColor;
    public void Log(string contents, TextColor textColor = TextColor.White)
    {
        switch (textColor)
        {
            case TextColor.White:
                Debug.Log("<color=white>" + contents + "</color>");
                break;
            case TextColor.Red:
                Debug.Log("<color=red>" + contents + "</color>");
                break;
            case TextColor.Yellow:
                Debug.Log("<color=yellow>" + contents + "</color>");
                break;
            case TextColor.Blue:
                Debug.Log("<color=aqua>" + contents + "</color>");
                break;
            case TextColor.Pink:
                Debug.Log("<color=#FF46AC>" + contents + "</color>");
                break;
        }
    }
    public void LogError(string contents)
    {
        Debug.LogError(contents);
    }

}
