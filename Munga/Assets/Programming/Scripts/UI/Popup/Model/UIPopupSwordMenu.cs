using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI.Popup.PopupView;
using Assets.Scripts.Common.DI;
using Assets.Scripts.Manager;

namespace Assets.Scripts.UI.Popup.Base
{
    public class UIPopupSwordMenu : PopupBase
    {
        [DependuncyInjection(typeof(FlowManager))]
        private FlowManager _flowManager;

        [DependuncyInjection(typeof(ResourcesManager))]
        private ResourcesManager _resourcesManager;

        [DependuncyInjection(typeof(PopupManager))]
        private PopupManager _popupManager;

        public SwordMenuView _swordMenuView;

        public override void Initialize()
        {
            base.Initialize();
            DependuncyInjection.Inject(this);
            _swordMenuView.FlowManager = _flowManager;
            _swordMenuView.ResourcesManager = _resourcesManager;
            _swordMenuView.PopupManager = _popupManager;
            //_swordMenuView.gameObject.SetActive(false);
        }

        public override void Show(params object[] data)
        {
            base.Show(data);
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
}