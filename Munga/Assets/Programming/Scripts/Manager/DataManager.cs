using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using System.IO;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Popup.PopupView;

namespace Assets.Scripts.Manager
{
    public class DataManager : UnitySingleton<DataManager>
    {
        // PlayerPrefs저장 경로 반환
        //private string savePath = Application.persistentDataPath;

        #region ::: QuestData :::

        public QuestStyle currentActiveStyle;
        public string currentActiveIndex;
        public string currentMainQuestIndex;
        public string currentSubQuestIndex;
        public string currentHiddenQuestIndex;

        #endregion

        #region ::: Path :::

        private const string ResourcesPath = "Resources/UserData/";
        private string _userDataPath;
        private string _questPath;

        #endregion

        public UserData UserData { get; private set; }
        public QuestData QuestData { get; private set; }

        private void Awake()
        {
            _userDataPath = Path.Combine(Application.dataPath, ResourcesPath, "UserData.json");
            _questPath = Path.Combine(Application.dataPath, ResourcesPath, "QuestData.json");
            Load();
            QuestManager.Instance.SetValue();
        }

        public void Save()
        {
            JsonSave_QuestData();
            JsonSave_UserData(true);
        }

        public void Load()
        {
            JsonLoad_UserData();
            JsonLoad_QuestData();
        }

        #region ::: Load  :::

        public void JsonLoad_UserData()
        {
            if (!File.Exists(_userDataPath)) // json 파일이 비어있다면 처음부터 생성함
            {
                DebugManager.instance.Log("UserData [X], 새로운 저장 만들기");
                JsonSave_UserData(false);
            }
            else // 비어있지 않은 경우
            {
                string loadJson = File.ReadAllText(_userDataPath);
                UserData = JsonUtility.FromJson<UserData>(loadJson);
                if (UserData != null)
                {
                    Debug.Log("<color=aqua> [DataManager] 캐릭터 인스턴스에 값 전달</color>");
                }
            }
        }

        public void JsonLoad_QuestData()
        {
            if (!File.Exists(_questPath))
            {
                DebugManager.instance.Log("Quest [X], 새로운 저장 만들기");
                //QuestDataJsonSave();
            }
            else // 비어있지 않은 경우
            {
                string loadJson = File.ReadAllText(_questPath);
                QuestData = JsonUtility.FromJson<QuestData>(loadJson);
                if (QuestData != null)
                {
                    Debug.Log("<color=aqua> [DataManager] 퀘스트 인스턴스에 값 전달</color>");
                    currentMainQuestIndex = QuestData.currentMainIndex;
                    currentSubQuestIndex = QuestData.currentSubIndex;
                    currentHiddenQuestIndex = QuestData.currentHiddenIndex;

                    currentActiveIndex = QuestData.currentActiveIndex;
                    currentActiveStyle = QuestData.currentActiveStyle;
                    //Debug.Log("currentMainQuestIndex" + currentMainQuestIndex);
                    //Debug.Log("currentSubQuestIndex" + currentSubQuestIndex);
                    //Debug.Log("currentHiddenQuestIndex" + currentHiddenQuestIndex);
                    //DebugManager.instance.Log(QuestData.currentMainIndex, DebugManager.TextColor.Blue);
                }
            }
        }

        #endregion


        #region ::: Save :::
        public void JsonSave_UserData(bool isOn)
        {
            DebugManager.instance.Log("UserData [SAVE]", DebugManager.TextColor.Yellow);
            //_playerData //;= new PlayerData();
            if (!isOn)
            {
                Debug.Log("새로운 값으로 저장");
                NewUserDataCreate();
            }

            string json = JsonUtility.ToJson(UserData, true);
            File.WriteAllText(_userDataPath, json);
            //Debug.Log("플레이어 데이터 : " +  UserData.userName);
            //Debug.Log("저장 완료" + json.ToString());
        }

        public void JsonSave_QuestData()
        {
            DebugManager.instance.Log("QuestData [SAVE]", DebugManager.TextColor.Yellow);
            string json = JsonUtility.ToJson(QuestData, true);
            File.WriteAllText(_questPath, json);
            //Debug.Log("퀘스트 index : " +  QuestData.questNumber);
        }

        
        #endregion

        private void JsonDataCreate(string _filePath)
        {
            // 새로운 json 저장체 생성
            string json = JsonUtility.ToJson(UserData, true);
            File.WriteAllText(_userDataPath, json);
        }

        // 처음 게임을 키게 되면 유저가 설정한 값으로 적용되도록 함
        private void NewUserDataCreate()
        {
            UserData = new UserData();
            UserData.userName = "Jungi";

            UserData.userExp = 1;
            UserData.userLevel = 1;

            UserData.userAdventureLevel = 1;
            UserData.userAdventureExp = 1;

            UserData.userGold = 0;

            //Debug.Log("플레이어 데이터 : " +  _playerData);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                QuestData.currentMainIndex = QuestManager.Instance.mainIndex;
                QuestData.currentSubIndex = QuestManager.Instance.subIndex;
                QuestData.currentHiddenIndex = QuestManager.Instance.hiddenIndex;
                
                QuestData.currentActiveIndex = QuestManager.Instance.currentActiveIndex;
                QuestData.currentActiveStyle = QuestManager.Instance.currentActiveStyle;
                DebugManager.instance.Log("퀘스트 데이터 저장함");
                JsonSave_QuestData();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                PopupManager.Instance.PopupList[0].transform.GetChild(0).GetComponent<BasicView>().questBasicPart.QuestDataSetting();
            }
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