using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    public float BaseHP { get; set; }
    public float HP { get; set; }
    public float MaxHP { get; set; }
    public float BaseAD { get; set; }
    public float AD { get; set; }
    public float AP { get; set; } = 0.0f;
    public float BaseDefense { get; set; }
    public float Defense { get; set; }
    public float BaseResist { get; set; }
    public float Resist { get; set; }
    public float BaseAS { get; set; }
    public float AS { get; set; }
    public float Reach { get; set; } = 160.0f;
    public float MoveSpeed { get; set; } = 1.5f;
    public float Tenacity { get; set; } = 0.0f;
    public float ArmorPenetration { get; set; } = 0.0f;
    public float MagicPenetration { get; set; } = 0.0f;

    public CharacterController cc;

    public Vector3 targetPos;

    public NavMeshAgent agent;

    public Animator anim;

    float dist;
    protected void Damaged(float dmg)
    {
        HP -= dmg;

        if (HP <= 0)
        {
            Die();
        }
    }

    protected void Idle()
    {

    }

    protected void Stop()
    {

    }
    protected void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    protected virtual void Start()
    {
        targetPos = transform.position;
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Walk", false);
        anim.SetFloat("WalkSpeed", MoveSpeed / 3f);
        agent.speed = MoveSpeed * 1.2f;
    }

    protected virtual void Update()
    {
        /*anim.SetFloat("WalkSpeed", MoveSpeed / 3f);
        agent.speed = MoveSpeed * 1.2f;

        if (anim.GetBool("Walk"))
        {
            agent.destination = targetPos;
            dist = Vector3.Distance(agent.transform.position, targetPos);
            if (dist <= 0.5f)
            {
                anim.SetBool("Walk", false);
            }

        }*/


    }
}
