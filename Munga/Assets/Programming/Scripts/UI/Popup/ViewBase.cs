using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common.DI;
using Assets.Scripts.Manager;
using Assets.Scripts.UI;

public class ViewBase : MonoBehaviour
{
    public FlowManager FlowManager { get; set; }
    public ResourcesManager ResourcesManager { get; set; }
    public PopupManager PopupManager { get; set; }
    public InputManager InputManager { get; set; }
    public DataManager DataManager { get; set; }

    private Animator _uiAnimator;
    
    /// <summary>
    /// 비활성화 효과주기
    /// </summary>
    public virtual void Hide()
    {
        if (_uiAnimator == null)
            _uiAnimator = GetComponent<Animator>();
        _uiAnimator.SetTrigger("Hide");
    }

    /// <summary>
    /// 활성화 효과주기
    /// </summary>
    public virtual void Show()
    {
        if (_uiAnimator == null)
            _uiAnimator = GetComponent<Animator>();
        _uiAnimator.SetTrigger("Show");
    }

    public void DestroyPopup()
    {
        Destroy(PopupManager.PopupList[1].gameObject);
    }
}