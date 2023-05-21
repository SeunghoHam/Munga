using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Assets.Scripts.Common;
using Assets.Scripts.Manager;
using Assets.Scripts.UI.Popup.Base;
using Assets.Scripts.UI.Popup.PopupView;
using Assets.Scripts.UI;
using UnityEngine;
using LitJson;
using Debug = UnityEngine.Debug;

public class Quest // 퀘스트 리스트
{
    public string _index;
    public QuestStyle _type;
    public string _name;
    public string _content;

    public Quest(string index, QuestStyle type, string name, string contnet)
    {
        this._index = index;
        this._type = type;
        this._name = name;
        this._content = contnet;
    }
}

namespace Assets.Scripts.Manager
{
    public class QuestManager : UnitySingleton<QuestManager>
    {
        // Number, Type(icon), Name
        private const string QuestIconPrefix = "Sprite/"; // Resources +  되어있음
        //public static List<Quest> QuestList = new List<Quest>();
        private List<Quest> QuestList = new List<Quest>();

        private JsonData jsonData;
        
        
        private QuestData mainData;
        private QuestData subData;
        private QuestData hiddenData;

        // Index
        public string mainIndex = null;
        public string subIndex = null;
        public string hiddenIndex = null;

        // Name(퀘스트 이름)
        public string mainName;
        public string subName;
        public string hiddenName;

        // Content
        public string mainContent;
        public string subContent;
        public string hiddenContent;


        // 현재 활성화 퀘스트(BasicView에 보일거) 정보 가져오기
        public string currentActiveIndex;
        public QuestStyle currentActiveStyle;
        // content 는 view 에서 안보이니까 제외

        private void ParsingJsonQuest(JsonData name, List<Quest> listQuest)
        {
            for (int i = 0; i < name.Count; i++)
            {
                
                string tempIndex = name[i][0].ToString();
                QuestStyle tempType = Enum.Parse<QuestStyle>(name[i][1].ToString());
                string tempName = name[i][2].ToString();
                string tempContent = name[i][3].ToString();

                // 형변환이 존재한다면
                // 형변환 예시
                //int tempIndex_ = int.Parse(tempIndex);
                Quest tempQuest = new Quest(tempIndex, tempType, tempName, tempContent);
                listQuest.Add(tempQuest);
            }
        }

        private void Awake()
        {
            LoadBase();
        }

        public void SetValue()
        {
            Debug.Log("SetValue");
            mainIndex = DataManager.Instance.currentMainQuestIndex;
            subIndex = DataManager.Instance.currentSubQuestIndex;
            hiddenIndex = DataManager.Instance.currentHiddenQuestIndex;
            currentActiveIndex = DataManager.Instance.currentActiveIndex;
            currentActiveStyle = DataManager.Instance.currentActiveStyle;
            
            mainName = GetQuestName(mainIndex);
            subName = GetQuestName(subIndex);
            hiddenName = GetQuestName(hiddenIndex);

            mainContent = GetQuestContent(mainIndex);
            subContent = GetQuestContent(subIndex);
            hiddenContent = GetQuestContent(hiddenIndex);
        }

        private void LoadBase()
        {
            string jsonString;
            string filePath;
            filePath = Application.streamingAssetsPath + "/QuestList.json";
            // StreamingAsset를 다른 플렛폼에서 하게 되면 이 과정 거쳐줘야함
            /*
            if (Application.platform == RuntimePlatform.Android)
            {
                WWW reader = new WWW(filePath);
                while (!reader.isDone) { }
                jsonString = reader.text;
            }
            else
            {
                jsonString = File.ReadAllText(filePath);
            }*/

            jsonString = File.ReadAllText(filePath);
            jsonData = JsonMapper.ToObject(jsonString);
            ParsingJsonQuest(jsonData, QuestList);
        }

        private string GetQuestName(string number)
        {
            for (int i = 0; i < QuestList.Count; i++)
            {
                //if (QuestList[i]._index == number)
                if(QuestList[i]._index.Contains(number))
                {
                    //DebugManager.instance.Log(QuestList[i]._name + " ; Name 할당 완료", DebugManager.TextColor.Yellow);
                    return QuestList[i]._name;
                }
                else continue;
            }
            return "[QuestManager Error]"+ number + "의 Name 정보 전달이 안되있음";
        }

        private string GetQuestContent(string number)
        {
            for (int i = 0; i < QuestList.Count; i++)
            {
                if (QuestList[i]._index.Contains(number))
                {
                    //DebugManager.instance.Log(QuestList[i]._content + "Content 할당 완료", DebugManager.TextColor.Yellow);
                    return QuestList[i]._content;
                }
                else continue;
            }
            return "[QuestManager Error]"+ number + " 의 Content 정보 전달이 안되있음";
        }

        public string GetCurrentQuestName()
        {
            return GetQuestName(currentActiveIndex);
        }

        public string GetCurrentQuestContent()
        {
            return GetQuestContent(currentActiveIndex);
        }
        public void SetCurrentIndex(QuestStyle style)
        {
            switch (style)
            {
                case QuestStyle.Main:
                    currentActiveIndex = mainIndex; 
                    break;
                case QuestStyle.Sub:
                    currentActiveIndex = subIndex;
                    break;
                case QuestStyle.Hidden:
                    currentActiveIndex = hiddenIndex;
                    break;
            }
        }
        public Sprite GetQuestIcon(QuestStyle style)
        {
            return Resources.Load<Sprite>(string.Format("{0}{1}", QuestIconPrefix, style));
        }

        public void ActiveQuestChange(string index)
        {
            currentActiveIndex = index;
        }

        private QuestStyle GetCurrentStyle(string number)
        {
            for (int i = 0; i < QuestList.Count; i++)
            {
                if(QuestList[i]._index.Contains(number))
                {
                    return QuestList[i]._type;
                }
                else continue;
            }
            return QuestStyle.Main;
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void UnInitialize()
        {
            base.UnInitialize();
        }
    }
}