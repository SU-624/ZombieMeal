using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDay : MonoBehaviour
{
    /// <summary>
    /// ��ư�� ������ �������� �Ѿ�� �ϱ�
    /// ���� ��¥�� �������� ǥ�õȴ�
    /// ��ȹ�� �İ߰���� Ư�� ��¥ �̺�Ʈ �߻�
    /// 
    /// </summary> 

    public static NextDay nextDay;

    private void Awake()
    {
        nextDay = this;
    }

    // ������Ʈ �Լ����� ���ּ��� �ѹ��� ���� ���ֱ� ���� ���̴� �÷���
    public bool isPopUp = false;
    
    public void Update()
    {
        if (GameManager.Instance.UIToday[1].text == "1" && !isPopUp && GameManager.Instance.timer >= 3f && InteractionEvent.Instance.isEvent == true)
        {
            StartCoroutine("FirstDay");
            Debug.LogWarning("ù°��");
        }
        else if ((GameManager.Instance.UIToday[1].text == "6" || GameManager.Instance.UIToday[1].text == "11" || GameManager.Instance.UIToday[1].text == "16")
            && isPopUp == false && InteractionEvent.Instance.isEvent == true && GameManager.Instance.timer >= 6f)
        {
            StartCoroutine("OtherDays");
            Debug.LogWarning("��°��");
        }
    }

    IEnumerator FirstDay()
    {
        yield return new WaitForSeconds(0.5f);

        //SoundManager.Instance.PlaySE("ContractOpen_Click");
        GameManager.Instance.PopUpOrder();
        isPopUp = true;

        Debug.LogWarning("ù°���� �ڷ�ƾ");

        //yield return null;
        StopCoroutine("FirstDay");
    }

    IEnumerator OtherDays()
    {
        //Coroutine runungCoroutine = null;

        yield return new WaitForSeconds(0.5f);


        //SoundManager.Instance.PlaySE("ContractOpen_Click");
        GameManager.Instance.PopUpOrder();
        isPopUp = true;

        Debug.LogWarning("OtherDays");


        StopCoroutine("OtherDays");

        //StopAllCoroutines();
    }


    public void PressButton()
    {
        SoundManager.Instance.PlaySE("ButtonClick");
        //DayAndNight.dayandNight.isDay = true;
        //DayAndNight.dayandNight.time = 3.6f;
        //DayAndNight.dayandNight.Day();
        DayAndNight.dayandNight.time = 0;
        DayAndNight.dayandNight.isNextDay = true;

        // �׳� �ִϸ��̼����� FadeOut In ���� �̺�Ʈ �����غ���
        // �׸��� ������ ��ư�� ������ �ٽ� ���� ����� �� �̺�Ʈ�� ���µ���Ʈ�� ���ڵ� �ٲ��
        // �ε��� ���������ֱ�
    }

}
