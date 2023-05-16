using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI.Popup.PopupView;

public class QuestObject : MonoBehaviour
{
    private BasicView _basicView;

    [SerializeField] private Image _questIcon;

    [SerializeField] private Text _questName;

    [SerializeField] private Text _questStartPosition;

    public void QuestSet(BasicView view)
    {
        _basicView = view;
        QuestDataSetting(QuestStyle.Main, "0.0.1");
    }
    
    private void QuestDataSetting(QuestStyle style, string index)
    {
        _questName.text = _basicView.QuestManager.GetQuestNumber(index);
        _questIcon.sprite = _basicView.QuestManager.GetQuestIcon(style);
    }
}