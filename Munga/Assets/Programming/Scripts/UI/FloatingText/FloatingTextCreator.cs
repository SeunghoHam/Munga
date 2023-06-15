using System;
using System.Collections;
using Assets.Scripts.Manager;
using DG.Tweening;
using UniRx.Triggers;
using UnityEngine;

public class FloatingTextCreator : MonoBehaviour
{
    [SerializeField] private Transform DamageTextCreatePoint;
    [SerializeField] private DamageText damageText; // Resources폴더에서 가져오는 방식으로 가야함
    [SerializeField] private DamageText stateText;
    
    private Camera camera;
    
    [Header("TextObject")]
    [SerializeField] private Transform TextObject;
    private void Awake()
    {
        camera = Camera.main;
        TextObject.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!TextObject.gameObject.activeSelf)
            return;
        TextObject.transform.rotation = camera.transform.rotation;
    }

    public void Active(StateType stateType ,DamageType damageType,int damage)
    {
        // Create
        TextObject.gameObject.SetActive(true);
        // 위치 랜덤조정 필요함
        TextObject.transform.position = DamageTextCreatePoint.position;
        
        // Active
        damageText.gameObject.SetActive(true);
        bool active = stateType != StateType.Normal ? true : false;
        stateText.gameObject.SetActive(active);
        
        // Set
        damageText.SetDamageType(BattleManager.Instance._characterUnit.currnetWeaponDamageType);
        damageText.SetDamageText(stateType, damage);
        stateText.SetStateText(stateType);
        
        // Action
        TextObject.transform.DOLocalMoveY(TextObject.position.y + 0.2f, 0.4f)
            .OnComplete(() =>
            {
                Destroy();
            });
    }
    private void Destroy()
    {
        TextObject.gameObject.SetActive(false);
    }
}


