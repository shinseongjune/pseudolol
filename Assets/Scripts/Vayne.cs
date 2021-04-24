using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vayne : Champion
{
    float tumbleSpeed = 12.0f;
    float tumbleTime = 0.2f;
    float tumbleStartTime = 0f;
    Vector3 tumbleDirection;
    Quaternion tumbleQ;
    Vector3 qHitPosition;

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
        tumbleQ = Quaternion.identity;
        qHitPosition = Vector3.zero;
        base.Start();
        anim.SetFloat("TumbleTime", tumbleTime);
    }

    protected override void Stop()
    {
        base.Stop();
    }

    protected override void Update()
    {
        if (anim.GetBool("Tumble"))
        {
            if (tumbleStartTime < tumbleTime)
            {
                cc.SimpleMove(tumbleDirection * tumbleSpeed);
                tumbleStartTime += Time.deltaTime;
                transform.LookAt(qHitPosition);
                return;
            }
            else
            {
                targetPos = transform.position;
                agent.destination = transform.position;
                anim.SetBool("Tumble", false);
                transform.rotation = tumbleQ;
                return;
            }
        }
        else
        {
            base.Update();
        }

        if (Input.GetButtonDown("Q"))
        {
            Ray qRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit qHit;

            if (Physics.Raycast(qRay, out qHit, 10000f))
            {
                anim.SetBool("Walk", false);
                targetPos = transform.position;
                tumbleStartTime = 0f;
                qHitPosition = new Vector3(qHit.point.x, transform.position.y, qHit.point.z);
                tumbleDirection = qHit.point - new Vector3(transform.position.x, qHit.point.y, transform.position.z);
                tumbleDirection.Normalize();
                tumbleQ = Quaternion.LookRotation(tumbleDirection);
                transform.rotation = tumbleQ;
                anim.SetBool("Tumble", true);
            }
        }
        else if (Input.GetButtonDown("W"))
        {
            
        }
    }
}
