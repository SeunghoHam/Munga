using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common.DI;
using Assets.Scripts.Util;
using Assets.Scripts.MangeObject;
using Assets.Scripts.Manager;
using Assets.Scripts.UI;

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
            GameObject camObject = GameObject.Find("Main Camera");
            if (camObject == null)
                return;
            camObject.SetActive(false);
        }
    }
    
}
