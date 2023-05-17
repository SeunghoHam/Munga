using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponPiece[] _weaponPieces;
    private int _previousNumber = 1;
    private bool _canChange= true;
    private void Awake()
    {
        int num = this.transform.childCount;
        _weaponPieces = new WeaponPiece[num];
        for (int i = 0; i < num; i++)
        {
            _weaponPieces[i] = this.transform.GetChild(i).GetComponent<WeaponPiece>();
            int weaponNum = i + 1;
            if (i == 0)
            {
                _weaponPieces[i].SetWeaponData(weaponNum, "검준기");
            }   
            else if (i == 1)
            {
                _weaponPieces[i].SetWeaponData(weaponNum, "창민머리");
            }
            else
            {
                _weaponPieces[i].SetWeaponData(weaponNum, "활데");
            }
        }
    }


    public void FirstSetting()
    {
        _weaponPieces[0].Select();
        _weaponPieces[1].DeSelect();
        _weaponPieces[2].DeSelect();
    }
    // 무기 이름, 정보같은거는 WeaponData 정리되면 받아오는식으로 해야함
    public void SelectWeapon(int num)
    {
        if (!_canChange)
            return;
        if (_previousNumber == num)
            return;
        
        _weaponPieces[_previousNumber-1].DeSelect();
        _weaponPieces[num-1].Select();
        _previousNumber = num;
    }
}
