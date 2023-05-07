using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    // 필요한거
    /*
     * 유저 이름
     * 유저 레벨
     * 유저 경험치
     * 유저 모험 경험 레벨
     * 유저 모험 경험치
     * 유저 보유골드
     * 
     */
    
    public string userName;
    
    public int userLevel;
    public float userExp;
    
    public int userAdventureLevel;
    public float userAdventureExp;
    
    public int userGold;
    
    public List<string> inventoryItems;
}
