using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanBullet : MonoBehaviour
{
    private Vector3 targetPoint;
    private Vector3 dir;

    public void Init()
    {

    }

    public void Shot(Vector3 _targetPoint)
    {
        targetPoint = _targetPoint;
        dir = (targetPoint - this.gameObject.transform.position).normalized;
        StartCoroutine(ShotCoroutine());
    }

    IEnumerator ShotCoroutine()
    {
        float timer = 0;
        while (timer < 10.0f)
        {
            timer += Time.deltaTime;
            this.gameObject.transform.position += dir * 10.0f * Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        
        Destroy(this.gameObject);
    }


}
