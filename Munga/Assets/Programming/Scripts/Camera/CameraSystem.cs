using System;
using UnityEngine;
using Cinemachine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _brain;

     [Header("Cameras")]
     [Space(3)]
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _escCamera;

     private Animator _animator;
    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// ESC 눌렀을 때의 카메라 설정으로
    /// </summary>
    public void ToEsc()
    {
        _animator.SetTrigger("ToEsc");   
    }

    /// <summary>
    /// 다시 플레이어 카메라로
    /// </summary>
    public void ToPlayer()
    {
        _animator.SetTrigger("ToPlayer");
    }
}
