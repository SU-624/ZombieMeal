using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    // 디데이 날짜에 발주서를 한번만 띄우기 위해 사용하는 플레그
    public bool isResult = false;

    public void Update()
    {
        // 지정날짜와 플래그가 모두 성립해야지 실행되게 했다. 플래그를 안해주면 매 프레임마다 업데이트가 되서 발주서가 계속 나온다.
        if ((GameManager.Instance.UIToday[1].text == "5" || GameManager.Instance.UIToday[1].text == "10" ||
        GameManager.Instance.UIToday[1].text == "15" || GameManager.Instance.UIToday[1].text == "20") && isResult == false)
        {
            GameManager.Instance.Result();
            isResult = true;
        }
    }

    public void PressButton()
    {
        SoundManager.Instance.PlaySE("ContractOpen_Click");
        GameManager.Instance.PressContinueButton();
        NextDay.nextDay.isPopUp = false;
    }
}
