using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vayne : Champion
{
    bool isTumbling = false;

    float tumbleSpeed = 12.0f;
    float tumbleTime = 0.2f;
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
            if (tumbleStartTime < tumbleTime)
            {
                cc.SimpleMove(tumbleDirection * tumbleSpeed);
                tumbleStartTime += Time.deltaTime;
                targetPos = transform.position;
                return;
            }
            else
            {
                tumbleDirection = Vector3.zero;
                targetPos = transform.position;
                isTumbling = false;
            }
        }

        base.Update();

        if (Input.GetButtonDown("Q"))
        {
            Ray qRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit qHit;

            if (Physics.Raycast(qRay, out qHit, 10000f))
            {
                tumbleStartTime = 0f;
                tumbleDirection = qHit.point - transform.position;
                tumbleDirection.Normalize();
                isTumbling = true;
            }
        }
        else if (Input.GetButtonDown("W"))
        {
            
        }
    }
}
