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
    [SerializeField] private CinemachineVirtualCamera _playerCamera; // �������� ���� ����
    [SerializeField] private CinemachineVirtualCamera _pinCamera;

    
    // About Pin Object
    [Header("Transform")]
    [Space(2)]
    [SerializeField] private Transform _characterTF;
    [SerializeField] private Transform _PinObjectTarget;
    // ȸ����ų ī�޶�
    private Camera noramlCam;

    // Lookat
    public PinType _pinType;
    
    private MonsterUnit _currentPinMonster; // BattleManager�� �ν��Ͻ����� �޾Ƽ� ���
    
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
        // ĳ���� �ٶ󺸴µ��� ���������
        if (_pinType == PinType.Monster)
            return;
        
        _pinCamera.transform.position = _playerCamera.transform.position;
        _pinCamera.transform.rotation = _playerCamera.transform.rotation;
    }

    public Vector3 GetDir;
    private void Pin()
    {
        // ������ ���õ� ����
        if (_pinType == PinType.Player)
            return;
        if(_currentPinMonster == null)
            return;
        
        // PinObjectTarget�� ��ġ�� ���⺤�͸� �̿��Ͽ� ����
        float mulValue = 2.5f;
        GetDir = (_characterTF.position - _currentPinMonster.transform.position).normalized;
        Vector3 vec_Pincamera = (_characterTF.position - _currentPinMonster.transform.position).normalized  * mulValue;

        float xValue = (_characterTF.position + vec_Pincamera).x; 
        float zValue = (_characterTF.position + vec_Pincamera).z;
        _PinObjectTarget.transform.position =
            new Vector3(
               xValue,
                2.2f, // ĳ���� Ű ���̸�ŭ?
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
        // ���� ������� ���Ͱ� ������ ���½�Ű�� �ٸ��ַ� ��ȯ
        BattleManager.Instance.currentPinMonster = BattleManager.Instance._activeMonsterList[0];
        _currentPinMonster = BattleManager.Instance._activeMonsterList[0];
        _currentPinMonster.PinActive(true);
        CamPriorityChange(PinType.Monster);
        DebugManager.instance.Log("Ÿ�� ���� ��� �̸� : " + BattleManager.Instance._activeMonsterList[0].name);
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
        // Priority �� �����ŷ� ������
        switch (type)
        {
            case PinType.Player :
                //_playerCamera.gameObject.SetActive(true);
                //_pinCamera.gameObject.SetActive(false);
                _playerCamera.Priority = 1; // Ȱ��ȭ
                _pinCamera.Priority = 0;
                break;
            case PinType.Monster :
                //_playerCamera.gameObject.SetActive(false);
                //_pinCamera.gameObject.SetActive(true);
                _playerCamera.Priority = 0;
                _pinCamera.Priority = 1; // Ȱ��ȭ
                    
                _pinCamera.m_LookAt = BattleManager.Instance._activeMonsterList[0].GetPinObject();
                break;
        }
    }

    /// <summary>
    /// ESC ������ ���� ī�޶� ��������
    /// </summary>
    public void ToEsc()
    {
        //_animator.SetTrigger("ToEsc");
    }

    /// <summary>
    /// �ٽ� �÷��̾� ī�޶��
    /// </summary>
    public void ToPlayer()
    {
        //_animator.SetTrigger("ToPlayer");
    }
}