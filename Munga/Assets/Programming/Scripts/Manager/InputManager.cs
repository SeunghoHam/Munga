using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common;
using Assets.Scripts.UI.Popup.PopupView;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : UnitySingleton<InputManager>
{ 
    public UIInput _input {  get;  set; }

    private BasicView basicView; 
    public void InputEnable()
    {
        //base.Initialize();

        // 활성화 되면서 input할당되도록
        _input = GetComponent<UIInput>();
        
        if (_input != null)
        {
            Debug.Log("input 할당 " + _input.gameObject.name);
            //return;
        }
        else return;
        //Input.UIActions.Character.Enable();
        //Input.UIActions.ESC.Enable();
    }
    public void UpdateInit()
    {
        _input.InputActions.UI.Quest.started += OnQuestStarted;
        _input.InputActions.UI.Character.started += OnCharacterStarted;
        _input.InputActions.UI.ESC.started += OnEscStarted;    
    }
    
    protected void OnInit(ViewBase view)
    {
        switch (view.GetComponent<ViewBase>())
        {
            case BasicView :
                basicView = view.gameObject.GetComponent<BasicView>();
                if(basicView != null)
                    Debug.Log(basicView.name +" 할당됨");
                else Debug.Log("null");
                
                //_input.InputActions.UI.ESC.started += OnEscStarted;
                break;
        }
            
    }
    protected virtual void OnQuestStarted(InputAction.CallbackContext context)
    {
        Debug.Log("퀘스트 창 켜기");
    }

    protected virtual void OnCharacterStarted(InputAction.CallbackContext context)
    {
        Debug.Log("캐릭터 창 켜기");
    }

    private void OnEscStarted(InputAction.CallbackContext context)
    {
        Debug.Log("ESC 켜기");
    }
}