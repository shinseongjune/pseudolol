using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vayne : Champion
{
    public new float BaseHP { get; set; } = 515.0f;
    public new float HP { get; set; } = 515.0f;
    public new float MaxHP { get; set; } = 515.0f;
    public new float HPInc { get; } = 89.0f;
    public new float BaseAD { get; set; } = 60.0f;
    public new float AD { get; set; } = 60.0f;
    public new float ADInc { get; } = 2.36f;
    public new float BaseDefense { get; set; } = 23.0f;
    public new float Defense { get; set; } = 23.0f;
    public new float DefenseInc { get; } = 3.4f;
    public new float BaseResist { get; set; } = 30.0f;
    public new float Resist { get; set; } = 30.0f;
    public new float ResistInc { get; } = 0.5f;
    public new float BaseAS { get; set; } = 0.658f;
    public new float AS { get; set; } = 0.658f;
    public new float ASInc { get; } = 0.033f;
    public new float Reach { get; set; } = 550.0f;
    public new float MoveSpeed { get; set; } = 330.0f;

    Transform myTransform;
    Vector3 destinationPosition;
    float destinationDistance;
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

    protected override void Move(Ray ray, RaycastHit rch)
    {
        base.Move(ray, rch);
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
        base.Update();
    }
}
