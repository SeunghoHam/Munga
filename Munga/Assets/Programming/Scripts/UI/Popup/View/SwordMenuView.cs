using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Assets.Scripts.UI.Popup.PopupView
{
    public class SwordMenuView : ViewBase
    {
        [Header("좌측 버튼들")]
        [SerializeField] private Button buttonProperty; // 속성
        [SerializeField] private Button buttonSacared; // 성유물

        [SerializeField] private Image whiteBG;
        [SerializeField] private GameObject viewObejct;

        private void OnEnable()
        {
            Init();
        }

        protected override void Init()
        {
            //whiteBG.color = new Color(1, 1, 1,0);
            viewObejct.SetActive(false);
            whiteBG.DOFade(0.75f, 0.25f).From(0f).SetEase(Ease.Linear)
                .OnComplete(()=>
                {
                    viewObejct.SetActive(true);
                    whiteBG.DOFade(0f, 0.25f).SetEase(Ease.Linear);
                });
            
            base.Init();
        }

        protected override void Hide()
        {
            base.Hide();
        }
    }
}