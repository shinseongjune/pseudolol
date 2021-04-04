using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vayne : Champion
{
    bool isTumbling = false;

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
            this.Tumble();
            isTumbling = false;
        }
    }

    void Tumble()
    {
        
    }
}
