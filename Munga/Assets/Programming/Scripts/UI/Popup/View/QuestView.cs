using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Assets.Scripts.UI.Popup.PopupView
{
    public class QuestView : ViewBase
    {
        public QuestManager QuestManager { get; set; }
        [SerializeField] private QuestPiece quest_Main;
        [SerializeField] private QuestPiece quest_Sub;
        [SerializeField] private QuestPiece quest_Hidden;

        //[SerializeField] private Transform QuestContent;
        [SerializeField] private Text _currentQuestName;
        [SerializeField] private Text _currentQuestContent;

        private QuestStyle _currentType;
        private void Awake()
        {
            Allocation();
        }

        private void Allocation()
        {
            //quest_Main = contentPart.GetChild(0).GetComponent<QuestPiece>();
            //quest_Sub = contentPart.GetChild(1).GetComponent<QuestPiece>();
            //quest_Hidden = contentPart.GetChild(2).GetComponent<QuestPiece>();

            //_currentQuestName = QuestContent.GetChild(0).GetComponent<Text>();
            //_currentQuestContent = QuestContent.GetChild(1).GetComponent<Text>();
            
            //quest_Main.GetComponent<Button>().onClick.AddListener(QuestButton_Main);
            quest_Main.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => QuestButtonClick(QuestStyle.Main));
            quest_Sub.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => QuestButtonClick(QuestStyle.Sub));
            quest_Hidden.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => QuestButtonClick(QuestStyle.Hidden));
        }

        private void Setting()
        {
            // 현재 어디까지 했는지는 DataManager.QuestData에서 가져옴
            // 퀘스트 리스트는 QuestManager.QuestList에서 가져옴
            // Name : QuestManager.GetQuestName
            // Content : QuestManager.

            quest_Main.SetQuestPiece2( 
                QuestManager.mainName,
                "메인 퀘스트");
            
            quest_Sub.SetQuestPiece2( 
                QuestManager.subName,
                "서브 퀘스트");
            
            quest_Hidden.SetQuestPiece2(
                QuestManager.hiddenName,
                "히든 퀘스트");
            
            // 이거 현재 상태를 가져와야함
            QuestButtonClick(QuestStyle.Main);
            _currentType = QuestStyle.Main;
        }
        private void OnEnable()
        {
            Show();
            Setting();
        }

        private void QuestButtonClick(QuestStyle style)
        {
            switch (style)
            {
                case QuestStyle.Main:
                    CurrentChooseChange(QuestManager.mainName, QuestManager.mainContent);
                    break;
                case QuestStyle.Sub:
                    CurrentChooseChange(QuestManager.subName, QuestManager.subContent);
                    break;
                case QuestStyle.Hidden:
                    CurrentChooseChange(QuestManager.hiddenName, QuestManager.hiddenContent);
                    break;
            }
        }

        private void CurrentChooseChange(string name, string content)
        {
            _currentQuestName.text = name;
            _currentQuestContent.text = content;
        }
        private void QuestButton_Main()
        {
            
        }
        public override void Show()
        {
            base.Show();
        }
        public override void Hide()
        {
            base.Hide();
        }
    }
}