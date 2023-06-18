using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI Cookie;
    private string textName = "Æ÷È¹Áß...";
    private string CookietextName = "ÄíÅ° Á¦Á¶ Áß...";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Typing());
        StartCoroutine(CookieTyping());
    }

   IEnumerator Typing()
    {
        while (true)
        {
            for (int i = 0; i <= textName.Length; i++)
            {
                text.text = textName.Substring(0, i);

                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    IEnumerator CookieTyping()
    {
        while (true)
        {
            for (int i = 0; i <= CookietextName.Length; i++)
            {
                Cookie.text = CookietextName.Substring(0, i);

                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
