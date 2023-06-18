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
    [Header("Weapon SO")] 
    public WeaponSO weaponSO;
    
    [Space(10)]
    [Header("Setting")]
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
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        _changeCooldown = _weaponIcon.transform.GetChild(0).GetComponent<Image>();
        _changeCooldown.color = new Color(0, 0, 0, 0.6f);
        _changeCooldown.fillAmount = 1f;
        _changeCooldown.gameObject.SetActive(false);
        
        _weaponName.text = weaponSO.WeaponName;
    }
    
    public void SetWeaponData(int number, string name)
    {
        _keyText.text = number.ToString();
        _weaponName.text = name;
    }

    public void CoolDownActive()
    {
        _changeCooldown.gameObject.SetActive(true);
        _changeCooldown.DOFillAmount(0f, 1f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _changeCooldown.fillAmount = 1f;
                _changeCooldown.gameObject.SetActive(false);
            });
    }

    public void Init(int number, bool select)
    {
        _keyText.text = number.ToString();
        if (select)
        {
            Select();
        }
        else
        {
            DeSelect();
        }
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