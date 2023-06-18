using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ��ư�� ������ ��õ��� �㳷�� ǥ�����ֱ� ���� ��ũ��Ʈ
/// ���� light�� Rotation�� ���� ����� ������ �����Ϸ��� time�� 3.6���� ����� �Ѵ�
/// 
/// ���� �ٲٰ� �Ǹ� math.DeltaAngle�� �̿��� ���� rotate�� ���� rotate�� ���ϰ�
/// �� ���� ������ Global������ �����ϸ鼭 ȸ�� �ѷ��� ���غ��� �����غ���.
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
    public bool isNextDay = false;  // �������� ���ϱ� ���� �÷��װ�
    public bool isDay = false;      // ������ ���ϱ� ���� �÷��װ�
    public bool isNight = false;    // �̺�Ʈ ó�����ֱ� ���� �÷��װ�
    public bool isStart = false;
    public bool isToday = false;
    void Awake()
    {
        dayandNight = this;
    }

    private void Update()
    {
        #region _���� ���� ����
        // ó�� ������ ��ư�� ������ �� 2�ʵ��� ȭ���� ��ο�����.
        //if (isNextDay == true && time <= 2)
        //{
        //    Night();
        //}
        //// ���� ���� �̺�Ʈ�� ���ų� ������ �ȸ¾Ƽ� ������ ���߰ų� �̺�Ʈ�� �� ������ ȭ���� ��ο����� 2~3�ʰ� ���� �� �ٽ� ��� ���ش�.
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

    // ��ο�����
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

    // ����������
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

