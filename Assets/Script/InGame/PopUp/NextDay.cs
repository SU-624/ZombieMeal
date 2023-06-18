using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDay : MonoBehaviour
{
    /// <summary>
    /// 버튼을 누르면 다음날로 넘어가게 하기
    /// 현재 날짜가 다음날로 표시된다
    /// 포획팀 파견결과와 특정 날짜 이벤트 발생
    /// 
    /// </summary> 

    public static NextDay nextDay;

    private void Awake()
    {
        nextDay = this;
    }

    // 업데이트 함수에서 발주서를 한번만 띄우게 해주기 위해 쓰이는 플래그
    public bool isPopUp = false;
    
    public void Update()
    {
        if (GameManager.Instance.UIToday[1].text == "1" && !isPopUp && GameManager.Instance.timer >= 3f && InteractionEvent.Instance.isEvent == true)
        {
            StartCoroutine("FirstDay");
            Debug.LogWarning("첫째날");
        }
        else if ((GameManager.Instance.UIToday[1].text == "6" || GameManager.Instance.UIToday[1].text == "11" || GameManager.Instance.UIToday[1].text == "16")
            && isPopUp == false && InteractionEvent.Instance.isEvent == true && GameManager.Instance.timer >= 6f)
        {
            StartCoroutine("OtherDays");
            Debug.LogWarning("둘째날");
        }
    }

    IEnumerator FirstDay()
    {
        yield return new WaitForSeconds(0.5f);

        //SoundManager.Instance.PlaySE("ContractOpen_Click");
        GameManager.Instance.PopUpOrder();
        isPopUp = true;

        Debug.LogWarning("첫째날의 코루틴");

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

        // 그냥 애니메이션으로 FadeOut In 만들어서 이벤트 적용해보기
        // 그리고 다음날 버튼을 누르고 다시 날이 밝아질 때 이벤트의 오픈데이트의 숫자도 바뀌게
        // 인덱스 증가시켜주기
    }

}
