using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public float MoveSpeed { get; set; } = 2.2f;
    public float Tenacity { get; set; } = 0.0f;
    public float ArmorPenetration { get; set; } = 0.0f;
    public float MagicPenetration { get; set; } = 0.0f;

    public CharacterController cc;

    public Vector3 targetPos;

    public NavMeshAgent agent;

    public Animator anim;

    public GameObject attackTarget;

    float dist;

    float attackDist;

    public bool attacking;

    Vector3 posXZ;

    Vector3 aTargetPosXZ;

    public GameObject bulletFactory;

    public GameObject normalAttackFirePosition;

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
        Exp = extraExp;
    }

    protected virtual void Damaged(float dmg)
    {
        HP -= dmg;

        if (HP <= 0)
        {
            Die();
        }
    }

    protected virtual void Idle()
    {

    }

    protected virtual void Stop()
    {

    }

    protected virtual void Attack()
    {
        if (!anim.GetBool("AttackDelay"))
        {
            GameObject bullet = Instantiate(bulletFactory);
            Bullet b = bullet.GetComponent<Bullet>();
            b.transform.position = this.normalAttackFirePosition.transform.position;
            b.attacker = this.gameObject;
            b.target = attackTarget;
            b.targetPos = attackTarget.transform.position;
            anim.SetBool("AttackDelay", true);
            StartCoroutine(NormalAttackDelay());
        }
    }

    IEnumerator NormalAttackDelay()
    {
        anim.SetBool("AttackDelay", true);
        yield return new WaitForSecondsRealtime(1 / AS);
        anim.SetBool("AttackDelay", false);
    }

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

        Revive();
    }

    protected virtual void Revive()
    {
        HP = MaxHP;
        cc.enabled = true;
        targetPos = new Vector3(0, 0, 0);
    }

    protected virtual void Start()
    {
        targetPos = transform.position;
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Walk", false);
        anim.SetFloat("WalkSpeed", MoveSpeed / 3f);
        anim.SetBool("AttackDelay", false);
        agent.speed = MoveSpeed * 1.2f;
        attacking = false;
        posXZ = new Vector3(transform.position.x, 0, transform.position.z);
    }

    protected virtual void Update()
    {
        //float dis = Vector3.Distance(transform.position, targetPos);
        //if (dis >= 0.01f)
        //{
        //transform.localPosition = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed * Time.deltaTime);
        //cc.Move(targetPos);
        //    isMoving = true;
        //}
        //else
        //{
        //    isMoving = false;
        //}

        posXZ = new Vector3(transform.position.x, 0, transform.position.z);
        anim.SetFloat("WalkSpeed", MoveSpeed / 3f);
        anim.SetFloat("AttackSpeed", 1 / AS);
        agent.speed = MoveSpeed * 1.2f;


        //if (isMoving)
        //{
        //    Vector3 dir = targetPos;
        //    Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        //    Quaternion targetRot = Quaternion.LookRotation(dirXZ);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 25.0f * Time.deltaTime);
        //}

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int mask = 1 << LayerMask.NameToLayer("Map") | 1 << LayerMask.NameToLayer("RedPlayer") | 1 << LayerMask.NameToLayer("Extra");
            
            if (Physics.Raycast(ray, out hit, 10000f, mask))
            {
                string hitName = hit.transform.gameObject.name;
                if (hitName == "Ground")
                {
                    //Vector3 targetPosXZ = new Vector3(hit.point.x, 0f, hit.point.z);
                    //Vector3 targetDir = Vector3.MoveTowards(transform.position, targetPosXZ, MoveSpeed * Time.deltaTime);
                    //targetPos = targetDir - transform.position;
                    targetPos = hit.point;
                    attackTarget = null;
                    anim.SetBool("Walk", true);
                }
                else
                {
                    attackTarget = hit.transform.gameObject;
                    attacking = true;
                }
            }
        }
        else if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int mask = 1 << LayerMask.NameToLayer("Map") | 1 << LayerMask.NameToLayer("RedPlayer") | 1 << LayerMask.NameToLayer("Extra");

            if (Physics.Raycast(ray, out hit, 10000f, mask))
            {
                string hitName = hit.transform.gameObject.name;
                if (hitName == "Ground")
                {
                    //Vector3 targetPosXZ = new Vector3(hit.point.x, 0f, hit.point.z);
                    //Vector3 targetDir = Vector3.MoveTowards(transform.position, targetPosXZ, MoveSpeed * Time.deltaTime);
                    //targetPos = targetDir - transform.position;
                    targetPos = hit.point;
                    attackTarget = null;
                    anim.SetBool("Walk", true);
                }
            }
        }

        if (attackTarget == null)
        {
            attacking = false;
            anim.SetBool("Attack", false);
        }

        if (attacking == true)
        {
            aTargetPosXZ = new Vector3(attackTarget.transform.position.x, 0, attackTarget.transform.position.z);
            attackDist = Vector3.Distance(posXZ, aTargetPosXZ);
            if (attackDist <= Reach / 20)
            {
                targetPos = transform.position;
                agent.SetDestination(transform.position);
                transform.LookAt(new Vector3(attackTarget.transform.position.x, transform.position.y, attackTarget.transform.position.z));
                anim.SetBool("Walk", false);
                anim.SetBool("Attack", true);
                Attack();
            }
            else
            {
                targetPos = attackTarget.transform.position;
                anim.SetBool("Attack", false);
                anim.SetBool("Walk", true);
            }
        }

        if (anim.GetBool("Walk"))
        {
            agent.destination = targetPos;
            dist = Vector3.Distance(agent.transform.position, targetPos);
            if (dist <= 0.5f)
            {
                anim.SetBool("Walk", false);
            }
        }

    }
}
