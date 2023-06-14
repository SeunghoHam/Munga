using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
public class DamageText : MonoBehaviour
{
   private float moveSpeed = 0.5f;
   private float alphaSpeed = 4.0f;
   private float destoryTime =2.0f;

   private TextMesh text;
   private Color color;

   // 사이즈 및 텍스트 회전도 관련
   private Camera camera;
   private Vector3 startScale;
   private float distance = 3;

   private bool isDoing = false;
   private void Awake()
   {
      camera = Camera.main;
      text = this.GetComponent<TextMesh>();
      color = text.color;
      //StartCoroutine(DestoryoObject());

      startScale = this.transform.localScale;
      this.gameObject.SetActive(false);
   }
   private void Update()
   {
      CamSetting();
   }

   private void CamSetting()
   {
      // 데미지 택스트가 카메라를 항상 바라보도록 설정
      this.transform.rotation = camera.transform.rotation;
      float dist = Vector3.Distance(camera.transform.position, this.transform.position);
      //Debug.Log("Dist :" + dist);
      Vector3 newScale = startScale * (dist / distance);
      //Debug.Log("newScale : " + newScale);
      //transform.localScale = newScale;
   }

   public void Active(int damage)
   {
      gameObject.SetActive(true);
      text.text = damage.ToString();
      //Vector3 upperVec = new Vector3(0, this.transform.position.y + 1f, 0);
      this.transform.DOLocalMoveY(transform.position.y + 0.2f, 0.4f)
         .OnComplete(() =>
         {
            Destroy();
         });
   }

   public void Destroy()
   {
      gameObject.SetActive(false);
      //Destroy(this.gameObject);
   }

   private WaitForSeconds perSec = new WaitForSeconds(0.2f);
   private IEnumerator ColorChange()
   {
      float color = 1f;
      while (text.color.a >= 0)
      {
         color = 1;
         yield return perSec;
      }
   }
   private IEnumerator DestoryoObject()
   {
      yield return new WaitForSeconds(2f);
      Destroy(this.gameObject);
   }
}
