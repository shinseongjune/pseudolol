using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Champion : MonoBehaviour
{
    public int Level { get; set; }
    public int Exp { get; set; }
    public int MaxExp { get; set; }
    public float BaseHP { get; set; }
    public float HP { get; set; }
    public float MaxHP { get; set; }
    public float BaseAD { get; set; }
    public float AD { get; set; }
    public float BaseAP { get; set; }
    public float AP { get; set; }
    public float BaseDefense { get; set; }
    public float Defense { get; set; }
    public float BaseResist { get; set; }
    public float Resist { get; set; }
    public float BaseAS { get; set; }
    public float AS { get; set; }
    public float CritChance { get; set; } = 0.0f;
    public float CritDamage { get; set; } = 2.0f;
    public float Reach { get; set; } = 160.0f;
    public float MoveSpeed { get; set; }
    public float Tenacity { get; set; } = 0.0f;
    public float ArmorPenetration { get; set; } = 0.0f;
    public float MagicPenetration { get; set; } = 0.0f;

    CharacterController cc;

    enum State
    {
        Idle,
        Stop,
        Move,
        Attack,
        AttackMove,
        Skill,
        Die
    }

    State state;

    protected virtual void GetEXP(int exp)
    {
        if (Level == 18)
        {
            return;
        }

        Exp += exp;

        if (Exp >= MaxExp)
        {
            int extraExp = Exp - MaxExp;
            LevelUp(extraExp);
        }
    }

    protected virtual void LevelUp(int extraExp)
    {
        if (Level == 18)
        {
            return;
        }

        Level += 1;
    }

    protected virtual void Damaged(float dmg)
    {
        HP -= dmg;

        if (HP <= 0)
        {
            state = State.Die;
            Die();
        }
    }

    protected virtual void Idle()
    {

    }

    protected virtual void Stop()
    {

    }

    protected abstract void Move();

    protected abstract void Attack();

    protected abstract void AttackMove();

    protected abstract void Skill();

    protected virtual void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(10.0f);
        
    }

    protected virtual void Start()
    {
        state = State.Idle;
        cc = GetComponent<CharacterController>();
    }

    protected virtual void Update()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Stop:
                Stop();
                break;
            case State.Move:
                Move();
                break;
            case State.Attack:
                Attack();
                break;
            case State.AttackMove:
                AttackMove();
                break;
            case State.Skill:
                Skill();
                break;
        }
    }
}
