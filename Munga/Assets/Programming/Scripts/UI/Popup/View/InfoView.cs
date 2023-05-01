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
    public class InfoView : MonoBehaviour
    {
        public FlowManager FlowManager { get; set; }
        public ResourcesManager ResourcesManager { get; set; }
        public PopupManager PopupManager { get; set; }

        [SerializeField] private Image sideObject; // 인포 좌측 선같은거
        
        [Space(10)]
        [SerializeField] private GameObject userInfoObject; // 유저 정보
        
        private void OnEnable()
        {
            DependuncyInjection.Inject(this);
            //Init();
        }

        private void Init()
        {
            sideObject.gameObject.SetActive(false);
            userInfoObject.gameObject.SetActive(false);
        }

        public void InfoEnable()
        {
            sideObject.gameObject.SetActive(true);
            /*
            sideObject.DOFade(1f, 0.2f).From(0f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    //_canInteract = true;
                    
                });
            
            userInfoObject.SetActive(true);
            userInfoObject.GetComponent<Image>().rectTransform.DOMoveX(128, 0.1f)
                .From(-720f)
                .SetEase(Ease.Linear);*/
        }
        

        public void InfoDisable()
        {
            /*
            sideObject.DOFade(0f, 0.2f).SetEase(Ease.Linear);
            //userInfoObject.GetComponent<Image>().DOFade(0f, 0.2f).SetEase(Ease.Linear);
            userInfoObject.GetComponent<Image>().rectTransform.DOMoveX(-720f, 0.1f)
                .SetEase(Ease.Linear);
            /*
            .OnComplete(() =>
            {
                //_canInteract = true;
                //sideObject.gameObject.SetActive(false);
            });*/
        }
    }
}