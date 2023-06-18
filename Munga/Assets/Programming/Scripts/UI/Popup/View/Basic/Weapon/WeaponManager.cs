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
        }
    }


    public void FirstSetting()
    {
        _weaponPieces[0].Init(1,true);
        _weaponPieces[1].Init(2,false);
        _weaponPieces[2].Init(3,false);
    }

    private WaitForSeconds perSec = new WaitForSeconds(1f);
    IEnumerator CoolDownRoutine()
    {
        _canChange = false;
        yield return perSec;
        _canChange = true;
        yield break;
    }
    public void SelectWeapon(int num)
    {
        if (!_canChange)
            return;
        if (_previousNumber == num)
            return;
        
        StartCoroutine(CoolDownRoutine());
        _weaponPieces[_previousNumber-1].DeSelect();
        _weaponPieces[num-1].Select();
        _previousNumber = num;
        
        PieceCooldown(num-1); // Deselect와 별개로 동작시키면 성능에 큰 영향을 미칠까? 아니라고봄
    }

    private void PieceCooldown(int num) // 입력받은 num은 -1 된 수치
    {
        for (int i = 0; i < _weaponPieces.Length; i++)
        {
            if(i!= num)
                _weaponPieces[i].CoolDownActive();

        }
    }
}
