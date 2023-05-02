using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public abstract class VirtualCameraReactToBlendOut : MonoBehaviour
{
    public abstract void _OnVirtualCameraAnimateIn_Started( CinemachineVirtualCameraBase vcam );
    public abstract void _OnVirtualCameraAnimateIn_Finished( CinemachineVirtualCameraBase vcam );
    public abstract void _OnVirtualCameraAnimateOut_Started( CinemachineVirtualCameraBase vcam );
    
    private CinemachineVirtualCamera virtualCamera;
}
