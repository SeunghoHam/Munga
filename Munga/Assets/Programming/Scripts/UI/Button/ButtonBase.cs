using System;
using System.Collections;
using System.Collections.Generic;
using AmplifyShaderEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour, IPointerEnterHandler
{
    //private Text  
    private void Awake()
    {
        //_button.onClick.AddListener(OnClick);
    }
    protected virtual void OnClick()
    {
        
    }
    // 마우스가 버튼 위에 올라갈 때 호출됩니다.
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(this.name+" 버튼 위에 올라갔습니다!");
    }
    
}
