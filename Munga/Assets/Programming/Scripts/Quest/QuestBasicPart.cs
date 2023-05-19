using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI.Popup.PopupView;

public class QuestBasicPart : MonoBehaviour
{
    private BasicView _basicView;

    [SerializeField] private Image _questIcon;

    [SerializeField] private Text _questName;

    [SerializeField] private Text _questStartPosition;

    public void QuestSet(BasicView view)
    {
        _basicView = view;
        QuestDataSetting(_basicView.QuestManager.currentActiveStyle, _basicView.QuestManager.currentActiveQuest);
    }
    
    private void QuestDataSetting(QuestStyle style, string index)
    {
        // 현재 활성화 시켜놓은거 가져와야함
        //_questName.text = _basicView.QuestManager.;
        //_questIcon.sprite = _basicView.QuestManager.GetQuestIcon(style);
    }
}