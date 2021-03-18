using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    public float camSpeed = 20f;
    float screenWidth = Screen.width;
    float screenHeight = Screen.height;
    
    public Transform target;
    Vector3 offset = new Vector3(0, 10, -10);

    void Update()
    {
        if (Input.mousePosition.x > screenWidth - 30)
        {
            transform.Translate(Vector3.right * camSpeed * Time.deltaTime);
        }
        else if (Input.mousePosition.x < 30)
        {
            transform.Translate(Vector3.left * camSpeed * Time.deltaTime);
        }

        if (Input.mousePosition.y > screenHeight - 30)
        {
            transform.Translate((Vector3.up * Mathf.Sin(0.959931f) + Vector3.forward * Mathf.Cos(0.959931f)).normalized * camSpeed * Time.deltaTime);
        }
        else if (Input.mousePosition.y < 30)
        {
            transform.Translate((Vector3.down * Mathf.Sin(0.959931f) + Vector3.back * Mathf.Cos(0.959931f)).normalized * camSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown("space"))
        {
            transform.position = target.position + offset;
        }
    }
}
