using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common.DI;
using Assets.Scripts.Util;
using Assets.Scripts.MangeObject;
using Assets.Scripts.Manager;
using Assets.Scripts.UI;
using Cinemachine;

namespace HAM
{
    public class TestScene : MonoBehaviour
    {
        void Start()
        {
            DependuncyInjection.Inject(this);
            ManageObjectFacade.Initialize();
            FlowManager.Instance.AddSubPopup(PopupStyle.Basic);
            
            CameraDisalbe();
        }

        // 다른카메라 있다면 비활성화
        private void CameraDisalbe()
        {
            Camera cam = GameObject.FindObjectOfType<Camera>();

            if (cam == null)
                return;

            //Debug.Log("카메라이름 : " + cam.name);
            if (cam.GetComponent<CinemachineBrain>())
            {
                //Debug.Log("하지만 브레인");
                return;
            }
            Debug.Log("기존 카메라 비활성화");
            cam.gameObject.SetActive(false);
            
        }
    }
    
}
