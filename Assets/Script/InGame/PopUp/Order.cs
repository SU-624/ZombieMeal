using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 발주서의 버튼을 누르면 바뀌어야 할 부분들을 계산해주는 함수 실행
/// 
/// 2022.08.03 Ocean Project1 Room
/// </summary>

public class Order : MonoBehaviour
{
    public void PressButton()
    {
        //GameManager.Instance.NextDay();
        SoundManager.Instance.PlaySE("ContractOpen_Click");
        GameManager.Instance.Off_Order();
        //NextDay.nextDay.isPopUp = true;
    }
}
