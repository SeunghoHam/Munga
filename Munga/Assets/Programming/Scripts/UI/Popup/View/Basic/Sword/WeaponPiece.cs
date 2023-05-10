using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WeaponPiece : MonoBehaviour
{
    public bool _isSelect;
    private Animator _animator;
    
    // PieceObject

    private Image _keyImage; // 키보드 키
    [SerializeField] private Text _keyText; // 키보드 키

    [SerializeField] private Image _selectBackImage; // 선택된 오브젝트에 활성화되는 뒷배경
    [SerializeField] private Image _deSelectBackImage; // 선택되지 않은 오브젝트에 활성화되는 뒷배경

    [SerializeField] private Image _selectIcon; // 선택된 오브젝트에 활성화되는 아이콘 ◀ 모양

    [SerializeField] private Text _weaponName; // 무기 이름

    [SerializeField] private Image _cooldownSlider_back; // 쿨타임 슬라이더
    private Image _cooldownSlider_fropnt; // 쿨타임 슬라이더


    [SerializeField] private Image _weaponIcon;
    private Image _changeCooldown;


    #region ::: Color ::

    private Color _changeImageColor = new Color(255, 255, 255, 100);
    private Color _changeTextColor = new Color(0, 0, 0, 100);

    #endregion


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        _changeCooldown = _weaponIcon.transform.GetChild(0).GetComponent<Image>();

        if (_isSelect)
        {
            
        }
        else
        {
            
        }
    }
    
    public void SetWeaponData(int number, string name)
    {
        _keyText.text = number.ToString();
        _weaponName.text = name;
    }
    public void Select()
    {
        _animator.SetTrigger("Select");
    }

    public void DeSelect()
    {
        _animator.SetTrigger("DeSelect");
    }
}