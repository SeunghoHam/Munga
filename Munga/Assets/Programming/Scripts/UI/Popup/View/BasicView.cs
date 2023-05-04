using Assets.Scripts.Common.DI;
using Assets.Scripts.Manager;
using Assets.Scripts.MangeObject;
using Assets.Scripts.UI.Popup.Base;
using Assets.Scripts.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Transactions;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEditor.UIElements;


namespace Assets.Scripts.UI.Popup.PopupView
{
    public class BasicView : ViewBase
    {
        #region ::: ObjectRange:::
        
        [Header("Range")]
        [SerializeField] private GameObject topRange;
        [SerializeField] private GameObject bottomRange;
        [SerializeField] private GameObject rightRange;
        //[SerializeField] private GameObject leftRange;
        [SerializeField] private GameObject miniMapObject;
        [SerializeField] private GameObject questObject;
        #endregion

        private MinimapManager minimap;
        private ActiveSwordManager activeSword;

        [Space(10)]
        [Header("Button")]
        [SerializeField] private Button _swordButotn;
        [SerializeField] private Button _invenButton;
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
            DependuncyInjection.Inject(this);
        }
        
        protected override void Init()
        {
            _swordButotn.OnClickAsObservable().Subscribe(_ =>
                {
                    Hide();
                    FlowManager.AddSubPopup(PopupStyle.SwordMenu);
                }
            );
            _invenButton.OnClickAsObservable().Subscribe(_ =>
            {
                Hide();
            });
            //EscToggleInputAction.action.started += ESC;
        }

        
        protected override void OnEscStarted(InputAction.CallbackContext context)
        {
            base.OnEscStarted(context);
            Hide();
            ToggleESC();
        }

        protected override void OnCharacterStarted(InputAction.CallbackContext context)
        {
            base.OnCharacterStarted(context);
        }

        protected override void OnQuestStarted(InputAction.CallbackContext context)
        {
            base.OnQuestStarted(context);
        }

        private void ToggleESC()
        {
            if (!_canInteract)
                return;
            _canInteract = false; // 각 활/비활성화 상태에서 true로 만들어줘야함
            
            if (_isActive) // Active -> DisActive
            {
                _isActive = false;
                
                InfoDisActive();
                return;
            }
            else // DisActive -> Active
            {
                _isActive = true;
                
                InfoActive();
                return;
            }
        }
        
        private void InfoActive()
        {
            _canInteract = true;
            FlowManager.Instance.AddSubPopup(PopupStyle.Info);
            
        }

        private void InfoDisActive()
        {
            _canInteract = true;
            PopupManager.PopupList[1].GetComponent<UIPopupInfo>().Hide();
        }
        
        #region Inherit Methods
        
        private float _duration = 0.5f;
        //private float topDest = 128f;
        //private float rightDest = 128f;
        protected override void Show()
        {
            
        }
        protected override void Hide()
        {
            // 미니맵 - 사이즈 사라지기
            // 퀘스트 - 사이즈 사라지기
            
            // 상단UI - 위로 올라가기 
            // 오른쪽UI - 연해지면서 오른쪽 이동
            // 체력UI - 아래로 사라지면서 연해짐
            miniMapObject.transform.DOScale(0f, _duration).SetEase(Ease.Linear);
            questObject.transform.DOScale(0f, _duration).SetEase(Ease.Linear);
            
            topRange.GetComponent<RectTransform>().DOMoveY( 1080f + 64f, _duration).SetEase(Ease.Linear);
            bottomRange.GetComponent<RectTransform>().DOMoveY(-64f, _duration).SetEase(Ease.Linear);
            rightRange.GetComponent<RectTransform>().DOMoveX(1920f +64f, _duration).SetEase(Ease.Linear);
        }

        #endregion
    }
}