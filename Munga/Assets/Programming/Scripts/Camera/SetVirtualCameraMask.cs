using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SetVirtualCameraMask : VirtualCameraReactToBlendOut
{
    public LayerMask cullingMaskWhileLive;
    private LayerMask _savedLayerMask;
 
    public override void _OnVirtualCameraAnimateIn_Started( CinemachineVirtualCameraBase vcam )
    {
        Debug.Log("Camera in Started");

    }
 
    public override void _OnVirtualCameraAnimateIn_Finished( CinemachineVirtualCameraBase vcam )
    {
        Debug.Log("Camera in End");
        _savedLayerMask = Camera.main.cullingMask;
        Camera.main.cullingMask = cullingMaskWhileLive;
    }
 
    public override void _OnVirtualCameraAnimateOut_Started( CinemachineVirtualCameraBase vcam )
    {
        Debug.Log("Camera Out End");

        Camera.main.cullingMask = _savedLayerMask;
    }


   [SerializeField] private Camera camera;
   [SerializeField] private float[] distaces;
    private void Start()
    {
         camera = GetComponent<Camera>();
         distaces = new float[32];
         distaces[8] = 10;
         distaces[9] = 15;
         distaces[10] = 20;
         camera.layerCullDistances = distaces;
         
         Debug.Log(camera.layerCullDistances[9]);
         Debug.Log(camera.layerCullDistances[10]);
         Debug.Log(camera.layerCullDistances[11]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            _OnVirtualCameraAnimateIn_Finished(this.GetComponent<CinemachineVirtualCameraBase>());
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            _OnVirtualCameraAnimateOut_Started(this.GetComponent<CinemachineVirtualCameraBase>());
        } 
    }
}
