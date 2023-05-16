using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Common;
using Assets.Scripts.Manager;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Popup.PopupView;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Quest // 퀘스트 리스트
{
    public string _index;
    public string _type;
    public string _name;
    public Transform _questStartTransform;
    public Quest(string index, string type, string name)
    {
        this._index = index;
        this._type = type;
        this._name = name;
    }
}
namespace Assets.Scripts.Manager
{
    public class QuestManager : UnitySingleton<QuestManager>
    {
        // Number, Type(icon), Name

        private const string QuestIconPrefix = "Sprite/"; // Resources +  되어있음
        public static List<Quest> QuestList = new List<Quest>();

        private JsonData questData;

        private void ParsingJsonQuest(JsonData name, List<Quest> listQuest)
        {
            for (int i = 0; i < name.Count; i++)
            {
                string tempIndex = name[i][0].ToString();
                string tempType = name[i][1].ToString();
                string tempName = name[i][2].ToString();

                // 형변환이 존재한다면
                // 형변환 예시
                //int tempIndex_ = int.Parse(tempIndex);

                Quest tempQuest = new Quest(tempIndex, tempType, tempName);
                listQuest.Add(tempQuest);
                Debug.Log("퀘스트 정보 전달 완료");
            }
        }

        private void Awake()
        {
            LoadBase();
        }
        
        public void LoadBase()
        {
            string jsonString;
            string filePath;

            filePath = Application.streamingAssetsPath + "/MainQuest.json";
            // StreamingAsset를 다른 플렛폼에서 하게 되면 이 과정 거쳐줘야함
            if (Application.platform == RuntimePlatform.Android)
            {
                WWW reader = new WWW(filePath);
                while (!reader.isDone)
                {
                }

                jsonString = reader.text;
            }
            else
            {
                jsonString = File.ReadAllText(filePath);
            }

            questData = JsonMapper.ToObject(jsonString);
            ParsingJsonQuest(questData, QuestList);
        }

        public string GetQuestNumber(string number)
        {
            for (int i = 0; i < QuestList.Count; i++)
            {
                if (QuestList[i]._index == number)
                {
                    Debug.Log(QuestList[i]._name + " 할당 완료");
                    return QuestList[i]._name;
                }
                else continue;
            }
            return "[QuestManager Error] 퀘스트 정보 전달이 안되있음";
        }
        public Sprite GetQuestIcon(QuestStyle style)
        {
            return Resources.Load<Sprite>(string.Format("{0}{1}", QuestIconPrefix, style));
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