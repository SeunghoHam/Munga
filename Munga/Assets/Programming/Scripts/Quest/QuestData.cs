using UnityEngine;

[System.Serializable]
public class QuestData
{
    [field: Header("현재 퀘스트 index")]
    public string currentIndex = "0.0.0";
    
    [field: Header("현재 퀘스트 이름")]
    public string currentName = "퀘스트 이름";

    [field: Header("퀘스트 설명")] 
    public string currentContent = "준기가 잃어버린 머리카락을 찾으러 떠나는 여행임";
    
    public string currentMainIndex;
    public string currentSubIndex;
    public string currentHiddenIndex;
}
