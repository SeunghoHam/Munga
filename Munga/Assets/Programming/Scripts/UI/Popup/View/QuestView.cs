using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using Assets.Scripts.UI.Popup.Base;
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

        [SerializeField] private Button _activeButton;

        private QuestStyle _currentType; // 현재 mainView에서 보여지고 있는 퀘스트
        private QuestStyle _selectType; // Object픽에서 눌러만 놓은 상태 (_current와 다르다면 버튼 보이게함)
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
            
            _activeButton.onClick.AddListener(ActiveQuestChange);
        }

        private void Setting()
        {
            // 현재 어디까지 했는지는 DataManager.QuestData에서 가져옴
            // 퀘스트 리스트는 QuestManager.QuestList에서 가져옴
            // Name : QuestManager.GetQuestName
            // Content : QuestManager.

            quest_Main.SetQuestPiece( 
                QuestManager.mainName,
                "메인 퀘스트");
            
            quest_Sub.SetQuestPiece( 
                QuestManager.subName,
                "서브 퀘스트");
            
            quest_Hidden.SetQuestPiece(
                QuestManager.hiddenName,
                "히든 퀘스트");
            
            // 이거 현재 상태를 가져와야함
            QuestButtonClick(QuestManager.currentActiveStyle);
            _currentType = QuestManager.currentActiveStyle;
            
            // 활성화 될때 설정 가져오기
            _currentQuestName.text = QuestManager.GetCurrentQuestName();
            _currentQuestContent.text = QuestManager.GetCurrentQuestContent();
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
                    _selectType = QuestStyle.Main;
                    CurrentChooseChange(QuestManager.mainName, QuestManager.mainContent);
                    break;
                case QuestStyle.Sub:
                    _selectType = QuestStyle.Sub;
                    CurrentChooseChange(QuestManager.subName, QuestManager.subContent);
                    break;
                case QuestStyle.Hidden:
                    _selectType = QuestStyle.Hidden;
                    CurrentChooseChange(QuestManager.hiddenName, QuestManager.hiddenContent);
                    break;
            }
        }

        private void CurrentChooseChange(string name, string content)
        {
            ButtonEnable();
            _currentQuestName.text = name;
            _currentQuestContent.text = content;
        }

        private void ButtonEnable()
        {
            if (_currentType == _selectType)
                _activeButton.gameObject.SetActive(false);
            else
                _activeButton.gameObject.SetActive(true);
                
        }
        private void ActiveQuestChange()
        {
            _currentType = _selectType;
            _activeButton.gameObject.SetActive(false);
            
            QuestManager.SetCurrentIndex(_currentType);
            QuestManager.currentActiveStyle = _currentType;
            
            PopupManager.PopupList[0].GetComponent<UIPopupBasic>().transform.GetChild(0).GetComponent<BasicView>().questBasicPart.QuestDataInit();
            // basicView에서 활성화 시킬 퀘스트 변경하기

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