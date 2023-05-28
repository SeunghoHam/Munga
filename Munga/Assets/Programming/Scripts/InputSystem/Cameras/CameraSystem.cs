using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using Cinemachine;
using UnityEditor;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class CameraSystem : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;
        private Transform _targetPoint;
        
        [Header("거리 기본값")]
        [SerializeField] [Range(2f, 3.5f)] private float defaultDistance = 2.8f;
        private float minimumDistance = 2.0f;
        private float maximumDistance = 3.5f;

        //[SerializeField] [Range(0f, 20f)]
        private float smoothing = 4f;
        
        [Header("민감도")]
        [SerializeField] [Range(1f, 10f)] private float zoomSensitivity = 4f;

        private CinemachineFramingTransposer framingTransposer;
        private CinemachineInputProvider inputProvider;

        private float currentTargetDistance;
        private float zoomValue;

        private void Awake()
        {
            framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            inputProvider = GetComponent<CinemachineInputProvider>();

            currentTargetDistance = defaultDistance;

            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            BattleManager.Instance.GetCameraSystem(this);
        }

        private void Update()
        {
            Zoom();
            
        }
        
        private void Zoom()
        {
            zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;

            currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumDistance, maximumDistance);

            float currentDistance = framingTransposer.m_CameraDistance;

            if (currentDistance == currentTargetDistance)
            {
                return;
            }

            float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

            framingTransposer.m_CameraDistance = lerpedZoomValue;
            
        }
        
        public void CameraAction(CameraActionType type)
        {
            switch (type)
            {
                case CameraActionType.Near:
                    Near();
                    break;
                
                case CameraActionType.Far:
                    Far();
                    break;
                
                case CameraActionType.Original:
                    Original();
                    break;
            }
        }

        private void Near()
        {
            // 최소값 제한 필요함
            currentTargetDistance -= 0.2f;
        }

        private void Far()
        {
            currentTargetDistance += 0.2f;
        }

        private void Original()
        {
            currentTargetDistance = defaultDistance;
        }
    }
    
    
    
}