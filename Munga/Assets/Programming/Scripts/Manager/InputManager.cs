using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public UIInput _input { get; private set; }

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
        _input.InputActions.UI.Quest.started += OnQuestStarted;
        _input.InputActions.UI.Character.started += OnCharacterStarted;
        _input.InputActions.UI.ESC.started += OnEscStarted;
    }

    public void InputDisable()
    {
    }
    

    protected virtual void OnQuestStarted(InputAction.CallbackContext context)
    {
        Debug.Log("퀘스트 창 켜기");
    }

    protected virtual void OnCharacterStarted(InputAction.CallbackContext context)
    {
        Debug.Log("캐릭터 창 켜기");
    }

    protected virtual void OnEscStarted(InputAction.CallbackContext context)
    {
        Debug.Log("ESC 활성화");
    }
}