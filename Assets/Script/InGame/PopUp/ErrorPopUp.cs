using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��ȣ�ۿ� �� �� ������ �������� ���ؼ� ���� �޽����� ���� �� OK
/// ��ư�� ������ �޼��� �˾��� ������ ���ִ� �Լ�
/// 
/// 2022.08.04 Ocean Project1 Room
/// </summary>

public class ErrorPopUp : MonoBehaviour
{
    public void PressButton()
    {
        SoundManager.Instance.PlaySE("ButtonClick");

        if (GameManager.Instance.isPopUp && !GameManager.Instance.isErrorPopUp)
        {
            GameManager.Instance.ErrorPopUp.SetActive(false);

            GameManager.Instance.isPopUp = false;
            GameManager.Instance.PressNextDayButton();
            GameManager.Instance.PopUpBackGroundOff();
        }
        else if(GameManager.Instance.isErrorPopUp)
        {
            GameManager.Instance.ErrorPopUp.SetActive(false);
            //GameManager.Instance.PressNextDayButton();
            GameManager.Instance.isErrorPopUp = false;
        }
    }
}
