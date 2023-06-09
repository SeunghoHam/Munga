using Assets.Scripts.Manager;
using Assets.Scripts.UI;
using System;
using System.Collections;
using UniRx;
using System.Threading;

namespace Assets.Scripts.MangeObject
{
    public class ManageObjectFacade
    {
        private static bool _isInitialize = false;
        
        public static FlowManager FlowManager
        { get { return FlowManager.Instance; } }

        public static PopupManager PopupManager
        { get { return PopupManager.Instance; } }

        public static  DataManager DataManager
        {
            get { return DataManager.Instance; }
        }
        public static InputManager InputManager
        {
            get { return InputManager.Instance; }
        }
        public static QuestManager QuestManager
        {
            get { return QuestManager.Instance; }
        }

        public static BattleManager BattleManager
        {
            get { return Manager.BattleManager.Instance; }
        }
        
        public static void Initialize()
        {
            if (_isInitialize)
                return;
            
            _isInitialize = true;
            FlowManager.Initialize();
            PopupManager.Initialize();
            DataManager.Initialize();
            QuestManager.Initialize();
            BattleManager.Initialize();
            //InputManager.Initialize();
        }
        public static IObservable<Unit> UnInitialize()
        {
            return Observable.FromCoroutine<Unit>(UnInitialize);
        }
        public static IEnumerator UnInitialize(IObserver<Unit> observer, CancellationToken token)
        {
            if(_isInitialize == false)
            {
                DebugManager.instance.Log("UnInitialize");
                observer.OnNext(Unit.Default);
                observer.OnCompleted();
            }
            else
            {
                PopupManager.UnInitialize();
                FlowManager.UnInitialize();
                DataManager.UnInitialize();
                QuestManager.UnInitialize();
                BattleManager.UnInitialize();
                //  InputManager.UnInitialize();
                
                yield return FrameCountType.FixedUpdate.GetYieldInstruction();
                _isInitialize = false;
                observer.OnNext(Unit.Default);
                observer.OnCompleted();
            }
        }

    }
}