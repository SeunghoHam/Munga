using Assets.Scripts.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class ResourcesManager : UnitySingleton<ResourcesManager>
    {
        private Dictionary<string, Sprite> _imageCaches = new Dictionary<string, Sprite>();
        private Dictionary<string, GameObject> _objectCaches = new Dictionary<string, GameObject>();
        public override void Initialize()
        {
            base.Initialize();
        }

        #region ###?????? ????
        public static GameObject LoadAndInit(string path, Transform parent)
        {
            var pathLoad = Load(path);
            if(pathLoad == null)
            {
                DebugManager.instance.LogError("LoadAndInit Error : " + path);
                return null;
            }
            var item = Instantiate(pathLoad, parent);
            var transform = item.GetComponent<Transform>();
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            return item;
        }

        public static GameObject Load(string path)
        {
            return Resources.Load<GameObject>(path);
        }
        #endregion

        #region ###????? ????
        public static Sprite GetImages(string id)
        {
            if(!Instance._imageCaches.ContainsKey(id))
            {
                DebugManager.instance.LogError("GetImages Erorr : " + id);
                return null;
            }
            return Instance._imageCaches[id];
        }

        public static Sprite GetPathImage(string subPath, string name)
        {
            if(Instance._imageCaches.ContainsKey(name) == false)
            {
                var path = string.Format("{0}{1}", subPath, name);
                var sprite = Resources.Load<Sprite>(path);
                Instance._imageCaches.Add(name, sprite); // ?????? ??????? ??????? ???? ???
            }
            return Instance._imageCaches[name];
        }
        #endregion
    }
}