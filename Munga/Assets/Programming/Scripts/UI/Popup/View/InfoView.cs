using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common.DI;
using Assets.Scripts.Manager;
using Assets.Scripts.UI.Popup.Base;
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

        
        [SerializeField] private Text _userName;

        #region ::: AdventureExp :::

        [SerializeField] private Slider _userAdventureExpSlider;
        [SerializeField] private Text _userAdventureLevel;
        [SerializeField] private Text _userAdventureExp;
        
        private float _sliderMaxValue;
        #endregion
        
        
        private void OnEnable()
        {
            Show();
            UserDataInit();
            _extiButton.onClick.AddListener(Hide);
        }
        
        private void UserDataInit()
        {
            _userName.text = DataManager.UserData.userName;
            _userAdventureLevel.text = DataManager.UserData.userAdventureLevel.ToString();


            
            // AdventrueExp

            _sliderMaxValue = 500 + DataManager.UserData.userAdventureLevel * 10;
            _userAdventureExpSlider.maxValue = _sliderMaxValue;
            _userAdventureExpSlider.value = DataManager.UserData.userAdventureExp; 
            string adventureExp = $"{DataManager.UserData.userAdventureExp.ToString()} / {_sliderMaxValue}";
            _userAdventureExp.text = adventureExp;
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            InputManager._cameraCursor.DisableCursor();
            PopupManager.PopupList[0].GetComponent<UIPopupBasic>()._basicView.Show();
            base.Hide();
        }
    }
}