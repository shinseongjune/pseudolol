using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Champion : MonoBehaviour
{
    public float HP { get; set; }
    public float AD { get; set; }
    public float AP { get; set; }
    public float Defense { get; set; }
    public float Resist { get; set; }
    public float AS { get; set; }
    public float CritChance { get; set; }
    public float CritDamage { get; set; }
    public float Reach { get; set; }
    public float MoveSpeed { get; set; }
    public float Tenacity { get; set; }

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

    protected virtual void Idle()
    {

    }

    protected abstract void Stop();

    protected abstract void Move();

    protected abstract void Attack();

    protected abstract void AttackMove();

    protected abstract void Skill();

    protected abstract void Die();

    protected virtual void Start()
    {
        state = State.Idle;
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
            case State.Die:
                Die();
                break;
        }
    }
}
