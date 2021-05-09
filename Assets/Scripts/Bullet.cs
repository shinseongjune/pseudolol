using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 10f;
    public GameObject attacker;
    public GameObject target;
    public Vector3 targetPos;
    Vector3 dir;
    float distance;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            targetPos = target.transform.position;
            dir = targetPos - transform.position;
            dir.Normalize();
            transform.LookAt(target.transform);
            transform.position += dir * speed * Time.deltaTime;

            distance = Vector3.Distance(targetPos, transform.position);
            if (distance < 0.5f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            dir = targetPos - transform.position;
            dir.Normalize();
            transform.LookAt(target.transform);
            transform.position += dir * speed * Time.deltaTime;

            distance = Vector3.Distance(targetPos, transform.position);
            if (distance < 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
