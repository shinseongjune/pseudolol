using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Champion : MonoBehaviour
{
    public int Level { get; set; } = 1;
    public int Exp { get; set; } = 0;
    public int MaxExp { get; set; } = 220;
    public float BaseHP { get; set; }
    public float HP { get; set; }
    public float MaxHP { get; set; }
    public float HPInc { get; }
    public float BaseAD { get; set; }
    public float AD { get; set; }
    public float ADInc { get; }
    public float AP { get; set; } = 0.0f;
    public float BaseDefense { get; set; }
    public float Defense { get; set; }
    public float DefenseInc { get; }
    public float BaseResist { get; set; }
    public float Resist { get; set; }
    public float ResistInc { get; }
    public float BaseAS { get; set; }
    public float AS { get; set; }
    public float ASInc { get; }
    public float CritChance { get; set; } = 0.0f;
    public float CritDamage { get; set; } = 2.0f;
    public float Reach { get; set; } = 160.0f;
    public float MoveSpeed { get; set; }
    public float Tenacity { get; set; } = 0.0f;
    public float ArmorPenetration { get; set; } = 0.0f;
    public float MagicPenetration { get; set; } = 0.0f;

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

    protected virtual void Move(Ray ray, RaycastHit rch)
    {
        Vector3 targetPoint = ray.GetPoint(0);
        destinationPosition = ray.GetPoint(0);
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        myTransform.rotation = targetRotation;
        
        destinationDistance = Vector3.Distance(destinationPosition, myTransform.position);

        if (destinationDistance > .5f)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, MoveSpeed * Time.deltaTime);
        }
    }

    protected abstract void Attack();

    protected abstract void AttackMove();

    protected abstract void Skill();

    protected virtual void Die()
    {
        StopAllCoroutines();

        float respawnTime;

        if (Level <= 6)
        {
            respawnTime = Level * 2 + 4;
        }
        else if (Level == 7)
        {
            respawnTime = 21;
        }
        else
        {
            respawnTime = Level * 2.5f + 7.5f;
        }

        StartCoroutine(DieProcess(respawnTime));
    }

    IEnumerator DieProcess(float respawnTime)
    {
        cc.enabled = false;

        yield return new WaitForSeconds(respawnTime);
        
    }

    protected virtual void Start()
    {
        state = State.Idle;
        cc = GetComponent<CharacterController>();
        myTransform = transform;
        destinationPosition = myTransform.position;
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch, 100))
            {
                state = State.Move;
                Move(ray, rch);
            }
        }
        else if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch, 100))
            {
                state = State.Move;
                Move(ray, rch);
            }
        }

        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Move:
                break;
            case State.Stop:
                Stop();
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
