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
        }
    }
    
}
