using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dummy : Enemy
{
    protected override void Start()
    {
        base.Start();

        StartCoroutine(CheckingAttack());
    }

    private void Update()
    {
        Vector3 dir = (target.GetComponent<Collider>().bounds.center - this.transform.position).normalized;
        dir.y = 0;

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10);
        rigid.velocity = dir * speed * Vector3.Distance(new Vector3(target.GetComponent<Collider>().bounds.center.x, this.transform.position.y, target.GetComponent<Collider>().bounds.center.z) - dir * attackRange * 0.8f, this.transform.position);
        rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, speed);
    }

    IEnumerator CheckingAttack()
    {
        while(true)
        {
            if (Vector3.Distance(new Vector3(target.GetComponent<Collider>().bounds.center.x, this.transform.position.y, target.GetComponent<Collider>().bounds.center.z), this.transform.position) <= attackRange)
            {
                target.GetComponent<PlayerController>().DecreaseHp(damage);
            }

            yield return new WaitForSeconds(attackDelay);
        }
    }
}
