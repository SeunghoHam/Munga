using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan : Monster
{
    private SkinnedMeshRenderer mRanderer;
    private GameObject model;


    [Header("RangeAttack")]
    [SerializeField] List<GameObject> shotPointList = new List<GameObject>();
    [SerializeField] TitanBullet bullet;
    [SerializeField] List<TitanBullet> instBulletList = new List<TitanBullet>();

    [Header("Effects")]
    [SerializeField] ParticleSystem meleeAttackEffect;

    private ParticleSystem currentEffect;


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

    protected override void Init()
    {
        base.Init();
    }


    #region Action

    public override void Idle()
    {
        ChangeState(MonsterState.IDLE);
    }

    public override void Move()
    {
        ChangeState(MonsterState.MOVE);
        mAgent.destination = target.transform.position;     
    }

    #region Attack

    public override void Attack()
    {
        ActionStart();
        NormalMeleeAttack();
        //강공격 조건
    }

    void NormalMeleeAttack()
    {
        mAnimator.SetTrigger("NormalMeleeAttack");
        //StartCoroutine(NormalMeleeAttackCoroutine());
        currentEffect?.Stop();
        currentEffect = meleeAttackEffect;
        currentEffect.Play();
    }
    //public void NormalMeleeAttackEffectPlay(Vector3 _angles)
    //{
    //    currentEffect?.Stop();
    //    meleeAttackEffect.transform.localEulerAngles = _angles;
    //    currentEffect = meleeAttackEffect;
    //    currentEffect.Play();
    //}

    //IEnumerator NormalMeleeAttackCoroutine()
    //{
    //    yield return new WaitForSeconds(0.6f);
    //    meleeAttackEffect.Play();
    //    meleeAttackEffect.transform.localEulerAngles =  new Vector3(-70, 90, 180);
    //    yield return new WaitForSeconds(0.7f);
    //    meleeAttackEffect.Play();
    //    meleeAttackEffect.transform.localEulerAngles = new Vector3(70, 90, 180);
    //    //0.55초
    //    yield break;
    //}

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
        //3타
        yield break;
    }

    #region RangeAttack
    public void RangeAttack()
    {
        ActionStart();
        mAnimator.SetTrigger("RangeAttack");
    }

    public void RangeAttackInst()
    {
        StartCoroutine(RangeAttackInstCoroutine());        
    }
    IEnumerator RangeAttackInstCoroutine()
    {
        foreach (var point in shotPointList)
        {
            TitanBullet instBullet = Instantiate(bullet, point.transform);
            instBullet.Init();
            instBulletList.Add(instBullet);
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    public void RangeAttackShot()
    {
        StartCoroutine(RangeAttackShotCoroutine());
    }
    IEnumerator RangeAttackShotCoroutine()
    {
        foreach (var bullet in instBulletList)
        {
            bullet.Shot(target.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
        instBulletList.Clear();
        yield break;
    }
    #endregion

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

        //이펙트 활성화

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
        //내려찍기
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
        //바닥도착이후
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
            if (true)//히트 시
            {
                mAnimator.SetTrigger("CounterAttack");
                //카운터 어택
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
        Debug.Log("사망");
    }

    public override void SearchTarget()
    {
        
    }
}
