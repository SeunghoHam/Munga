using Assets.Scripts.Common.DI;
using Assets.Scripts.Manager;
using Assets.Scripts.MangeObject;
using Assets.Scripts.UI.Popup.Base;
using Assets.Scripts.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEditor.UIElements;


namespace Assets.Scripts.UI.Popup.PopupView
{
    public class BasicView : MonoBehaviour
    {
        public FlowManager FlowManager { get; set; }
        public ResourcesManager ResourcesManager { get; set; }
        public PopupManager PopupManager { get; set; }

        [SerializeField] private InputActionReference EscToggleInputAction;
        
        [Header("SideObject")]
        [SerializeField] private Image sideObject; // 인포 좌측 선같은거
        
        [Space(10)]
        [SerializeField] private GameObject userInfoObject; // 유저 정보
        
        public enum CurrentViewType
        {
            None,
            Status,
            System,
        }

        public CurrentViewType _currentViewType;


        #region ::: bool Data :::
        private bool _isActive = false; // 활성화 여부
        private bool _canInteract = true;
        #endregion
        
        
        private void Start()
        {
            Init();
            AddEvent();
            DependuncyInjection.Inject(this);
        }
        private void Init()
        {
            sideObject.gameObject.SetActive(false);
            
            userInfoObject.SetActive(false);
        }
        private void AddEvent()
        {
            /*
            this.ObserveEveryValueChanged(_ => Character.Instance.CurHP)
                //.Where(_ => )
                .Subscribe()
                .AddTo(gameObject);
            */
            //Input.InputActions.Player.ESC.started += ESC;
            EscToggleInputAction.action.started += ESC;
        }

        private void ESC(InputAction.CallbackContext context)
        {
            ToggleESC();
        }

        private void ToggleESC()
        {
            if (!_canInteract)
                return;
            _canInteract = false; // 각 활/비활성화 상태에서 true로 만들어줘야함
            
            if (_isActive) // Active -> DisActive
            {
                //Debug.Log("비활성화");
                _isActive = false;
                
                InfoDisActive();
                return;
            }
            
            
            else // DisActive -> Active
            {
                //Debug.Log("활성화");
                _isActive = true;
                
                InfoActive();
                return;
            }
        }

        #region ::: Side Object :::
        private void InfoActive()
        {
            _canInteract = true;
            
            FlowManager.Instance.AddSubPopup(PopupStyle.Info);
            /*
            sideObject.gameObject.SetActive(true);
            sideObject.DOFade(1f, 0.2f).From(0f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _canInteract = true;
                });*/
        }

        private void InfoDisActive()
        {
            _canInteract = true;
            
            //UI.PopupManager.Instance.PopupList[1].GetComponent<UIPopupInfo>().Hide();
            PopupManager.PopupList[1].GetComponent<UIPopupInfo>().Hide();
            //PopupManager.PopupList[1].GetComponent<>()
            /*
            sideObject.DOFade(1f, 0.2f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _canInteract = true;
                    sideObject.gameObject.SetActive(false);
                });*/
        }
        #endregion

        #region ::: User Info

        /// <summary> 사용자 정보 UI 의 활성화 정도</summary>
        /// <param name="active"></param>
        private void UserInfoActive(bool active)
        {
            if (active)
            {
                
            }
            else
            {
                
            }
        }
        

        #endregion
        
        
        
    }
}