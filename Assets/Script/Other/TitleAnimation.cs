using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    public List<GameObject> animationUI;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("TitleON", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TitleON()
    {
        for(int i = 0; i < animationUI.Count; i++)
        {
            animationUI[i].SetActive(true);
        }
    }
}
