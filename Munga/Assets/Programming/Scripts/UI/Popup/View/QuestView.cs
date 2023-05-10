using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI.Popup.Base;


namespace Assets.Scripts.UI.Popup.PopupView
{
    public class QuestView : ViewBase
    {
        
        
        
        private void OnEnable()
        {
            Show();
        }
        public override void Show()
        {
            base.Show();
        }
        public override void Hide()
        {
            //InputManager._cameraCursor.DisableCursor();
            //PopupManager.PopupList[0].GetComponent<UIPopupBasic>()._basicView.Show();
            base.Hide();
        }
    }
}