using UnityEngine;
using Assets.Scripts.Common.DI;
using Assets.Scripts.Manager;
using Unity.VisualScripting;

public class CharacterAddedManager : MonoBehaviour
{
    [DependuncyInjection(typeof(ResourcesManager))]
    ResourcesManager ResourcesManager;

    private const string filePath = "Prefabs/System/";
    private const string _cameraSystem = "#CameraSystem";
    private const string _manager = "#Manager";
    
    [Header("캐릭터 프리팹")]
    [SerializeField] private GameObject characterPrefab;
    
    //private Transform _createPos;
    private void Awake()
    {
        //_createPos = this.gameObject.transform.GetChild(0);
        
        //GameObject character =  Instantiate(ResourcesManager.Load(filePath + _character));

        if (characterPrefab != null)
        {
            GameObject character = Instantiate(characterPrefab, this.transform);
            character.transform.position = this.transform.position;
        }
        else
        {
          
        }
        var cam = Instantiate(ResourcesManager.Load(filePath + _cameraSystem));
        var manager = Instantiate(ResourcesManager.Load(filePath + _manager));
        
        cam.transform.parent = transform;
        manager.transform.parent = transform;

        //Character.Instance.cameraSystem = cam.GetComponent<CameraSystem>();
    }
}
