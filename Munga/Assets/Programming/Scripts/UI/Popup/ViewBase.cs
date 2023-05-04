using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common.DI;
using Assets.Scripts.Manager;
using Assets.Scripts.UI;

public class ViewBase : InputManager
{
    
    public FlowManager FlowManager { get; set; }
    public ResourcesManager ResourcesManager { get; set; }
    public PopupManager PopupManager { get; set; }

    protected virtual void Init()
    {
        DependuncyInjection.Inject(this);
    }
    /// <summary>
    /// 비활성화 효과주기
    /// </summary>
    protected virtual void Hide()
    {
    }
    /// <summary>
    /// 활성화 효과주기
    /// </summary>
    protected virtual void Show()
    {
    }
}
