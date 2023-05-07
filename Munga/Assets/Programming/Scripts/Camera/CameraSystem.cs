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
    /// ESC ������ ���� ī�޶� ��������
    /// </summary>
    public void ToEsc()
    {
        _animator.SetTrigger("ToEsc");   
    }

    /// <summary>
    /// �ٽ� �÷��̾� ī�޶��
    /// </summary>
    public void ToPlayer()
    {
        _animator.SetTrigger("ToPlayer");
    }
}
