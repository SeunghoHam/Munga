using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;
public class DamageText : MonoBehaviour
{
   private TextMesh text;
   // 사이즈 및 텍스트 회전도 관련
   private Camera camera;
   private Vector3 startScale;
   private float distance = 3;

   private bool isDoing = false;
   private void Awake()
   {
      camera = Camera.main;
      text = this.GetComponent<TextMesh>();
      //startScale = this.transform.localScale;
   }

   private void CamSetting()
   {
      float dist = Vector3.Distance(camera.transform.position, this.transform.position);
      Vector3 newScale = startScale * (dist / distance);
      //Debug.Log("newScale : " + newScale);
      //transform.localScale = newScale;
   }
   
   public void SetStateText(StateType type)
   {
      switch (type)
      {
         case StateType.Normal:
            break;
         case StateType.Weak:
            text.text = "Weak";
            text.color = Color.red;
            break;
         case StateType.Resist:
            text.text = "Resist";
            text.color = Color.blue;
            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
   }
   public void SetDamageText(StateType type, int damage)
   {
      switch (type)
      {
         case StateType.Normal:
            text.text = damage.ToString();
            break;
         case StateType.Weak:
            text.text = Mathf.Round(damage * 1.1f).ToString();
            break;
         case StateType.Resist:
            text.text = Mathf.Round(damage * 0.9f).ToString();
            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
   }
   public void SetDamageType(DamageType type)
   {
      switch (type)  
      {
         case DamageType.Fire:
            text.color = Color.red;
            break;
         case DamageType.Water:
            text.color = Color.blue;
            break;
         case DamageType.Electric:
            text.color = Color.yellow;
            break;
         case DamageType.Ground:
            text.color = Color.black;
            break;
         case DamageType.Wind:
            text.color = Color.cyan;
            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
   }
}
