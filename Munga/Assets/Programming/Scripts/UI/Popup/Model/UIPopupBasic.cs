using Assets.Scripts.Manager;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common.DI;
using UnityEngine;
using Assets.Scripts.UI.Popup.PopupView;

namespace Assets.Scripts.UI.Popup.Base
{
    public class UIPopupBasic : PopupBase
    {
        [DependuncyInjection(typeof(FlowManager))]
        private FlowManager _flowManager;

        [DependuncyInjection(typeof(ResourcesManager))]
        private ResourcesManager _resourcesManager;

        [DependuncyInjection(typeof(PopupManager))]
        private PopupManager _popupManager;
        
        [DependuncyInjection(typeof(InputManager))]
        private InputManager _inputManager;
        
        [DependuncyInjection(typeof(DataManager))]
        private DataManager _dataManager;
        
        [DependuncyInjection(typeof(QuestManager))]
        private QuestManager _questManager;
        
        public BasicView _basicView;

        public override void Initialize()
        {
            base.Initialize();
            DependuncyInjection.Inject(this);

            _basicView.FlowManager = _flowManager;
            _basicView.ResourcesManager = _resourcesManager;
            _basicView.PopupManager = _popupManager;
            _basicView.InputManager  = _inputManager;
            _basicView.DataManager = _dataManager;
            _basicView.QuestManager = _questManager;
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