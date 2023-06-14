using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FloatingTextCreator : MonoBehaviour
{
    [SerializeField] private Transform DamageTextCreatePoint;
    [SerializeField] private DamageText damageText; // Resources폴더에서 가져오는 방식으로 가야함

    public void Create(int damage)
    {
        // DamagePoint 근처의 랜덤으로 damageText.gameobject 의 위치 설정
        damageText.transform.position = DamageTextCreatePoint.position;
        damageText.Active(damage);
    }

    public void Destory()
    {
        damageText.Destroy();
    }
    
}
