using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vayne : Champion
{
    bool isTumbling = false;

    float tumbleSpeed = 20.0f;
    float tumbleTime = 0.1f;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int mask = 1 << LayerMask.NameToLayer("Map") | 1 << LayerMask.NameToLayer("RedPlayer") | 1 << LayerMask.NameToLayer("Extra");

            if (Physics.Raycast(ray, out hit, 10000f, mask))
            {
                string hitName = hit.transform.gameObject.name;
                if (hitName == "Ground")
                {
                    tumbleDirection = hit.point;
                }
            }
            StartCoroutine(Tumble());
            isTumbling = false;
        }
    }

    IEnumerator Tumble()
    {
        if (tumbleStartTime < tumbleTime)
        {
            cc.Move(tumbleDirection * Time.deltaTime * tumbleSpeed);
            tumbleStartTime += Time.deltaTime;
        }
        else
        {
            tumbleDirection = Vector3.zero;
            yield return null;
        }
    }
}
