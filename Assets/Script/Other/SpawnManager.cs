using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<GameObject> StartPoints;
    public List<GameObject> EndPoints;
    public List<GameObject> Zombie;

    public List<GameObject> path1;
    public List<GameObject> path2;

    Move move;

    //bool canWalk = true;
    GameObject spawnZombie;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            int index = Random.Range(0, StartPoints.Count);
            Debug.Log(index);

            Vector3 startPoint = StartPoints[index].transform.position;
            spawnZombie = Zombie[Random.Range(0, Zombie.Count)].gameObject;

            GameObject instance_char = (GameObject)Instantiate(spawnZombie, startPoint, Quaternion.identity);
            Debug.Log(instance_char.name);

            move = instance_char.GetComponent<Move>();

            switch (index)
            {
                case 0:
                    instance_char.tag = "Char1";
                    break;

                case 1:
                    instance_char.tag = "Char2";
                    break;
            }

            yield return new WaitForSeconds(5f);
        }
    }
}
