using System;
using UnityEngine;
using Cinemachine;
using Assets.Scripts.Manager;

public enum PinType
{
    Player,
    Monster,
}

//[RequireComponent(typeof(Animator))]
public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private CinemachineBrain _brain;

    [Header("Cameras")] [Space(3)] 
    [SerializeField] private CinemachineVirtualCamera _playerCamera; // 고정하지 않은 상태
    [SerializeField] private CinemachineVirtualCamera _pinCamera;

    
    // About Pin Object
    [Header("Transform")]
    [Space(2)]
    [SerializeField] private Transform _characterTF;
    [SerializeField] private Transform _PinObjectTarget;
    // 회전시킬 카메라
    private Camera noramlCam;

    // Lookat
    public PinType _pinType;
    
    private MonsterUnit _currentPinMonster; // BattleManager의 인스턴스에서 받아서 사용
    
    private void Awake()
    {
        instance = this;

        _brain = this.GetComponent<CinemachineBrain>();

        _pinType = PinType.Player;
        CamPriorityChange(PinType.Player);

        _pinCamera.Priority = 0;
        _playerCamera.Priority = 1;
    }

    private void Update()
    {
        Pin();
        Pos();
    }

    private void Pos()
    {
        // 캐릭터 바라보는동안 따라오도록
        if (_pinType == PinType.Monster)
            return;
        
        _pinCamera.transform.position = _playerCamera.transform.position;
        _pinCamera.transform.rotation = _playerCamera.transform.rotation;
    }

    public Vector3 GetDir;
    private void Pin()
    {
        // 고정과 관련된 정보
        if (_pinType == PinType.Player)
            return;
        if(_currentPinMonster == null)
            return;
        
        // PinObjectTarget의 위치를 방향벡터를 이용하여 설정
        float mulValue = 2.5f;
        GetDir = (_characterTF.position - _currentPinMonster.transform.position).normalized;
        Vector3 vec_Pincamera = (_characterTF.position - _currentPinMonster.transform.position).normalized  * mulValue;

        float xValue = (_characterTF.position + vec_Pincamera).x; 
        float zValue = (_characterTF.position + vec_Pincamera).z;
        _PinObjectTarget.transform.position =
            new Vector3(
               xValue,
                2.2f, // 캐릭터 키 높이만큼?
                zValue);
    }
    
    public void PinTargetSetting()
    {
        if (_pinType == PinType.Player)
        {
            if (BattleManager.Instance._activeMonsterList.Count == 0)
            {
                DebugManager.instance.Log("{ActiveMonster = null}", DebugManager.TextColor.Yellow);
                return;
            }
            DebugManager.instance.Log("{Player => Monster}", DebugManager.TextColor.Yellow);
            MonsterPin();
        }
        else if (_pinType == PinType.Monster)
        {
            DebugManager.instance.Log("{Monster => Player}", DebugManager.TextColor.Yellow);
            PlayerPin();
        }
    }

    private void MonsterPin()
    {
        // 현재 대상중인 몬스터가 죽으면 리셋시키고 다른애로 반환
        BattleManager.Instance.currentPinMonster = BattleManager.Instance._activeMonsterList[0];
        _currentPinMonster = BattleManager.Instance._activeMonsterList[0];
        _currentPinMonster.PinActive(true);
        CamPriorityChange(PinType.Monster);
        DebugManager.instance.Log("타겟 고정 대상 이름 : " + BattleManager.Instance._activeMonsterList[0].name);
        _pinType = PinType.Monster;
    }

    private void PlayerPin()
    {
        if (_currentPinMonster != null)
        {
            _currentPinMonster.PinActive(false);
            _currentPinMonster = null;
        }
        _pinType = PinType.Player;
        CamPriorityChange(PinType.Player);
    }

    private void CamPriorityChange(PinType type)
    {
        // Priority 가 높은거로 보여짐
        switch (type)
        {
            case PinType.Player :
                //_playerCamera.gameObject.SetActive(true);
                //_pinCamera.gameObject.SetActive(false);
                _playerCamera.Priority = 1; // 활성화
                _pinCamera.Priority = 0;
                break;
            case PinType.Monster :
                //_playerCamera.gameObject.SetActive(false);
                //_pinCamera.gameObject.SetActive(true);
                _playerCamera.Priority = 0;
                _pinCamera.Priority = 1; // 활성화
                    
                _pinCamera.m_LookAt = BattleManager.Instance._activeMonsterList[0].GetPinObject();
                break;
        }
    }

    /// <summary>
    /// ESC 눌렀을 때의 카메라 설정으로
    /// </summary>
    public void ToEsc()
    {
        //_animator.SetTrigger("ToEsc");
    }

    /// <summary>
    /// 다시 플레이어 카메라로
    /// </summary>
    public void ToPlayer()
    {
        //_animator.SetTrigger("ToPlayer");
    }
}