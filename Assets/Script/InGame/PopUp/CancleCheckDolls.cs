using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancleCheckDolls : MonoBehaviour
{
    int Value;
    int SelectedUI_Val;

    public void PressButton1()
    {
        /// SelectedUI리스트는 인덱스 값을 저장하는 리스트여서 지워주고 다른 값으로 채울 필요가없다.
        if (GameManager.Instance.CookieUI[0].text != "")
        {
            GameManager.Instance.Selected[0].SetActive(false);
            
            GameManager.Instance.DollText[0].SetActive(false);
            GameManager.Instance.DollText_text[0].text = "";

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[0], out Value);

            GameManager.Instance.Check_Dolls.Remove(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet -= GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin -= 100;

            GameManager.Instance.isSelected = false;

            SelectedUI_Val = GameManager.Instance.FindListIndex(GameManager.Instance.SelectedUI, 0);
            GameManager.Instance.SelectedUI.RemoveAt(SelectedUI_Val);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }

    public void PressButton2()
    {
        if (GameManager.Instance.CookieUI[1].text != "")
        {                                 
            GameManager.Instance.Selected[1].SetActive(false);

            GameManager.Instance.DollText[1].SetActive(false);
            GameManager.Instance.DollText_text[1].text = "";

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[1], out Value);

            GameManager.Instance.Check_Dolls.Remove(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet -= GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin -= 100;

            GameManager.Instance.isSelected = false;

            SelectedUI_Val = GameManager.Instance.FindListIndex(GameManager.Instance.SelectedUI, 1);
            GameManager.Instance.SelectedUI.RemoveAt(SelectedUI_Val);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton3()
    {
        if (GameManager.Instance.CookieUI[2].text != "")
        {
            GameManager.Instance.Selected[2].SetActive(false);

            GameManager.Instance.DollText[2].SetActive(false);
            GameManager.Instance.DollText_text[2].text = "";

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[2], out Value);

            GameManager.Instance.Check_Dolls.Remove(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet -= GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin -= 100;

            GameManager.Instance.isSelected = false;

            SelectedUI_Val = GameManager.Instance.FindListIndex(GameManager.Instance.SelectedUI, 2);
            GameManager.Instance.SelectedUI.RemoveAt(SelectedUI_Val);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton4()
    {
        if (GameManager.Instance.CookieUI[3].text != "")
        {
            GameManager.Instance.Selected[3].SetActive(false);

            GameManager.Instance.DollText[3].SetActive(false);
            GameManager.Instance.DollText_text[3].text = "";

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[3], out Value);
            
            GameManager.Instance.Check_Dolls.Remove(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet -= GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin -= 100;

            GameManager.Instance.isSelected = false;

            SelectedUI_Val = GameManager.Instance.FindListIndex(GameManager.Instance.SelectedUI, 3);
            GameManager.Instance.SelectedUI.RemoveAt(SelectedUI_Val);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton5()
    {
        if (GameManager.Instance.CookieUI[4].text != "")
        {
            GameManager.Instance.Selected[4].SetActive(false);

            GameManager.Instance.DollText[4].SetActive(false);
            GameManager.Instance.DollText_text[4].text = "";

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[4], out Value);

            GameManager.Instance.Check_Dolls.Remove(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet -= GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin -= 100;

            GameManager.Instance.isSelected = false;

            SelectedUI_Val = GameManager.Instance.FindListIndex(GameManager.Instance.SelectedUI, 4);
            GameManager.Instance.SelectedUI.RemoveAt(SelectedUI_Val);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton6()
    {
        if (GameManager.Instance.CookieUI[5].text != "")
        {
            GameManager.Instance.Selected[5].SetActive(false);

            GameManager.Instance.DollText[5].SetActive(false);
            GameManager.Instance.DollText_text[5].text = "";

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[5], out Value);

            GameManager.Instance.Check_Dolls.Remove(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet -= GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin -= 100;

            GameManager.Instance.isSelected = false;

            SelectedUI_Val = GameManager.Instance.FindListIndex(GameManager.Instance.SelectedUI, 5);
            GameManager.Instance.SelectedUI.RemoveAt(SelectedUI_Val);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton7()
    {
        if (GameManager.Instance.CookieUI[6].text != "")
        {
            GameManager.Instance.Selected[6].SetActive(false);

            GameManager.Instance.DollText[6].SetActive(false);
            GameManager.Instance.DollText_text[6].text = "";

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[6], out Value);

            GameManager.Instance.Check_Dolls.Remove(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet -= GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin -= 100;

            GameManager.Instance.isSelected = false;

            SelectedUI_Val = GameManager.Instance.FindListIndex(GameManager.Instance.SelectedUI, 6);
            GameManager.Instance.SelectedUI.RemoveAt(SelectedUI_Val);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton8()
    {
        if (GameManager.Instance.CookieUI[7].text != "")
        {
            GameManager.Instance.Selected[7].SetActive(false);

            GameManager.Instance.DollText[7].SetActive(false);
            GameManager.Instance.DollText_text[7].text = "";

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[7], out Value);

            GameManager.Instance.Check_Dolls.Remove(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet -= GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin -= 100;

            GameManager.Instance.isSelected = false;

            SelectedUI_Val = GameManager.Instance.FindListIndex(GameManager.Instance.SelectedUI, 7);
            GameManager.Instance.SelectedUI.RemoveAt(SelectedUI_Val);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);
        }

    }
    public void PressButton9()
    {
        if (GameManager.Instance.CookieUI[8].text != "")
        {
            GameManager.Instance.Selected[8].SetActive(false);

            GameManager.Instance.DollText[8].SetActive(false);
            GameManager.Instance.DollText_text[8].text = "";

            GameManager.Instance.SelectedDolls.TryGetValue(GameManager.Instance.CookieUI[8], out Value);

            GameManager.Instance.Check_Dolls.Remove(GameManager.Instance.dollInfo[Value]);

            GameManager.Instance.CookieGet -= GameManager.Instance.dollInfo[Value].Cookie_Get;
            GameManager.Instance.useCoin -= 100;

            GameManager.Instance.isSelected = false;

            SelectedUI_Val = GameManager.Instance.FindListIndex(GameManager.Instance.SelectedUI, 8);
            GameManager.Instance.SelectedUI.RemoveAt(SelectedUI_Val);

            GameManager.Instance.TotalCookies.text = string.Format("{0}", GameManager.Instance.CookieGet);
            GameManager.Instance.TotalCoin.text = string.Format("{0}", GameManager.Instance.useCoin);

        }

    }
}
