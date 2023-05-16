using Assets.Scripts.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Threading;
using System.Linq;
using Assets.Scripts.Common.DI;
using Assets.Scripts.Util;

namespace Assets.Scripts.UI
{
    public class PopupManager : UnitySingleton<PopupManager>
    {
        // = Constant
        private const string PopupPrefix = "Prefabs/Popup/UIPopup";

        // = Field
        public List<PopupBase> PopupList = new List<PopupBase>();

        public override void Initialize()
        {
            base.Initialize();
            DependuncyInjection.Inject(this);
        }
        public override void UnInitialize()
        {
            //base.UnInitialize();
            foreach (var popup in PopupList)
            {
                Destroy(popup.gameObject);
            }
            PopupList.Clear();
        }

        // = Construct
        public IObservable<T> Get<T>(PopupStyle style) where T : PopupBase
        {
            return Observable.FromCoroutine<T>((observer, token) => Get(observer, token, style));
        }
        private IEnumerator Get<T>(IObserver<T> observer, CancellationToken cancelltationToken, PopupStyle style) where T : PopupBase
        {
            PopupBase popupBase = null;
            yield return Observable.FromCoroutineValue<T>(() =>
            Get<T>(cancelltationToken, style))
                .Where(popup => popup != null)
                .StartAsCoroutine(popup => popupBase = popup);

            observer.OnNext(popupBase.GetComponent<T>());
            observer.OnCompleted();
        }
        private IEnumerator Get<T>(CancellationToken token, PopupStyle style) where T : PopupBase
        {
            var popupName = GetPopupName(style);
            var popupBase = PopupList.Find(child => child.PopupStyle.Equals(style));

            if (popupBase == null)
            {
                var resource = Resources.LoadAsync<GameObject>(popupName);
                while (!resource.isDone)
                {
                    if (token.IsCancellationRequested)
                        yield break;
                    yield return FrameCountType.FixedUpdate.GetYieldInstruction();
                }
                // = resources?ех? ??
                popupBase = Instantiate((GameObject)resource.asset).GetComponent<PopupBase>();

                // ?????? ???????? ???? ????? ????? canvas ??????
                var popupCanvas = popupBase.GetComponentInChildren<Canvas>();
                if (popupBase as PopupSub)
                {
                    // ???? ??? X 
                    popupCanvas.worldCamera = null;
                    popupCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                }
                else
                {
                    // ???? ??? O
                    // ???? ????? ???? ?????? ??? ????? ?????

                    //popupCanvas.worldCamera = Camera.main; 
                    //popupCanvas.renderMode = RenderMode.ScreenSpaceCamera;
                    popupCanvas.worldCamera = null;
                    popupCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                }
                popupBase.transform.SetParent(transform);
                popupBase.transform.localScale = Vector3.one;
                popupBase.transform.localPosition = Vector3.zero;
                popupBase.gameObject.SetActive(false);
                PopupList.Add(popupBase);
            }
            yield return popupBase;
        }

        // Show
        public IObservable<T> Show<T>(PopupStyle style, params object[] data) where T : PopupBase
        {
            return Observable.FromCoroutine<T>((observer, token) => Show(observer, token, style, data)); // ????? 4???? Show ???????
        }
        private IEnumerator Show<T>(IObserver<T> observer, CancellationToken token, PopupStyle style, object[] data) where T : PopupBase
        {
            //yield return Get<T>(token, style);
            /* 
             yield return Observable.FromCoroutineValue<T>(() =>
              Get<T>(token, style)).Where(popup => popup != null)
                  .StartAsCoroutine(popup => popupBase = popup);*/
            yield return Get<T>(token, style);
            PopupBase popupBase = PopupList[PopupList.Count - 1];
            popupBase.SetParent(transform);
            popupBase.Show(data);
            popupBase.gameObject.SetActive(true);

            observer.OnNext(popupBase.GetComponent<T>());
            observer.OnCompleted();
        }

        public void Hide(PopupStyle style)
        {
            var popup = PopupList.Find(child => child.PopupStyle.Equals(style));
            if (popup != null && popup.gameObject.activeSelf)
                popup.Hide();
        }
        // = Method
        private string GetPopupName(PopupStyle style)
        {
            // Prefabs/Popup/UIPopup  + style
            return string.Format("{0}{1}", PopupPrefix, style);
        }
        public List<PopupBase> GetShowingPopupList()
        {
            return PopupList.Where(@base => @base.IsActive).ToList();
        }
    }

}
