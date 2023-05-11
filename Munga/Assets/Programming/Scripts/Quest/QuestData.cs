using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    // 필요한거
    /*
     * 퀘스트 분류 번호 n.n.n 식으로
     * 퀘스트 메인 이름 ex) 강호의 출처 
     * 퀘스트 서브 이름 ex) 
     * 위치정보 ( 외부에서 할당할 수 있게)
     */
    public string questNumber;
    public string mainName;
    public string subName;
    public Transform questPosition;

}
