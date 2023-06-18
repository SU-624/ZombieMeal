using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 다음날 버튼을 누르면 잠시동안 밤낮을 표현해주기 위한 스크립트
/// 원래 light의 Rotation과 가장 비슷한 각도를 유지하려면 time을 3.6으로 해줘야 한다
/// 
/// 만약 바꾸게 되면 math.DeltaAngle을 이용해 이전 rotate와 현재 rotate를 비교하고
/// 이 둘의 차분을 Global변수에 축적하면서 회전 총량을 구해봐서 조절해보자.
/// 
/// 2022. 08. 13 Ocean House
/// </summary>
public class DayAndNight : MonoBehaviour
{
    //public static DayAndNight dayAndNight;

    //[SerializeField] private float secondPerRealTimeSecond = 1000;
    //public bool isDay = false;
    //public bool isNight = false;
    //public bool isStop = false;

    //public float time;
    //public float checkTime;

    //private void Start()
    //{
    //    time = 3.7f;
    //    checkTime = 0f;

    //}

    //private void Awake()
    //{
    //    dayAndNight = this;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (isDay)
    //    {
    //        Day();
    //    }
    //}

    //public void Day()
    //{
    //    time -= Time.deltaTime;

    //    if (time > checkTime && !isStop)
    //    {
    //        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);

    //        if (transform.eulerAngles.x >= 170)
    //        {
    //            isNight = true;
    //            isStop = true;
    //            transform.Rotate(Vector3.right, 90f);
    //        }
    //        else if (transform.eulerAngles.x <= 20)
    //            isNight = false;

    //        GameManager.Instance.NoPressNextDayButton();
    //    }
    //    else
    //    {
    //        GameManager.Instance.UIToday[1].text = (int.Parse(GameManager.Instance.UIToday[1].text) + 1).ToString();
    //        GameManager.Instance.isTodayCapture = false;
    //        GameManager.Instance.isTodayMakeCookie = false;
    //        NextDay.nextDay.isPopUp = false;
    //        GameManager.Instance.PressNextDayButton();
    //        isDay = false;
    //    }
    //}

    public static DayAndNight dayandNight;
    public GameObject Bloom;
    public Image Day;
    public float time = 0f;
    public float StartTime = 0f;
    float f_Time = 2f;
    public bool isNextDay = false;  // 저녁으로 변하기 위한 플래그값
    public bool isDay = false;      // 낮으로 변하기 위한 플래그값
    public bool isNight = false;    // 이벤트 처리해주기 위한 플래그값
    public bool isStart = false;
    public bool isToday = false;
    void Awake()
    {
        dayandNight = this;
    }

    private void Update()
    {
        #region _낮밤 이전 구현
        // 처음 다음날 버튼을 눌렀을 때 2초동안 화면이 어두워진다.
        //if (isNextDay == true && time <= 2)
        //{
        //    Night();
        //}
        //// 만약 저녁 이벤트가 없거나 조건이 안맞아서 실행을 못했거나 이벤트가 다 끝나면 화면이 어두워지고 2~3초가 지난 후 다시 밝게 해준다.
        //else if (isDay == false && time <= 2 && isStart == true)
        //{
        //    DayTime();
        //}
        //if (isDay == false && time >= 1 && isStart == true)
        //{
        //    if (isToday)
        //    {
        //        isNight = false;
        //        GameManager.Instance.UIToday[1].text = (int.Parse(GameManager.Instance.UIToday[1].text) + 1).ToString();
        //        GameManager.Instance.isTodayCapture = false;
        //        GameManager.Instance.isTodayMakeCookie = false;
        //        NextDay.nextDay.isPopUp = false;
        //        GameManager.Instance.PressNextDayButton();
        //        isToday = false;
        //    }
        //}
        //if (time >= 0.9 && isDay == false && isToday == false)
        //{
        //    isStart = true;
        //    time = 0;
        //}
        #endregion

        if (isNextDay == true && time <= 2)
        {
            Night();
        }
        if (isDay == true && time <= 2 && isNight == false)
        {
            DayTime();
        }
        if (time >= 2 && isNextDay == true && isStart == true)
        {
            isNextDay = false;
            time = 0;
            isDay = true;
            isNight = false;
        }
        if (time >= 2 && isNextDay == true && isStart == false)
        {
            if (isDay == false)
            {
                StartTime += Time.deltaTime;

                if (StartTime >= 1)
                {
                    isStart = true;
                    StartTime = 0;
                }
            }
        }
        if (time >= 1 && isDay == true && isNight == false)
        {
            isDay = false;
            time = 0;

            if (isToday == true)
            {
                GameManager.Instance.UIToday[1].text = (int.Parse(GameManager.Instance.UIToday[1].text) + 1).ToString();
                GameManager.Instance.isTodayCapture = false;
                GameManager.Instance.isTodayMakeCookie = false;
                NextDay.nextDay.isPopUp = false;
                GameManager.Instance.PressNextDayButton();
                isToday = false;
                GameManager.Instance.timer = 0;
            }
        }


    }

    // 어두워지는
    public void Night()
    {
        GameManager.Instance.NoPressNextDayButton();

        time += Time.deltaTime / f_Time;
        Color alpha = Day.color;

        alpha.a = Mathf.Lerp(0, 0.95f, time);

        if(alpha.a >= 0.5)
        {
            Bloom.SetActive(true);
        }

        if (alpha.a >= 0.9f)
        {
            //isNextDay = false;
            isNight = true;
        }

        Day.color = alpha;
    }

    // 투명해지는
    public void DayTime()
    {
        GameManager.Instance.PressNextDayButton();

        time += Time.deltaTime / f_Time;

        Color alpha = Day.color;

        alpha.a = Mathf.Lerp(0.95f, 0, time);

        if(alpha.a <= 0.5)
        {
            Bloom.SetActive(false);
        }

        if (alpha.a == 0)
        {
            isStart = false;
            isToday = true;
        }
        Day.color = alpha;
    }

}

