using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Manager;
using Assets.Scripts.Common.DI;
using Assets.Scripts.UI.Popup.PopupView;

namespace Assets.Scripts.UI.Popup.Base
{
    public class UIPopupQuest : PopupBase
    {
        [DependuncyInjection(typeof(FlowManager))]
        private FlowManager _flowManager;

        [DependuncyInjection(typeof(ResourcesManager))]
        private ResourcesManager _resourcesManager;

        [DependuncyInjection(typeof(PopupManager))]
        private PopupManager _popupManager;
        
        [DependuncyInjection(typeof(QuestManager))]
        private QuestManager _questManager;
        
        public QuestView _QuestView;
        
        #region ::: bool Data :::

        #endregion
        
        public override void Initialize()
        {
            base.Initialize();
            DependuncyInjection.Inject(this);

            _QuestView.FlowManager = _flowManager;
            _QuestView.ResourcesManager = _resourcesManager;
            _QuestView.PopupManager = _popupManager;
            _QuestView.QuestManager = _questManager;
        }
        public override void Show(params object[] data)
        {
            base.Show(data);
            //_infoView.InfoEnable();
        }
        public override void Hide()
        {
            base.Hide();
        }
    }
}