using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Manager;
using Assets.Scripts.Common.DI;
using Assets.Scripts.UI.Popup.PopupView;

namespace Assets.Scripts.UI.Popup.Base
{
    public class UIPopupInfo : PopupBase
    {
        [DependuncyInjection(typeof(FlowManager))]
        private FlowManager _flowManager;

        [DependuncyInjection(typeof(ResourcesManager))]
        private ResourcesManager _resourcesManager;

        [DependuncyInjection(typeof(PopupManager))]
        private PopupManager _popupManager;
        
        public InfoView _infoView;


        #region ::: bool Data :::

        #endregion
        
        public override void Initialize()
        {
            base.Initialize();
            DependuncyInjection.Inject(this);

            _infoView.FlowManager = _flowManager;
            _infoView.ResourcesManager = _resourcesManager;
            _infoView.PopupManager = _popupManager;
        }
        public override void Show(params object[] data)
        {
            base.Show(data);
            _infoView.InfoEnable();
        }
        public override void Hide()
        {
            _infoView.InfoDisable();           
            //Invoke("base.Hide()", 0.3f);
            base.Hide();
        }

        public void ViewHide()
        {
            _infoView.InfoDisable();
            Invoke("Hide", 0.3f); 
        }
        
    }
}