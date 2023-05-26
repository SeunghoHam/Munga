using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class CameraZoom : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;
        
        private Transform _targetPoint;
        
        [Header("거리 기본값")]
        [SerializeField] [Range(2f, 3.5f)] private float defaultDistance = 2.8f;
        private float minimumDistance = 2.5f;
         private float maximumDistance = 3.2f;

        //[SerializeField] [Range(0f, 20f)]
        private float smoothing = 4f;
        
        [Header("민감도")]
        [SerializeField] [Range(1f, 10f)] private float zoomSensitivity = 4f;

        private CinemachineFramingTransposer framingTransposer;
        private CinemachineInputProvider inputProvider;

        private float currentTargetDistance;

        private void Awake()
        {
            framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            inputProvider = GetComponent<CinemachineInputProvider>();

            currentTargetDistance = defaultDistance;

            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            Zoom();
        }
        
        private void Zoom()
        {
            float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;

            currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumDistance, maximumDistance);

            float currentDistance = framingTransposer.m_CameraDistance;

            if (currentDistance == currentTargetDistance)
            {
                return;
            }

            float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

            framingTransposer.m_CameraDistance = lerpedZoomValue;
            
        }

        public void CameraAction()
        {
            
        }

        private IEnumerator BasicAttackAction()
        {
            
            yield break;
        }
    }
}