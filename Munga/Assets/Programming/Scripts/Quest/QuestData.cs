using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    // 필요한거
    /*
     * 퀘스트 분류 번호 n.n.n 식으로
     * 위치정보 ( 외부에서 할당할 수 있게)
     */
    [field: Header("퀘스트 분류 번호")]
    public string questNumber = "0.0.0";
    
    [field: Header("퀘스트 이름")]
    public string questName = "뭔가뭔가하기";
    //public string subName;
    //private Transform questPosition;

}
