using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    SpawnManager spawnManager;

    Animator animator;

    float speed = 1f;
    int index = 0;

    public bool textActive = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TextOn();

        if(gameObject.tag == "Char1")
        {
            Path1();
        }
        else if(gameObject.tag == "Char2")
        {
            Path2();
        }
    }

    void Path1()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();

        if(index <= spawnManager.path1.Count - 1)
        {
            if (gameObject.transform.position == spawnManager.path1[index].transform.position)
            {
                index++;
            }
            else
            {
                Vector3 lookDir = (spawnManager.path1[index].transform.position - transform.position).normalized;

                Quaternion from = transform.rotation;
                Quaternion to = Quaternion.LookRotation(lookDir);

                transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime);

                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, spawnManager.path1[index].transform.position, Time.deltaTime * speed);
            }
        }
    }

    void Path2()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();

        if(index <= spawnManager.path2.Count - 1)
        {
            if (gameObject.transform.position == spawnManager.path2[index].transform.position)
            {
                index++;
            }
            else
            {
                Vector3 lookDir = (spawnManager.path2[index].transform.position - transform.position).normalized;

                Quaternion from = transform.rotation;
                Quaternion to = Quaternion.LookRotation(lookDir);

                transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime);

                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, spawnManager.path2[index].transform.position, Time.deltaTime * speed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }

    void TextOn()
    {
        if (textActive == true)
        {
            speed = 0f;
            animator.SetBool("IsTalking", true);
        }
        else
        {
            speed = 2f;
            animator.SetBool("IsTalking", false);
        }
            

    }
}
