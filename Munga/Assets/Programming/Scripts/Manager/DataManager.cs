using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using System.IO;

namespace Assets.Scripts.Manager
{
    public class DataManager : UnitySingleton<DataManager>
    {
        // PlayerPrefs저장 경로 반환
        //private string savePath = Application.persistentDataPath;

        #region ::: QuestData :::

        public string currentQuestStyle;
        public string currentQuestName;

        #endregion
        #region ::: Path :::
        
        private const string ResourcesPath = "Resources/UserData/";
        private string _userDataPath;
        private string _questDataPath;
        
        #endregion

        public UserData UserData { get; private set;}

        private void Awake()
        {
            _userDataPath = Path.Combine(Application.dataPath, ResourcesPath, "UserData.json");
            //_questDataPath = Path.Combine(Application.dataPath, ResourcesPath, "QuestData.json");
            //_questDataPath = Path.Combine
            JsonLoad(_userDataPath);
            //JsonLoad(_questDataPath);
            //JsonSave(true);
        }

        #region ::: Runes :::
        public void GetRunes(int runesAmount)
        {
            //PlayerData playerData = new PlayerData();
            // 증가는 그냥 시키고?
            // 저장을 한번에 받는게 좋을듯
            UserData.userGold += runesAmount;
        }
        
        #endregion
        
        public void JsonLoad(string path)
        {
            if (!File.Exists(path)) // json 파일이 비어있다면 처음부터 생성함
            {
                Debug.Log("PlayerData X, 새로운 저장 만들기");
                JsonSave(false);
            }
            else // 비어있지 않은 경우
            {
                //JsonSave(true);
                Debug.Log("PlayerData O");
                string loadJson = File.ReadAllText(path);
                UserData = JsonUtility.FromJson<UserData>(loadJson);
                // 값 할당시키기
                if (UserData != null)
                {
                    Debug.Log("캐릭터 인스턴스에 값 전달");
                    // 이 부분에서 전달해야함
                }
            }
        }
        
        public void JsonSave(bool isOn)
        {
            DebugManager.instance.Log("playerData [SAVE]",DebugManager.TextColor.Yellow);
            //_playerData //;= new PlayerData();
            if (!isOn)
            {
                Debug.Log("새로운 값으로 저장");
                NewCharacterCreate();
            }
            
            string json = JsonUtility.ToJson(UserData, true);
            File.WriteAllText(_userDataPath, json);
            Debug.Log("플레이어 데이터 : " +  UserData.userName);
            //Debug.Log("저장 완료" + json.ToString());
        }

        private void JsonDataCreate(string _filePath)
        {
            // 새로운 json 저장체 생성
            string json = JsonUtility.ToJson(UserData, true);
            File.WriteAllText(_userDataPath, json);
        }

        // 처음 게임을 키게 되면 유저가 하게 해야하는데 일단 바로 적용되도록 함
        private void NewCharacterCreate()
        {
            UserData = new UserData();
            UserData.userName = "Jungi";
            
            UserData.userExp = 1;
            UserData.userLevel = 1;
            
            UserData.userAdventureLevel = 1;
            UserData.userAdventureExp = 1;
            
            UserData.userGold = 1000;
            
            //Debug.Log("플레이어 데이터 : " +  _playerData);
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