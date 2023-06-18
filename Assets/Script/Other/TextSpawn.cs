using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextSpawn : MonoBehaviour
{
    TextMeshProUGUI text;
    NPCText npcText;
    Move moveScript;

    Image textBox;

    private void Awake()
    {
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textBox = gameObject.GetComponentInChildren<Image>();
        npcText = GameObject.FindGameObjectWithTag("TextManager").GetComponent<NPCText>();
        moveScript = gameObject.GetComponent<Move>();
    }
    // Start is called before the first frame update
    void Start()
    {
        textBox.enabled = false;
        text.enabled = false;

        StartCoroutine(TextRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TextRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 10f));

            textBox.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(1.5f, 1f, 0));

            textBox.enabled = true;
            text.enabled = true;
            text.text = npcText.NPCScripts[Random.Range(0, npcText.NPCScripts.Count)];

            moveScript.textActive = true;

            yield return new WaitForSeconds(3f);

            textBox.enabled = false;
            text.enabled = false;

            moveScript.textActive = false;

            yield return null;
        }  
    }
}
