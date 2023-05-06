using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class DashObjectManager : MonoBehaviour
{
    public static DashObjectManager instance;

    [SerializeField] private GameObject DashObject;
    private Image backDashImage;
    private Image frontDashImage;

    private float _currnetDashGauge; // gauge
    private float _maxDashGauge = 10f;

    private float _length = 1f;

    private float _scale = 0.6f;

    private bool isDashing; // 대쉬중인지
    private bool isDashObjectEnable;
    public bool isSprintStarted; // 스프린트가 시작되었는지

    private Camera _camera;

    public bool Dashing
    {
        get { return isDashing; }
        set
        {
            if (value)
            {
                if (DashRoutine != null)
                    StopCoroutine(DashRoutine);
                
                if (DashObject.transform.localScale != Vector3.one)
                {
                    DashEnable();
                    isDashObjectEnable = true;
                }
                DashRoutine = Decrease();
                //isSprintStarted = false;
            }
            else
            {
                DebugManager.instance.Log("대기 게이지 회복하기", DebugManager.TextColor.Blue);
                DashRoutine = Increase();
            }
            isDashing = value;
        }
    }

    private float _dashDuration = 0.1f;

    private IEnumerator DashRoutine;
    private WaitForSeconds _perSec = new WaitForSeconds(0.02f);

    private void Awake()
    {
        backDashImage = DashObject.transform.GetChild(0).GetComponent<Image>();
        frontDashImage = DashObject.transform.GetChild(1).GetComponent<Image>();

        if (instance == null)
            instance = this;
        _currnetDashGauge = _maxDashGauge;

        _camera = Camera.main;

        DashDisable();
    }


    private IEnumerator Increase()
    {
        //Debug.Log("대쉬 게이지 상승");
        
        while (!isDashing) // 대쉬중이 아닐때만 상승되도록
        {
            if (_currnetDashGauge >= _maxDashGauge)
            {
                Debug.Log("회복 종료");
                DashDisable();
                yield break;
            }
            
            _currnetDashGauge += _length;
            frontDashImage.fillAmount = _currnetDashGauge / _maxDashGauge;
            yield return _perSec;
        }
        yield break;
    }

    private IEnumerator Decrease()
    {
        //Debug.Log("대쉬 게이지 하락");
        while (isDashing)
        {
            if (_currnetDashGauge <= 0f)
            {
                //DashDisable();
                Debug.Log("대쉬 한계지점");
                yield break;
            }
            
            _currnetDashGauge -= _length;
            frontDashImage.fillAmount = _currnetDashGauge / _maxDashGauge;
            yield return _perSec;
        }

        yield break;
    }

    private void Update()
    {
        this.transform.LookAt(_camera.transform);

        // 대쉬 ui 가 활성화 되어있지 않다면 반환시키기
        if (!isDashObjectEnable)
            return;

        if (isDashing) // 데쉬 게이지 감소
        {
            _currnetDashGauge -= _length * Time.deltaTime;
            //if (_currnetDashGauge <= 0)
            if (_currnetDashGauge < 0)
            {
                //Debug.Log("대쉬 게이지 0 에 수렴");
                _currnetDashGauge = 0;
                //isDashObjectEnable = false;   
            }
        }
        else // 대쉬 게이지 회복
        {
            _currnetDashGauge += _length * Time.deltaTime;
            if (_currnetDashGauge > _maxDashGauge)
            {
                //Debug.Log("대쉬 게이지 회복 완료");
                _currnetDashGauge = _maxDashGauge;
                DashDisable();

                isDashObjectEnable = false;
            }
        }

        frontDashImage.fillAmount = _currnetDashGauge / _maxDashGauge;
        if (_currnetDashGauge >= _maxDashGauge || _currnetDashGauge <= 0)
            return;
        //Debug.Log("currentFillAmount : " +frontDashImage.fillAmount);
    }

    
    
    private void DashEnable()
    {
        //Debug.Log("Dash Enable");
        DashObject.transform.localScale = Vector3.one  *_scale;
        DashObject.transform.DOScale(_scale, _dashDuration).From(_scale - 0.2f).SetEase(Ease.InQuad);
        frontDashImage.DOFade(1f, _dashDuration).From(0f).SetEase(Ease.Linear);
        backDashImage.DOFade(1f, _dashDuration).From(0f).SetEase(Ease.Linear);
    }

    private void DashDisable()
    {
        frontDashImage.transform.DOLocalMoveX(0f, _dashDuration).SetEase(Ease.Linear);
        backDashImage.transform.DOLocalMoveX(0f, _dashDuration).SetEase(Ease.Linear);
        DashObject.transform.DOScale(0f, _dashDuration).SetEase(Ease.Linear);
    }
}