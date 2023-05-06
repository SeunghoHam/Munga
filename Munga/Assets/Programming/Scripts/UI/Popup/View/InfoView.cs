using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common.DI;
using Assets.Scripts.Manager;
using UnityEngine.UI;
using DG.Tweening;

namespace Assets.Scripts.UI.Popup.PopupView
{ 
    public class InfoView : ViewBase
    {

        [SerializeField] private Image sideObject; // 인포 좌측 선같은거
        
        [Space(10)]
        [SerializeField] private GameObject userInfoObject; // 유저 정보

        [SerializeField] private Button _extiButton;
        private void Start()
        {
            Show();
            _extiButton.onClick.AddListener(Hide);
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
}