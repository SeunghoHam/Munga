using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan : Monster
{
    SkinnedMeshRenderer mRanderer;
    GameObject model;

    [Header("Effects")]
    ParticleSystem currentEffect;
    [SerializeField] ParticleSystem meleeAttackEffect;


    public int meleeAttackFailCount
    {
        get;
        private set;
    }
    public int rangeAttackFailCount
    {
        get;
        private set;
    }


    protected override void Awake()
    {
        base.Awake();
        
        if (mRanderer == null)  mRanderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (model == null)      model = mAnimator.gameObject;
        if (meleeAttackEffect == null)  meleeAttackEffect = GetComponentInChildren<ParticleSystem>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
    }


    #region Action

    public override void Idle()
    {
        mAnimator.SetBool("IsWalk", false);
        mAgent.speed = 0;
    }

    public override void Move()
    {
        mAnimator.SetBool("IsWalk", true);
        mAgent.speed = speed;
        mAgent.destination = target.transform.position;     
    }

    #region Attack

    public override void Attack()
    {
        ActionStart();
        NormalMeleeAttack();
        //������ ����
    }
    public void RangeAttack()
    {
        ActionStart();
        mAnimator.SetTrigger("RangeAttack");
        //�� ������ ������
    }

    void NormalMeleeAttack()
    {
        mAnimator.SetTrigger("NormalMeleeAttack");
        StartCoroutine(NormalMeleeAttackCoroutine());
    }
    public void NormalMeleeAttackEffectPlay(Vector3 _angles)
    {
        currentEffect?.Stop();
        meleeAttackEffect.transform.localEulerAngles = _angles;
        currentEffect = meleeAttackEffect;
        currentEffect.Play();
    }


    IEnumerator NormalMeleeAttackCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        meleeAttackEffect.Play();
        meleeAttackEffect.transform.localEulerAngles =  new Vector3(-70, 90, 180);
        yield return new WaitForSeconds(0.7f);
        meleeAttackEffect.Play();
        meleeAttackEffect.transform.localEulerAngles = new Vector3(70, 90, 180);
        //0.55��
        yield break;
    }

    void HeavyMeleeAttack()
    {
        mAnimator.SetTrigger("HeavyMeleeAttack");
    }
    IEnumerator HeavyMeleeAttackCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        meleeAttackEffect.Play();
        meleeAttackEffect.transform.eulerAngles = new Vector3(-70, 90, 180);
        yield return new WaitForSeconds(0.7f);
        meleeAttackEffect.Play();
        meleeAttackEffect.transform.eulerAngles = new Vector3(70, 90, 180);
        //3Ÿ
        yield break;
    }



    public void DashAttack()
    {
        ActionStart();
        StartCoroutine(DashAttackCoroutine());        
    }
    IEnumerator DashAttackCoroutine()
    {
        mAnimator.SetTrigger("DashAttack");
        float startY = model.transform.position.y;
        yield return new WaitForSeconds(0.55f);
        float curY;
        while (true)
        {
            model.transform.position += Vector3.up * 50 * Time.deltaTime;
            curY = model.transform.position.y;
            if (curY - startY > 50)
                break;
            yield return null;
        }

        mRanderer.enabled = false;

        //����Ʈ Ȱ��ȭ

        float count = 0;       
        while (true)
        {
            count += Time.deltaTime;
            this.transform.position = target.transform.position;
            if (count >= 1)
                break;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        mRanderer.enabled = true;
        //�������
        while (true)
        {
            model.transform.position -= Vector3.up * 100 * Time.deltaTime;
            curY = model.transform.position.y;
            if (model.transform.position.y < transform.position.y)
            {
                model.transform.position = transform.position;
                break;
            }
            yield return null;
        }
        //�ٴڵ�������
        mAnimator.SetTrigger("DashAttackNext");
        yield return new WaitForSeconds(1.0f);
        mAnimator.SetTrigger("DashAttackNext");
        yield break;
    }

    public void CounterAttack()
    {
        ActionStart();
        StartCoroutine(CounterAttackCoroutine());
    }
    IEnumerator CounterAttackCoroutine()
    {
        mAnimator.SetTrigger("CounterCharge");
        while (IsActioning)
        {
            if (true)//��Ʈ ��
            {
                mAnimator.SetTrigger("CounterAttack");
                //ī���� ����
                yield break;
            }
            yield return null;
        }
        yield break;
    }

    #endregion

    #endregion



    protected override void Dead()
    {
        Debug.Log("���");
    }

    public override void SearchTarget()
    {
        
    }
}
