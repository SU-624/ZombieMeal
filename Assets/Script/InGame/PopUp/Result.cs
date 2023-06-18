using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    // ���� ��¥�� ���ּ��� �ѹ��� ���� ���� ����ϴ� �÷���
    public bool isResult = false;

    public void Update()
    {
        // ������¥�� �÷��װ� ��� �����ؾ��� ����ǰ� �ߴ�. �÷��׸� �����ָ� �� �����Ӹ��� ������Ʈ�� �Ǽ� ���ּ��� ��� ���´�.
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
