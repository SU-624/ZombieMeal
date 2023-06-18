using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인형 버튼을 누르면 선택되게 하는 함수
/// 선택이 된 인형의 정보를 리스트에 담아둔다.
/// for문으로 돌리고 싶었지만 그러면 버튼 하나를 선택하면 
/// 다른 곳도 다 같이 선택된걸로 된다..
/// 
/// isSelected변수는 사용하고있지 않다.
/// 
/// </summary>
public class CheckDolls : MonoBehaviour
{
    int Value;

    public void PressButton1()
    {

        if (GameManager.Instance.CookieUI[0].text != "")
        {
            SoundManager.Instance.PlaySE("DollSelected");
            GameManager.Instance.Selected[0].SetActive(true);

            GameManager.Instance.DollText[0].SetActive(true);
            GameManager.Instance.DollText_text[0].text = GameManager.Instance.dollInfo[0].DollText;

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[0], out Value);
            
            GameManager.Instance.Check_Dolls.Add(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet += GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin += 100;

            GameManager.Instance.isSelected = true;

            GameManager.Instance.SelectedUI.Add(0);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }

    public void PressButton2()
    {
        if (GameManager.Instance.CookieUI[1].text != "")
        {
            SoundManager.Instance.PlaySE("DollSelected");

            GameManager.Instance.Selected[1].SetActive(true);

            GameManager.Instance.DollText[1].SetActive(true);
            GameManager.Instance.DollText_text[1].text = GameManager.Instance.dollInfo[1].DollText;

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[1], out Value);

            GameManager.Instance.Check_Dolls.Add(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet += GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin += 100;

            GameManager.Instance.isSelected = true;

            GameManager.Instance.SelectedUI.Add(1);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton3()
    {
        if (GameManager.Instance.CookieUI[2].text != "")
        {
            SoundManager.Instance.PlaySE("DollSelected");

            GameManager.Instance.Selected[2].SetActive(true);

            GameManager.Instance.DollText[2].SetActive(true);
            GameManager.Instance.DollText_text[2].text = GameManager.Instance.dollInfo[2].DollText;
            
            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[2], out Value);

            GameManager.Instance.Check_Dolls.Add(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet += GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin += 100;

            GameManager.Instance.isSelected = true;

            GameManager.Instance.SelectedUI.Add(2);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton4()
    {
        if (GameManager.Instance.CookieUI[3].text != "")
        {
            SoundManager.Instance.PlaySE("DollSelected");

            GameManager.Instance.Selected[3].SetActive(true);

            GameManager.Instance.DollText[3].SetActive(true);
            GameManager.Instance.DollText_text[3].text = GameManager.Instance.dollInfo[3].DollText;

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[3], out Value);

            GameManager.Instance.Check_Dolls.Add(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet += GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin += 100;

            GameManager.Instance.isSelected = true;

            GameManager.Instance.SelectedUI.Add(3);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton5()
    {
        if (GameManager.Instance.CookieUI[4].text != "")
        {
            SoundManager.Instance.PlaySE("DollSelected");

            GameManager.Instance.Selected[4].SetActive(true);

            GameManager.Instance.DollText[4].SetActive(true);
            GameManager.Instance.DollText_text[4].text = GameManager.Instance.dollInfo[4].DollText;

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[4], out Value);

            GameManager.Instance.Check_Dolls.Add(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet += GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin += 100;

            GameManager.Instance.isSelected = true;

            GameManager.Instance.SelectedUI.Add(4);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton6()
    {
        if (GameManager.Instance.CookieUI[5].text != "")
        {
            SoundManager.Instance.PlaySE("DollSelected");

            GameManager.Instance.Selected[5].SetActive(true);

            GameManager.Instance.DollText[5].SetActive(true);
            GameManager.Instance.DollText_text[5].text = GameManager.Instance.dollInfo[5].DollText;

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[5], out Value);

            GameManager.Instance.Check_Dolls.Add(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet += GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin += 100;

            GameManager.Instance.isSelected = true;

            GameManager.Instance.SelectedUI.Add(5);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton7()
    {
        if (GameManager.Instance.CookieUI[6].text != "")
        {
            SoundManager.Instance.PlaySE("DollSelected");

            GameManager.Instance.Selected[6].SetActive(true);

            GameManager.Instance.DollText[6].SetActive(true);
            GameManager.Instance.DollText_text[6].text = GameManager.Instance.dollInfo[6].DollText;
            
            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[6], out Value);

            GameManager.Instance.Check_Dolls.Add(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet += GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin += 100;

            GameManager.Instance.isSelected = true;

            GameManager.Instance.SelectedUI.Add(6);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);

        }

    }
    public void PressButton8()
    {
        if (GameManager.Instance.CookieUI[7].text != "")
        {
            SoundManager.Instance.PlaySE("DollSelected");

            GameManager.Instance.Selected[7].SetActive(true);

            GameManager.Instance.DollText[7].SetActive(true);
            GameManager.Instance.DollText_text[7].text = GameManager.Instance.dollInfo[7].DollText;
            
            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[7], out Value);

            GameManager.Instance.Check_Dolls.Add(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet += GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin += 100;

            GameManager.Instance.isSelected = true;

            GameManager.Instance.SelectedUI.Add(7);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);

        }

    }
    public void PressButton9()
    {
        if (GameManager.Instance.CookieUI[8].text != "")
        {
            SoundManager.Instance.PlaySE("DollSelected");

            GameManager.Instance.Selected[8].SetActive(true);

            GameManager.Instance.DollText[8].SetActive(true);
            GameManager.Instance.DollText_text[8].text = GameManager.Instance.dollInfo[8].DollText;

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[8], out Value);

            GameManager.Instance.Check_Dolls.Add(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet += GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin += 100;

            GameManager.Instance.isSelected = true;

            GameManager.Instance.SelectedUI.Add(8);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
}
