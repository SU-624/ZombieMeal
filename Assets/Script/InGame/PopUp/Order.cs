using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ּ��� ��ư�� ������ �ٲ��� �� �κе��� ������ִ� �Լ� ����
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
