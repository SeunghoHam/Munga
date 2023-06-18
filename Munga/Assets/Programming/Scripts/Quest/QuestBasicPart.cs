using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI.Popup.PopupView;

public class QuestBasicPart : MonoBehaviour
{
    //private BasicView _basicView;

    [SerializeField] private Image _questIcon;
    [SerializeField] private Text _questName;

    public void QuestDataInit()
    {
        // 현재 활성화 시켜놓은거 가져와야함
        _questIcon.sprite = QuestManager.Instance.GetQuestIcon(QuestManager.Instance.currentActiveStyle);
        _questName.text = QuestManager.Instance.GetCurrentQuestName();
    }
}