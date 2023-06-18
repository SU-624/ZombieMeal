using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drive : MonoBehaviour
{
    float speed;

    public float slowSpeed;
    public float fastSpeed;

    public float slowDampSpeed;

    public float rotationSpeed;

    Vector3 vel = Vector3.zero;

    Transform target;
    int index;
    float distance;

   public List<Transform> movePoints;

    public int backIndex1, backIndex2;

    public int fastIndex, slowIndex;

    public int stopIndex;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        index = 0;

        speed = slowSpeed;
    }

    private void Update()
    {
        if(index < movePoints.Count)
        {
            animator.SetBool("IsDriving", true);

            target = movePoints[index];

            distance = Vector3.Distance(transform.position, target.position);

            if (index == backIndex1 || index == backIndex2)
            {
                LookBack();
            }
            else
            {
                LookForward();
            }

            SpeedCheck();

            if (distance < 0.3f)
            {
                index++;
            }
            else
            {
                Move();
            }
        }
        else if(index == movePoints.Count)
        {
            LookAtStartPoint();
            animator.SetBool("IsDriving", false);
        }
        
    }

    void LookBack()
    {
        Vector3 lookDir = (target.position - transform.position).normalized;

        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.LookRotation(lookDir);

        transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime*rotationSpeed);
    }

    void LookForward()
    {
        Vector3 lookDir = -(target.position - transform.position).normalized;

        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.LookRotation(lookDir);

        transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime * rotationSpeed);
    }

    void LookAtStartPoint()
    {
        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.Euler(0, -90, 0);

        transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime * rotationSpeed);
    }

    void SpeedCheck()
    {
        if(index == slowIndex)
        {
            speed = slowSpeed;
            rotationSpeed = 2f;
        }
        else if(index == fastIndex)
        {
            speed = fastSpeed;
        }
        else if(index == stopIndex)
        {
            speed = slowDampSpeed;
        }
    }

    private void Move()
    {
        if(index == stopIndex)
        {
            transform.position = Vector3.SmoothDamp(gameObject.transform.position, target.position, ref vel, speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        }
    }
}
