using Assets.Scripts.Common.DI;
using Assets.Scripts.Manager;
using Assets.Scripts.MangeObject;
using Assets.Scripts.UI.Popup.Base;
using Assets.Scripts.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;


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

        [SerializeField] private GameObject dashObject; 
        #endregion


        private Image backDashImage;
        private Image frontDashImage;
        
        private MinimapManager minimap;
        private ActiveSwordManager activeSword;
        
        
        [Space(10)]
        [Header("Button")]
        [SerializeField] private Button _swordButotn;
        [SerializeField] private Button _invenButton;

        private Animator _uiAnimator;
        
        #region ::: bool Data :::
        private bool _isActive = false; // 활성화 여부
        private bool _canInteract = true;
        #endregion
        
        private void Start()
        {
            Init();
            _uiAnimator = GetComponent<Animator>();

            backDashImage = dashObject.transform.GetChild(0).GetComponent<Image>();
            frontDashImage = dashObject.transform.GetChild(1).GetComponent<Image>();
            
            InputManager._input.InputActions.UI.ESC.started += OnEscStarted;
            InputManager._input.InputActions.UI.Character.started += OnSwordMenuStarted;
        }
        
        private void Init() // View
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
        }
        
        #region ::: Dash :::

        private float _dashDuration = 0.1f;
        public void DashEnable()
        {
            dashObject.transform.localScale = Vector3.one;
            frontDashImage.DOFade(1f, _dashDuration).From(0f).SetEase(Ease.Linear);
            backDashImage.DOFade(1f, _dashDuration).From(0f).SetEase(Ease.Linear);
        }

        public void DashDisable()
        {
            frontDashImage.transform.DOLocalMoveX(0f, _duration).SetEase(Ease.Linear);
            backDashImage.transform.DOLocalMoveX(0f, _duration).SetEase(Ease.Linear);
            dashObject.transform.DOScale(0f, _duration).SetEase(Ease.Linear);
        }
        

        #endregion
        private void OnEscStarted(InputAction.CallbackContext context)
        {
            Debug.Log(PopupManager.PopupList.Count +"개 활성화 되어있음 ");
            //base.OnEscStarted(context);\
            if (PopupManager.PopupList.Count >= 2)
            {
                Debug.Log(PopupManager.PopupList[1].name);
                PopupManager.PopupList[1].transform.GetChild(0).GetComponent<ViewBase>().Hide();
                Show();
            }
            else
            {
                Hide();
                InfoActive();    
            }
            
        }
        private void OnSwordMenuStarted(InputAction.CallbackContext context)
        {
            Hide();
            SwordMenuActive();
        }
        
        #region ::: Info(ESC) :::
        
        private void InfoActive()
        {
            _canInteract = true;
            FlowManager.Instance.AddSubPopup(PopupStyle.Info);
        }
        
        #endregion

        #region ::: SwordMenu ( C ) :::

        private void SwordMenuActive()
        {
            _canInteract = true;
            FlowManager.AddSubPopup(PopupStyle.SwordMenu);
        }
        
        private void SwordMenuDisActive()
        {
            
        }

        #endregion
        
        
        #region Inherit Methods
        
        private float _duration = 0.5f;
        //private float topDest = 128f;
        //private float rightDest = 128f;
        public override void Show()
        {
            base.Show(); 
            Debug.Log("기존거 보이기");
        }
        public override void Hide()
        {
            base.Hide();
            //_uiAnimator.SetTrigger("Hide");
            // 미니맵 - 사이즈 사라지기
            // 퀘스트 - 사이즈 사라지기
            
            // 상단UI - 위로 올라가기 
            // 오른쪽UI - 연해지면서 오른쪽 이동
            // 체력UI - 아래로 사라지면서 연해짐
            /*
            miniMapObject.transform.DOScale(0f, _duration).SetEase(Ease.Linear);
            questObject.transform.DOScale(0f, _duration).SetEase(Ease.Linear);
            
            topRange.GetComponent<RectTransform>().DOMoveY( 1080f + 64f, _duration).SetEase(Ease.Linear);
            bottomRange.GetComponent<RectTransform>().DOMoveY(-64f, _duration).SetEase(Ease.Linear);
            rightRange.GetComponent<RectTransform>().DOMoveX(1920f +64f, _duration).SetEase(Ease.Linear);*/
        }
        
        #endregion
    }
}