using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    public UIInputActions InputActions { get; private set; }
    public UIInputActions.UIActions UIActions { get; private set; }


    private InputManager inputManager;
    private void Awake()
    {
        InputActions = new UIInputActions();
        UIActions = InputActions.UI;

        inputManager = GetComponent<InputManager>();
    }
    private void OnEnable()
    {
        InputActions.Enable();
        inputManager.InputEnable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}
