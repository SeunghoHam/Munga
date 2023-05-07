using AmplifyShaderEditor;
using Cinemachine;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class CameraZoom : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;

        [SerializeField] private Transform _chestPoint;
        [SerializeField] private Transform _headPoint;
        private Transform _targetPoint;
        
        [Header("거리 기본값")]
        [SerializeField] [Range(1f, 3f)] private float defaultDistance = 2.8f;
        private float minimumDistance = 0.8f;
         private float maximumDistance = 3f;

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

        
        private Transform CurTarget
        {
            //get;
            set
            {
                Debug.Log("Target변경");
                if(value == _headPoint) // HeadPoint
                {
                    _virtualCamera.m_Follow = _headPoint;
                    _virtualCamera.m_LookAt = _headPoint;
                }
                else // ChestPoint
                {
                    _virtualCamera.m_Follow = _chestPoint;
                    _virtualCamera.m_LookAt = _chestPoint;
                }
            }
        }
        private void Zoom()
        {
            float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;

            currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumDistance, maximumDistance);

            /*
            if (currentTargetDistance <= minimumDistance + 0.2f)
            {
                CurTarget = _headPoint;
            }
            else
            {
                CurTarget = _chestPoint;
            }
*/
            float currentDistance = framingTransposer.m_CameraDistance;

            if (currentDistance == currentTargetDistance)
            {
                return;
            }

            float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

            framingTransposer.m_CameraDistance = lerpedZoomValue;
            
        }
    }
}