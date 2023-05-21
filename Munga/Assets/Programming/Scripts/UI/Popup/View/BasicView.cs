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
        public QuestManager QuestManager { get; set; }
        #region ::: ObjectRange:::
        
        [Header("Range")]
        [SerializeField] private GameObject topRange;
        [SerializeField] private GameObject bottomRange;
        [SerializeField] private GameObject rightRange;
        

        [SerializeField] private GameObject dashObject; 
        #endregion

        [SerializeField] private WeaponManager _weaponManager;

        
      
        private MinimapManager minimap;
        private WeaponManager m_weapon;
        
        
        [Space(10)]
        [Header("Button")]
        [SerializeField] private Button _swordButotn;
        [SerializeField] private Button _invenButton;

        //private Animator _uiAnimator;
        
        
        // QuestObject
        public QuestBasicPart questBasicPart;
        
        #region ::: bool Data :::
        private bool _isActive = false; // 활성화 여부
        private bool _canInteract = true;
        #endregion
        
        private void Start()
        {
            Init();
            _uiAnimator = this.GetComponent<Animator>();

            InputManager._input.InputActions.UI.ESC.started += OnEscStarted;
            InputManager._input.InputActions.UI.Character.started += OnSwordMenuStarted;
            InputManager._input.InputActions.UI.Quest.started += OnQuestStarted;
            InputManager._input.InputActions.UI.KeyNumber1.started += OnKeyNumberStarted1;
            InputManager._input.InputActions.UI.KeyNumber2.started += OnKeyNumberStarted2;
            InputManager._input.InputActions.UI.KeyNumber3.started += OnKeyNumberStarted3;
            
            
            _weaponManager.FirstSetting();
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

        private void OnQuestStarted(InputAction.CallbackContext context)
        {
            if (PopupManager.PopupList.Count >= 2)
                return;
            Hide();
            FlowManager.AddSubPopup(PopupStyle.Quest);
        }
        private void OnEscStarted(InputAction.CallbackContext context)
        {
            if (PopupManager.PopupList.Count >= 2)
            {
                Debug.Log(PopupManager.PopupList[1].name);
                PopupManager.PopupList[1].transform.GetChild(0).GetComponent<ViewBase>().Hide();
                Show();
                // 카메라 캐릭터 뒤로 움직여지도록 해야함
                //InputManager._cameraSystem.ToPlayer();
            }
            else
            {
                InputManager._cameraCursor.EnableCursor();
                Hide();
                InfoActive();    
                //InputManager._cameraSystem.ToEsc();
                
                // 움직임도 막아야하는데 어캐함? 몰룽
            }
        }
        private void OnSwordMenuStarted(InputAction.CallbackContext context)
        {
            if (PopupManager.PopupList.Count >= 2)
                return;
            Hide();
            SwordMenuActive();
        }

        #region ::: KeyNumber :::
        private void OnKeyNumberStarted1(InputAction.CallbackContext context)
        {
            if (PopupManager.PopupList.Count >= 2)
                return;
            _weaponManager.SelectWeapon(1);
        }
        private void OnKeyNumberStarted2(InputAction.CallbackContext context)
        {
            if (PopupManager.PopupList.Count >= 2)
                return;
            _weaponManager.SelectWeapon(2);
        }
        private void OnKeyNumberStarted3(InputAction.CallbackContext context)
        {
            if (PopupManager.PopupList.Count >= 2)
                return;
            _weaponManager.SelectWeapon(3);

        }        

        #endregion
        

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

        private void DisableCursor()
        {
            InputManager._cameraCursor.DisableCursor();
        }
        private void EnableCursor()
        {
            InputManager._cameraCursor.EnableCursor();
        }
        
        #region Inherit Methods
        
        //private float topDest = 128f;
        //private float rightDest = 128f;
        public override void Show()
        {
            base.Show();
            Debug.Log("기존거 보이기");
            DisableCursor();
        }
        public override void Hide()
        {
            base.Hide();
            EnableCursor();
        }
        
        #endregion
    }
}