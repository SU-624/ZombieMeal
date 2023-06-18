using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무언가 상호작용 할 때 조건을 충족하지 못해서 에러 메시지가 떴을 때 OK
/// 버튼을 누르면 메세지 팝업이 꺼지게 해주는 함수
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
