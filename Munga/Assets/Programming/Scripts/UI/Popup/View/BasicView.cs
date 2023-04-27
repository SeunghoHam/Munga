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

namespace Assets.Scripts.UI.Popup.PopupView
{
    public class BasicView : MonoBehaviour
    {
        public FlowManager FlowManager { get; set; }
        public ResourcesManager ResourcesManager { get; set; }
        public PopupManager PopupManager { get; set; }
        
        public enum CurrentViewType
        {
            None,
            Status,
            System,
        }

        public CurrentViewType _currentViewType;
        
        // 팝업 활성화여부
        private bool _interactActive = false;
        private bool _pauseActive = false;

        private void Start()
        {
            Init();
            AddEvent();
            DependuncyInjection.Inject(this);
        }
        private void Init()
        {
            
        }
        private void AddEvent()
        {
            /*
            this.ObserveEveryValueChanged(_ => Character.Instance.CurHP)
                //.Where(_ => )
                .Subscribe()
                .AddTo(gameObject);
            */
        }
        
    }
}