using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CameraSystem cameraSystem;
    
    public static Character Instance;
    
    private int _maxHP = 10;
    private int _curHP;

    #region ::: PlayerData :::
    
    public string plyaerName;
    public int playerRunes;
    public float playerExp;
    
    #endregion
    // Can Interact?
    #region ::: Interact :::

    private bool _canInteract = false;
    public bool CanInteract
    {
        get { return _canInteract; }
        set 
        {
            if(_canInteract != value)
                _canInteract = value;
        }
    }
    private bool _isInteract = false;
    public bool IsInteract
    {
        get { return _isInteract; }
        set
        {
            if(_isInteract != value)
            {
                //Animator.Anim_Idle();
                _isInteract = value;
            }
        }
    }
    #endregion
    
    public int MaxHP
    {
        get { return _maxHP; }
    }

    public int CurHP
    {
        get { return _curHP; }
        set
        {
            _curHP = value;
        }
    }

    // CharacterState : Normal / Sword
    public enum WeaponState
    { 
        Normal,
        Sword,
    }
    public WeaponState weaponState = WeaponState.Normal;

    [Space(10)]
    public Transform TargetPoint;

    private int _attackDamage = 1;
    public int AttackDamage
    { get{ return _attackDamage; } }
    

    public CameraSystem GetCameraSystem
    {
        get { return cameraSystem; }
        set 
        { if (cameraSystem == null)
                cameraSystem = value;
        }
    }


    private void Awake()
    {
        Instance = this;
        _curHP = _maxHP;
    }
}
