using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;

public class QuestPiece : MonoBehaviour
{
    [SerializeField] private Image _typeIcon;
    [SerializeField] private Text _questName;
    [SerializeField] private Text _questContent;


    /// <summary>
    /// QuestPiece 설정하기
    /// </summary>
    /// <param name="style">퀘스트 타입</param>
    /// <param name="_name">퀘스트 이름</param>
    /// <param name="_content">퀘스트 설명</param>
    public void SetQuestPiece(QuestStyle _style, string _name, string _content)
    {
        SetIcon(_style);
        SetName(_name);
        SetContent(_content);
    }
    public void SetQuestPiece2(string _name, string _content)
    {
        //SetIcon(_style);
        SetName(_name);
        SetContent(_content);
    }
    private void SetIcon(QuestStyle style)
    {
        _typeIcon.sprite = QuestManager.Instance.GetQuestIcon(style);
    }
    private void SetName(string _name)
    {
        _questName.text = _name;
    }

    private void SetContent(string _content)
    {
        _questContent.text = _content;
    }
}
