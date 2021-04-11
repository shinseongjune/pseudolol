using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vayne : Champion
{
    bool isTumbling = false;

    float tumbleSpeed = 2.0f;
    float tumbleTime = 0.8f;
    float tumbleStartTime = 0f;
    Vector3 tumbleDirection;

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void AttackMove()
    {
        throw new System.NotImplementedException();
    }

    protected override void Damaged(float dmg)
    {
        base.Damaged(dmg);
    }

    protected override void Die()
    {
        base.Die();
    }

    protected override void GetEXP(int exp)
    {
        base.GetEXP(exp);
    }

    protected override void Idle()
    {
        base.Idle();
    }

    protected override void LevelUp(int extraExp)
    {
        base.LevelUp(extraExp);
    }

    protected override void Revive()
    {
        base.Revive();
    }

    protected override void Skill()
    {
        throw new System.NotImplementedException();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Stop()
    {
        base.Stop();
    }

    protected override void Update()
    {
        if (isTumbling == true)
        {
            return;
        }

        base.Update();

        if (Input.GetButtonDown("Q"))
        {
            isTumbling = true;
            tumbleStartTime = 0f;
            Ray qRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit qHit;

            if (Physics.Raycast(qRay, out qHit, 10000f))
            {
                Vector3 xzPosition = new Vector3(transform.position.x, 0, transform.position.z);
                tumbleDirection = qHit.point - xzPosition;
                tumbleDirection.Normalize();
                transform.rotation = new Quaternion(tumbleDirection.x, tumbleDirection.y, tumbleDirection.z, 0);
                StartCoroutine(Tumble());
            }
        }
    }

    IEnumerator Tumble()
    {
        while (true)
        {
            if (tumbleStartTime < tumbleTime)
            {
                cc.Move(tumbleDirection * Time.deltaTime * tumbleSpeed);
                tumbleStartTime += Time.deltaTime;
                targetPos = transform.position;
            }
            else
            {
                tumbleDirection = Vector3.zero;
                targetPos = transform.position;
                isTumbling = false;
                yield return null;
            }
        }
    }
}
