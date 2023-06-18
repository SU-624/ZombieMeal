using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// 
/// </summary>
public class InteractionEvent : MonoBehaviour
{
    private static InteractionEvent _instance;

    public static InteractionEvent Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(InteractionEvent)) as InteractionEvent; ;

                if (_instance == null)
                {
                    Debug.Log("no Singleton Obj");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        sendTime = 0f;
        waitingTime = 2f;
        NowEventIndex = 0;
        isEvent = true;
        isGameOver = false;
    }

    // 11일날 저녁으로 넘어갈 때 이벤트 인덱스가 증가하지 않아서 가족사진 이벤트 실행 안됌
    private void Update()
    {
        sendTime += Time.deltaTime;

        if (sendTime > waitingTime && isGameOver == false)
        {
            RequirementEvent();
            sendTime = 0;
        }
    }

    /// 현재 실행하고 있는 이벤트의 인덱스(i)
    public int NowEventIndex;
    private float sendTime;
    private float waitingTime;

    /// 이벤트가 실행될 때는 다른걸 못하게 해야한다.
    public bool isEvent;
    public bool isGameOver;

    /// 특정 조건에 이벤트 패널을 켜주기 위한 변수
    public GameObject EventPenal;
    public GameObject eventContent, eventBox;
    public GameObject EventSelectionButton1;
    public GameObject EventSelectionButton2;
    public GameObject EventSelectionButton3;
    public TextMeshProUGUI EventTitle;

    /// 버튼이 생성됐을 때 이벤트가 반복적으로 메세지를 보내지 못하게 하는 플래그
    public bool EventButton1Exist;
    public bool EventButton2Exist;
    public bool EventButton3Exist;

    /// 조건을 만족해야지 열릴 이벤트의 데이터를 저장할 리스트
    public List<EventInfo> checkEvent = new List<EventInfo>();
    public Dictionary<string, string> saveEventID = new Dictionary<string, string>();   // 어떤 이벤트를 완료 했는지 저장하기위한 변수, value1
    //public Dictionary<string, int> saveDollID = new Dictionary<string, int>();          // 어떤 이벤트를 완료 했는지 저장하기위한 변수. value2
    public Dictionary<int, string> saveDollID = new Dictionary<int, string>();
    public List<string> dollID = new List<string>();
    public List<string> nextEvent1ID = new List<string>();                               // 선택지에 따른 다음 이벤트를 찾아주기위해 이벤트 아이디를 비교하기 위한 리스트   
    public List<string> nextEvent2ID = new List<string>();                               // 선택지에 따른 다음 이벤트를 찾아주기위해 이벤트 아이디를 비교하기 위한 리스트   
    public List<string> nextEvent3ID = new List<string>();
    public List<int> tempOpenDate = new List<int>();                                            // 다음날 눌렀을 때 현재 이벤트의 정보를 저장해서 안한 이벤트는 뛰어넘어갈 수 있게 해주기 위한 리스트

    // 선택지에 따른 다음 이벤트를 찾아주기위해 이벤트 아이디를 비교하기 위한 리스트   

    // TODO:OCEAN - 이벤트가 발생하면 다른 작업을 하지 못하게 막으려고
    public bool IsEventExecuted()
    {
        //??
        // 이벤트가 나올 때 발생하는 조건들을 다 만족하면 true를 반환한다.

        return false;
    }

    // 이벤트 UI에 텍스트를 만들어주는 함수
    public void SendEventText(string text)
    {
        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newMessageBox = Instantiate(eventBox, eventContent.transform);

        newMessageBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newMessage.text;
    }

    public void SendEventSelectionText1(string text)
    {
        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newMessageBox = Instantiate(EventSelectionButton1, eventContent.transform);

        newMessageBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newMessage.text;

        newMessageBox.GetComponent<Button>().onClick.AddListener(SelectionText1);

        EventButton1Exist = true;
    }

    public void SendEventSelectionText2(string text)
    {
        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newMessageBox = Instantiate(EventSelectionButton2, eventContent.transform);

        newMessageBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newMessage.text;

        newMessageBox.GetComponent<Button>().onClick.AddListener(SelectionText2);

        EventButton2Exist = true;
    }

    public void SendEventSelectionText3(string text)
    {
        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newMessageBox = Instantiate(EventSelectionButton3, eventContent.transform);

        newMessageBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newMessage.text;

        newMessageBox.GetComponent<Button>().onClick.AddListener(SelectionText3);

        EventButton3Exist = true;
    }

    /// <summary>
    /// 주기적으로 이벤트 체크를 하기 위한 함수
    /// </summary>
    public void RequirementEvent()
    {
        // PSEUDO Code (한글)
        // 오픈 데이트가 맞고, 오픈타입1도 맞고, 오픈타입2도 맞는 i번째
        // 이벤트인포를 찾아서.....
        EventInfo _nowInfo = ParsingEvent.EventInfoList[NowEventIndex];
        CheckEvent(_nowInfo);

        // 모든 데이터를 순회 할 때까지 못찾았다면 그냥 이벤트가 없는거다.
    }

    public void CheckEvent(EventInfo info)
    {
        // 특수하게 오픈 데이트가 아닌 오픈타입만 있는 이벤트만 따로 예외처리 해줬다.
        if (info.OpenDate == 0 && (info.OpenType1 == 4 || info.OpenType2 == 4 || info.OpenType1 == 5))
        {
            //if (DayAndNight.dayandNight.isNight == true && info.ID != "M303")
            //{
            //    UntilTheOpenDate11();
            //}
            if(info.ID == "S004" && int.Parse(info.Value1) <= int.Parse(GameManager.Instance.Coin.text))
            {
                NowEventIndex  = 90;
            }
            else if (Check_OpenType1(info) == false || Check_OpenType2(info) == false)
            {
                return;
            }
            else
            {
                isEvent = false;
                SoundManager.Instance.PlayEvent("EventAlarm");

                if (!saveEventID.ContainsKey(info.ID))
                {
                    saveEventID.Add(info.ID, "완료");
                }

                EventPenal.SetActive(true);
                GameManager.Instance.PopUpBackGroundOn();
                GameManager.Instance.NoPressNextDayButton();

                OneEventLine(info);
            }
        }
        // 첫째날 인형 포획은 했지만 쿠키 제조는 하지 않았을 때 예외처리
        else if (GameManager.Instance.GetOpenDate() == 1 && info.ID == "M103" && DayAndNight.dayandNight.isNight == true)
        {
            // 인덱스를 OpenType1이 나올 때까지 늘려주고 이벤트 실행
            UntilTheOpenType1ToFirstDay();
        }
        else if (info.OpenDate == 19 && GameManager.Instance.GetOpenDate() == 19 && info.OpenType1 != 1 &&DayAndNight.dayandNight.isNight == true)
        {
            FindEventID();
            
            isEvent = false;
            DayAndNight.dayandNight.isDay = true;
            DayAndNight.dayandNight.isStart = false;

            SoundManager.Instance.PlayEvent("EventAlarm");
            EventPenal.SetActive(true);
            //GameManager.Instance.PopUpBackGroundOn();
            GameManager.Instance.NoPressNextDayButton();

            OneEventLine(info);
        }
        // 밤이 되어야지 실행되는 이벤트
        else if (GameManager.Instance.GetOpenDate() == info.OpenDate && (info.OpenType1 == 1 || info.OpenType2 == 1))
        {
            if (Check_OpenDate(GameManager.Instance.GetOpenDate(), info) == true
                && Check_OpenType1(info) == true
                && Check_OpenType2(info) == true && DayAndNight.dayandNight.isNight == true)
            {
                isEvent = false;
                DayAndNight.dayandNight.isDay = true;
                DayAndNight.dayandNight.isStart = false;

                SoundManager.Instance.PlayEvent("EventAlarm");
                EventPenal.SetActive(true);
                //GameManager.Instance.PopUpBackGroundOn();
                GameManager.Instance.NoPressNextDayButton();

                OneEventLine(info);
            }
            else if (Check_OpenDate(GameManager.Instance.GetOpenDate(), info) == true &&
                (Check_OpenType1(info) == false || Check_OpenType2(info) == false || DayAndNight.dayandNight.isNight == false))
            {
                return;
            }
        }
        // 이런 조건은 NextEvent에 나오는 조건이라 NextEvent가 눌리면 이벤트 진행되게 해줬다.
        else if (info.OpenDate == 0 && info.OpenType1 == 0 && info.OpenType1 == 0 && info.ID != "")
        {
            if (!saveEventID.ContainsKey(info.ID) && info.ID != "")
            {
                saveEventID.Add(info.ID, "완료");
            }
            PlayNextEventSound();
            OneEventLine(info);
        }
        // 현재 실행 중인 이벤트의 다음줄 조건이니 무조건 진행해준다.
        else if (info.OpenDate == 0 && info.OpenType1 == 0 && info.OpenType1 == 0)
        {
            OneEventLine(info);
        }
        // 이벤트를 하지않고 다음날로 넘어갔을 때의 예외처리, 인게임 날짜와 같아질 때 까지 이벤트의 인덱스를 증가시킨다
        else if (GameManager.Instance.GetOpenDate() != info.OpenDate && info.OpenDate != 0)
        {
            if (info.OpenDate == 4 && GameManager.Instance.GetOpenDate() == 5)
            {
                // 이 날은 아무것도 하지 않기 때문에 넘겨준다.
                return;
            }
            else if (info.OpenDate == 6 && GameManager.Instance.GetOpenDate() == 4)
            {
                // 이 날은 아무것도 하지 않기 때문에 넘겨준다.
                return;
            }
            else if (info.OpenDate == 9 && GameManager.Instance.GetOpenDate() == 10)
            {
                // 인덱스를 OpenDate가 11이 될 때 까지 증가시켜준다.
                UntilTheOpenDate11();
            }
            else if (info.OpenDate == 11 && GameManager.Instance.GetOpenDate() == 12)
            {
                // 인덱스를 OpenDate가 14가 될 때까지 증가시켜준다.
                UntilTheOpenDate14();
            }
            else if (info.OpenDate == 14 && (GameManager.Instance.GetOpenDate() == 11 || GameManager.Instance.GetOpenDate() == 12 || GameManager.Instance.GetOpenDate() == 13))
            {
                // 이 날은 아무것도 하지 않기 때문에 넘겨준다.
                return;
            }
            else if (info.OpenDate == 14 && GameManager.Instance.GetOpenDate() == 15)
            {
                // 인덱스를 OpenDate가 16가 될 때까지 증가시켜준다.
                UntilTheOpenDate16();
            }
            else if (info.OpenDate == 16 && GameManager.Instance.GetOpenDate() == 14)
            {
                // 이 날은 아무것도 하지 않기 때문에 넘겨준다.
                return;
            }
            else if (info.OpenDate == 16 && GameManager.Instance.GetOpenDate() == 15)
            {
                // 이 날은 아무것도 하지 않기 때문에 넘겨준다.
                return;
            }
            else if (info.OpenDate == 16 && GameManager.Instance.GetOpenDate() == 17)
            {
                // 인덱스를 OpenDate가 17가 될 때까지 증가시켜준다.
                UntilTheOpenDate17();
            }
            else if (info.OpenDate == 17 && GameManager.Instance.GetOpenDate() == 18)
            {
                // 인덱스를 OpenDate가 18가 될 때까지 증가시켜준다.
                UntilTheOpenDate18();
            }
            else if (info.OpenDate == 18 && GameManager.Instance.GetOpenDate() == 19)
            {
                // 인덱스를 OpenDate가 19가 될 때까지 증가시켜준다.
                UntilTheOpenDate19();
            }
            else if (info.OpenDate == 11 && GameManager.Instance.GetOpenDate() == 9)
            {
                return;
            }
            // 만약 하루에 진행하는 이벤트가 하나만 있다면 그 이벤트의 OpenDate가 오늘 날짜에 하루 더한것과 같으면 그냥 넘어간다.
            else if (info.OpenDate == GameManager.Instance.GetOpenDate() + 1)
            {
                return;
            }
            else
            {
                FindNextOpenDate();
            }
        }
        // 9일차 이벤트 두 가지 분기점 예외처리
        else if (GameManager.Instance.GetOpenDate() == 9 && info.OpenType1 == 3 && info.OpenType2 == 9 && GameManager.Instance.isTodayMakeCookie)
        {
            if (Check_OpenType1(info) == false || Check_OpenType2(info) == false)
            {
                NowEventIndex++;
            }
            else if (Check_OpenType1(info) == true && Check_OpenType2(info) == true)
            {
                isEvent = false;

                if (!saveEventID.ContainsKey(info.ID))
                {
                    saveEventID.Add(info.ID, "완료");
                }

                SoundManager.Instance.PlayEvent("EventAlarm");
                EventPenal.SetActive(true);
                GameManager.Instance.PopUpBackGroundOn();
                GameManager.Instance.NoPressNextDayButton();

                OneEventLine(info);
            }
        }
        // 그게 아니라면 일반 이벤트이니 계속 게임의 날짜와 현재 보고있는 이벤트 정보가 들어있는 리스트를 확인해준다.
        else if (Check_OpenDate(GameManager.Instance.GetOpenDate(), info) == true)
        {
            if (Check_OpenType1(info) == false || Check_OpenType2(info) == false)
            {
                return;
            }
            else if (Check_OpenType1(info) == true && Check_OpenType2(info) == true)
            {
                isEvent = false;

                if (!saveEventID.ContainsKey(info.ID))
                {
                    saveEventID.Add(info.ID, "완료");
                }

                SoundManager.Instance.PlayEvent("EventAlarm");
                EventPenal.SetActive(true);
                GameManager.Instance.PopUpBackGroundOn();
                GameManager.Instance.NoPressNextDayButton();

                OneEventLine(info);
            }
        }
        // 위에 조건들을 모두 충족하지 못했다면 나가서 다시 조건이 충족되기 전까지는 인덱스를 증가시키지 않는다.
        // 그러다 하루가 지나 게임상의 날짜가 하루 지나면 이벤트 정보다 담긴 리스트의 인덱스를
        // 게임상의 날짜랑 같은 날짜가 나올 때까지 증가시켜준다.
        else
        {
            return;
        }
    }

    // 파싱한 json파일을 한 줄씩 처리해주기 위한 함수
    private void OneEventLine(EventInfo info)
    {
        // 버튼을 이미 생성한 상태라면 패스
        if (EventButton1Exist == true ||
            EventButton2Exist == true ||
            EventButton3Exist == true)
        {
            return;
        }

        SendEventText(info.EventText);
        // 조건이 맞는 이벤트라면
        // UI에 출력도 하고 이것저것을 한다.

        // 만약 selectiontext1이 있으면 
        if (info.SelectionText1.Length != 0)
        {
            SendEventSelectionText1(info.SelectionText1);

            if (info.NextEvent1 != "")
            {
                nextEvent1ID.Add(info.NextEvent1);
            }
        }

        if (info.SelectionText2.Length != 0)
        {
            SendEventSelectionText2(info.SelectionText2);

            if (info.NextEvent2 != "null")
            {
                nextEvent2ID.Add(info.NextEvent2);
            }
        }

        if (info.SelectionText3.Length != 0)
        {
            SendEventSelectionText2(info.SelectionText3);

            if (info.NextEvent3 != "null")
            {
                nextEvent3ID.Add(info.NextEvent3);
            }
        }

        //info.isEventChecked = true;
        EventTitle.text = info.EventTitle;

        // 버튼이 한 개라도 있으면
        if (info.SelectionText1.Length != 0
            || info.SelectionText2.Length != 0
            || info.SelectionText3.Length != 0)
        {
            return;
        }
        // 버튼이 한 개도 없다면
        else
        {
            /// TODO:OCEAN 코루틴으로 딜레이를 주고 한다.

            NowEventIndex++;
        }

    }

    /// <summary>
    /// 3가지 조건이 모두 없다면(0) 이것은 조건체크를 하지 않는 데이터이다.
    /// Event Text의 다음줄을 읽어오기 위한 조건문
    /// 
    /// </summary>
    public bool Check_NextEvent(EventInfo info)
    {
        if (info.OpenDate == 0
            && info.OpenType1 == 0
            && info.OpenType2 == 0)
        {
            return true;
        }

        return false;
    }

    public bool Check_OpenDate(int openDateNum, EventInfo info)
    {
        if (info.OpenDate == openDateNum)
        {
            return true;
        }

        return false;
    }

    public bool Check_OpenType1(EventInfo info)
    {
        bool isOpenType1 = false;

        switch (info.OpenType1)
        {
            case 1: // 다음날로 이동 버튼을 누르고 저녁이 되었을 때 이벤트 등장
            {
                if (DayAndNight.dayandNight.isNight)
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 2: // OpenDate날 포획을 완료했을 때 이벤트 등장
            {
                if (info.OpenDate == int.Parse(GameManager.Instance.UIToday[1].text) && GameManager.Instance.isTodayCapture)
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 3: // OpenDate날 쿠키제조를 완료했을 때 이벤트 등장
            {
                if (info.OpenDate == int.Parse(GameManager.Instance.UIToday[1].text) && GameManager.Instance.isTodayMakeCookie)
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 4: // 해당 ID의 이벤트가 완료가 되었을 때 이벤트 등장
            {
                if (info.Value1 != "")
                {
                    // 만약 vlaue값이 여러개이면 ,로 문자열을 잘라준다
                    bool isComma = info.Value1.Contains(",");

                    if (isComma)
                    {
                        string[] newValue = info.Value1.Split(',');
                        int newValueSize = newValue.Length;

                        // 문자열 길이에 따라 조건문 처리를 다르게 해줘야해서 사용
                        switch (newValueSize)
                        {
                            case 2:
                            {
                                if (saveEventID.ContainsKey(newValue[0]) && saveEventID.ContainsKey(newValue[1]))
                                {
                                    isOpenType1 = true;
                                }
                            }
                            break;
                            case 3:
                            {
                                if (saveEventID.ContainsKey(newValue[0]) && saveEventID.ContainsKey(newValue[1]) && saveEventID.ContainsKey(newValue[2]))
                                {
                                    isOpenType1 = true;
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (saveEventID.ContainsKey(info.Value1))
                        {
                            isOpenType1 = true;
                        }
                    }
                }

            }
            break;
            case 5: // 플레이어가 소지하고 있는 현금이 value 이하일 때 이벤트 발생
            {
                if (int.Parse(GameManager.Instance.Coin.text) <= int.Parse(info.Value1))
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 6:  // 플레이어가 소지하고 있는 현금이 value 이상일 때 이벤트 발생
            {
                if (int.Parse(GameManager.Instance.Coin.text) >= int.Parse(info.Value1))
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 7: // 플레이어가 보유하고 있는 인령수가 value 이하일 때 이벤트 발생
            {
                if (GameManager.Instance.Dollnum <= int.Parse(info.Value1))
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 8: // 플레이어가 보유하고 있는 인령수가 value 이상일 때 이벤트 발생
            {
                if (GameManager.Instance.Dollnum >= int.Parse(info.Value1))
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 9: // 플레이어가 현재 소지하고 있는 인형중에 value의 ID가 있으면 이벤트 발생
            {
                // value에 들어있는 ID가 딕셔너리에 있는지 확인한다.
                if (info.Value1 != "")
                {
                    // 만약 vlaue값이 여러개이면 ,로 문자열을 잘라준다
                    bool isComma = info.Value1.Contains(",");

                    if (isComma)
                    {
                        string[] newValue = info.Value1.Split(',');
                        int newValueSize = newValue.Length;

                        // 문자열 길이에 따라 조건문 처리를 다르게 해줘야해서 사용
                        switch (newValueSize)
                        {
                            case 2:
                            {
                                if (saveDollID.ContainsValue(newValue[0]) && saveDollID.ContainsValue(newValue[1]))
                                {
                                    isOpenType1 = true;
                                }
                            }
                            break;
                            case 3:
                            {
                                if (saveDollID.ContainsValue(newValue[0]) && saveDollID.ContainsValue(newValue[1]) && saveDollID.ContainsValue(newValue[2]))
                                {
                                    isOpenType1 = true;
                                }
                            }
                            break;
                            case 4:
                            {
                                if (saveDollID.ContainsValue(newValue[0]) && saveDollID.ContainsValue(newValue[1]) && saveDollID.ContainsValue(newValue[2]) && saveDollID.ContainsValue(newValue[3]))
                                {
                                    isOpenType1 = true;
                                }
                            }
                            break;
                            case 5:
                            {
                                if (saveDollID.ContainsValue(newValue[0]) && saveDollID.ContainsValue(newValue[1]) && saveDollID.ContainsValue(newValue[2]) && saveDollID.ContainsValue(newValue[3]) && saveDollID.ContainsValue(newValue[4]))
                                {
                                    isOpenType1 = true;
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (saveDollID.ContainsValue(info.Value1))
                        {
                            isOpenType1 = true;
                        }
                    }
                }
            }
            break;

            case 10: // value ID의 이벤트가 완료되지 않았을 때 이벤트 발생
            {
                //if (!saveEventID.ContainsKey(info.Value1))
                //{
                //    isOpenType1 = true;
                //}

                if (info.Value1 != "")
                {
                    // 만약 vlaue값이 여러개이면 ,로 문자열을 잘라준다
                    bool isComma = info.Value1.Contains(",");

                    if (isComma)
                    {
                        string[] newValue = info.Value1.Split(',');
                        int newValueSize = newValue.Length;

                        // 문자열 길이에 따라 조건문 처리를 다르게 해줘야해서 사용
                        switch (newValueSize)
                        {
                            case 2:
                            {
                                if (!saveEventID.ContainsKey(newValue[0]) && !saveEventID.ContainsKey(newValue[1]))
                                {
                                    isOpenType1 = true;
                                }
                            }
                            break;

                        }
                    }
                    else
                    {
                        if (saveEventID.ContainsKey(info.Value1))
                        {
                            isOpenType1 = true;
                        }
                    }
                }
            }
            break;

            case 11: // value ID의 인형이 없을 때 이벤트 발생
            {
                List<string> dollID = new List<string>();

                // 새로운 리스트를 만들어주고 거기에 현재 소지하고있는 인형의 정보를 넣어준 후 
                for (int i = 0; i < 9; i++)
                {
                    dollID.Insert(i, GameManager.Instance.dollInfo[i].ID);
                }

                if (info.OpenDate == int.Parse(GameManager.Instance.UIToday[1].text) && !saveDollID.ContainsValue(info.Value1))
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 0:
            default:
            {
                isOpenType1 = true;
            }
            break;
        }

        return isOpenType1;
    }

    public bool Check_OpenType2(EventInfo info)
    {
        bool isOpenType2 = false;

        switch (info.OpenType2)
        {
            case 1: // 다음날로 이동 버튼을 누르고 저녁이 되었을 때 이벤트 등장
            {
                if (DayAndNight.dayandNight.isNight)
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 2: // OpenDate날 포획을 완료했을 때 이벤트 등장
            {
                if (info.OpenDate == int.Parse(GameManager.Instance.UIToday[1].text) && GameManager.Instance.isTodayCapture)
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 3: // OpenDate날 쿠키제조를 완료했을 때 이벤트 등장
            {
                if (info.OpenDate == int.Parse(GameManager.Instance.UIToday[1].text) && GameManager.Instance.isTodayMakeCookie)
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 4: // 해당 ID의 이벤트가 완료가 되었을 때 이벤트 등장
            {
                if (info.Value2 != "")
                {
                    bool isComma = info.Value2.Contains(",");

                    if (isComma)
                    {
                        string[] newValue = info.Value2.Split(',');
                        int newValueSize = newValue.Length;

                        // 문자열 길이에 따라 조건문 처리를 다르게 해줘야해서 사용
                        switch (newValueSize)
                        {
                            case 2:
                            {
                                if (saveEventID.ContainsKey(newValue[0]) && saveEventID.ContainsKey(newValue[1]))
                                {
                                    isOpenType2 = true;
                                }
                            }
                            break;
                            case 3:
                            {
                                if (saveEventID.ContainsKey(newValue[0]) && saveEventID.ContainsKey(newValue[1]) && saveEventID.ContainsKey(newValue[2]))
                                {
                                    isOpenType2 = true;
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (saveEventID.ContainsKey(info.Value2))
                        {
                            isOpenType2 = true;
                        }
                    }
                }
            }
            break;

            case 5: // 플레이어가 소지하고 있는 현금이 value 이하일 때 이벤트 발생
            {
                if (int.Parse(GameManager.Instance.Coin.text) <= int.Parse(info.Value2))
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 6:  // 플레이어가 소지하고 있는 현금이 value 이상일 때 이벤트 발생
            {
                if (int.Parse(GameManager.Instance.Coin.text) >= int.Parse(info.Value2))
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 7: // 플레이어가 보유하고 있는 인령수가 value 이하일 때 이벤트 발생
            {
                if (GameManager.Instance.Dollnum <= int.Parse(info.Value2))
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 8: // 플레이어가 보유하고 있는 인령수가 value 이상일 때 이벤트 발생
            {
                if (GameManager.Instance.Dollnum >= int.Parse(info.Value2))
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 9: // 플레이어가 현재 소지하고 있는 인형중에 value의 ID가 있으면 이벤트 발생
            {
                // value에 들어있는 ID가 딕셔너리에 있는지 확인한다.
                if (info.Value2 != "")
                {
                    // 만약 vlaue값이 여러개이면 ,로 문자열을 잘라준다
                    bool isComma = info.Value2.Contains(",");

                    if (isComma)
                    {
                        string[] newValue = info.Value2.Split(',');
                        int newValueSize = newValue.Length;

                        // 문자열 길이에 따라 조건문 처리를 다르게 해줘야해서 사용
                        switch (newValueSize)
                        {
                            case 2:
                            {
                                if (saveDollID.ContainsValue(newValue[0]) && saveDollID.ContainsValue(newValue[1]))
                                {
                                    isOpenType2 = true;
                                }
                            }
                            break;
                            case 3:
                            {
                                if (saveDollID.ContainsValue(newValue[0]) && saveDollID.ContainsValue(newValue[1]) && saveDollID.ContainsValue(newValue[2]))
                                {
                                    isOpenType2 = true;
                                }
                            }
                            break;
                            case 4:
                            {
                                if (saveDollID.ContainsValue(newValue[0]) && saveDollID.ContainsValue(newValue[1]) && saveDollID.ContainsValue(newValue[2]) && saveDollID.ContainsValue(newValue[3]))
                                {
                                    isOpenType2 = true;
                                }
                            }
                            break;
                            case 5:
                            {
                                if (saveDollID.ContainsValue(newValue[0]) && saveDollID.ContainsValue(newValue[1]) && saveDollID.ContainsValue(newValue[2]) && saveDollID.ContainsValue(newValue[3]) && saveDollID.ContainsValue(newValue[4]))
                                {
                                    isOpenType2 = true;
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (saveDollID.ContainsValue(info.Value2))
                        {
                            isOpenType2 = true;
                        }
                    }
                }
            }
            break;

            case 10: // value ID의 이벤트가 완료되지 않았을 때 이벤트 발생
            {
                bool isComma = info.Value2.Contains(",");

                if (isComma)
                {
                    string[] newValue = info.Value2.Split(',');
                    int newValueSize = newValue.Length;

                    // 문자열 길이에 따라 조건문 처리를 다르게 해줘야해서 사용
                    switch (newValueSize)
                    {
                        case 2:
                        {
                            if (!saveEventID.ContainsKey(newValue[0]) && !saveEventID.ContainsKey(newValue[1]))
                            {
                                isOpenType2 = true;
                            }
                        }
                        break;

                    }
                }
                else
                {
                    if (saveEventID.ContainsKey(info.Value2))
                    {
                        isOpenType2 = true;
                    }
                }

            }
            break;

            case 11: // value ID의 인형이 없을 때 이벤트 발생
            {

                if (info.OpenDate == int.Parse(GameManager.Instance.UIToday[1].text) && !saveDollID.ContainsValue(info.Value2))
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 0:
            default:
            {
                isOpenType2 = true;
            }
            break;
        }

        return isOpenType2;
    }

    public void Check_Result1(EventInfo info)
    {
        switch (info.Result1)
        {
            case 1:
            {
                GameManager.Instance.CurrentCookies.text = (int.Parse(GameManager.Instance.CurrentCookies.text) + int.Parse(info.ResultValue1)).ToString();
                SoundManager.Instance.PlaySE("CookieChange");
            }
            break;

            case 3:
            {
                GameManager.Instance.Coin.text = (int.Parse(GameManager.Instance.Coin.text) + int.Parse(info.ResultValue1)).ToString();
                SoundManager.Instance.PlaySE("CoinChange");
            }
            break;

            case 5:
            {
                // 모든 인형을 지워줘야해서 관련된 데이터들을 초기화 시켜준다.
                GameManager.Instance.Doll.text = "0";
                SoundManager.Instance.PlaySE("DollChange");

                for (int i = 0; i < 9; i++)
                {
                    if (GameManager.Instance.dollInfo[i] != null)
                    {
                        GameManager.Instance.dollInfo[i] = null;
                        dollID.Clear();
                        saveDollID.Clear();
                        GameManager.Instance.Selected[i].SetActive(false);
                        GameManager.Instance.DollsImage[i].SetActive(false);
                    }

                    if (GameManager.Instance.CookieUI[i].text != "")
                    {
                        GameManager.Instance.CookieUI[i].text = "";
                    }

                }
                GameManager.Instance.SelectedDolls.Clear();
                GameManager.Instance.SelectedUI.Clear();
                GameManager.Instance.Check_Dolls.Clear();
            }
            break;

            case 6:
            {
                int ResultValue_DollID;

                /// 해당 아이디를 가진 인형이 진열대 어디에 있는지 확인하고 해당 진열대를 비워주기 위해 처리 중
                if (dollID.Contains(info.ResultValue1))
                {
                    ResultValue_DollID = saveDollID.FirstOrDefault(x => x.Value == info.ResultValue1).Key;

                    GameManager.Instance.CookieUI[ResultValue_DollID].text = "";
                    GameManager.Instance.DollText_text[ResultValue_DollID].text = "";

                    GameManager.Instance.dollInfo[ResultValue_DollID] = null;
                    GameManager.Instance.SelectedDolls.Remove(GameManager.Instance.CookieUI[ResultValue_DollID]);
                    GameManager.Instance.Selected[ResultValue_DollID].SetActive(false);
                    GameManager.Instance.DollsImage[ResultValue_DollID].SetActive(false);

                    GameManager.Instance.Doll.text = (int.Parse(GameManager.Instance.Doll.text) - 1).ToString();
                    GameManager.Instance.CurrentDoll.text = (int.Parse(GameManager.Instance.CurrentDoll.text) - 1).ToString();
                    saveDollID.Remove(ResultValue_DollID);
                    dollID.Remove(info.ResultValue1);
                    SoundManager.Instance.PlaySE("DollChange");
                }
                else
                {
                    return;
                }
            }
            break;

            case 7:
            {
                if (GameManager.Instance.isTodayCapture == false)
                {
                    GameManager.Instance.isTodayCapture = true;
                }
            }
            break;

            case 8:
            {
                if (GameManager.Instance.isTodayMakeCookie == false)
                {
                    GameManager.Instance.isTodayMakeCookie = true;
                }
            }
            break;

            case 9:
            {
                isGameOver = true;
                GameManager.Instance.HideUI.SetActive(false);
                SoundManager.Instance.StopBGM("MainBGM");
                SoundManager.Instance.StopAllSE();
                if (info.ResultValue1 == "EndingScene4" || info.ResultValue1 == "EndingScene3" || info.ResultValue1 == "EndingScene2")
                {
                    SoundManager.Instance.PlayBGM("NormalEnding_BGM");
                }
                else
                {
                    SoundManager.Instance.PlayBGM("GoodEndingBGM");
                }
                SceneManager.LoadScene(info.ResultValue1);
            }
            break;

            case 0:
            default:
            {

            }
            break;
        }
    }

    public void Check_Result2(EventInfo info)
    {
        switch (info.Result2)
        {
            case 1:
            {
                GameManager.Instance.CurrentCookies.text = (int.Parse(GameManager.Instance.CurrentCookies.text) + info.ResultValue2).ToString();
                SoundManager.Instance.PlaySE("CookieChange");
            }
            break;

            case 3:
            {
                GameManager.Instance.Coin.text = (int.Parse(GameManager.Instance.Coin.text) + info.ResultValue2).ToString();
                SoundManager.Instance.PlaySE("CoinChange");
            }
            break;

            case 5:
            {
                GameManager.Instance.Doll.text = "0";
                SoundManager.Instance.PlaySE("DollChange");
                for (int i = 0; i < 9; i++)
                {
                    if (GameManager.Instance.dollInfo[i] != null)
                    {
                        GameManager.Instance.dollInfo[i] = null;
                        dollID.Clear();
                        saveDollID.Clear();
                        GameManager.Instance.Selected[i].SetActive(false);
                        GameManager.Instance.DollsImage[i].SetActive(false);
                    }

                    if (GameManager.Instance.CookieUI[i].text != "")
                    {
                        GameManager.Instance.CookieUI[i].text = "";
                    }

                }

                GameManager.Instance.SelectedDolls.Clear();
                GameManager.Instance.SelectedUI.Clear();
                GameManager.Instance.Check_Dolls.Clear();
            }
            break;
            #region _아직까지는 Result2에서 6번을 사용하지 않는다.
            //case 6:
            //{
            //    int ResultValue_DollID;

            //    /// 해당 아이디를 가진 인형이 진열대 어디에 있는지 확인하고 해당 진열대를 비워주기 위해 처리 중
            //    if (dollID.Contains(info.ResultValue2))
            //    {
            //        saveDollID.TryGetValue(info.ResultValue1, out ResultValue_DollID);

            //        GameManager.Instance.CookieUI[ResultValue_DollID].text = "";
            //        GameManager.Instance.DollText_text[ResultValue_DollID].text = "";

            //        GameManager.Instance.dollInfo[ResultValue_DollID] = null;
            //        GameManager.Instance.SelectedDolls.Remove(GameManager.Instance.CookieUI[ResultValue_DollID]);
            //        GameManager.Instance.Selected[ResultValue_DollID].SetActive(false);
            //        GameManager.Instance.DollsImage[ResultValue_DollID].SetActive(false);
            //    }
            //    saveDollID.Remove(info.ResultValue2);

            //}
            //break;
            #endregion

            case 7:
            {
                if (GameManager.Instance.isTodayCapture == false)
                {
                    GameManager.Instance.isTodayCapture = true;
                }
            }
            break;

            case 8:
            {
                if (GameManager.Instance.isTodayMakeCookie == false)
                {
                    GameManager.Instance.isTodayMakeCookie = true;
                }
            }
            break;

            case 9:
            {
                isGameOver = true;
                GameManager.Instance.HideUI.SetActive(false);
                SoundManager.Instance.StopBGM("MainBGM");
                SoundManager.Instance.StopAllSE();
                if ((info.ResultValue2).ToString() == "EndingScene4" || (info.ResultValue2).ToString() == "EndingScene3" || (info.ResultValue2).ToString() == "EndingScene2")
                {
                    SoundManager.Instance.PlayBGM("NormalEnding_BGM");
                }
                else
                {
                    SoundManager.Instance.PlayBGM("GoodEndingBGM");
                }
                SceneManager.LoadScene((info.ResultValue2).ToString());
            }
            break;

            case 0:
            default:
            {

            }
            break;
        }
    }

    public void Check_Result3(EventInfo info)
    {
        switch (info.Result3)
        {
            case 1:
            {
                GameManager.Instance.CurrentCookies.text = (int.Parse(GameManager.Instance.CurrentCookies.text) + info.ResultValue3).ToString();
                SoundManager.Instance.PlaySE("CookieChange");
            }
            break;

            case 3:
            {
                GameManager.Instance.Coin.text = (int.Parse(GameManager.Instance.Coin.text) + info.ResultValue3).ToString();
                SoundManager.Instance.PlaySE("CoinChange");
            }
            break;

            case 5:
            {
                GameManager.Instance.Doll.text = "0";
                SoundManager.Instance.PlaySE("DollChange");

                for (int i = 0; i < 9; i++)
                {
                    if (GameManager.Instance.dollInfo[i] != null)
                    {
                        GameManager.Instance.dollInfo[i] = null;
                        dollID.Clear();
                        saveDollID.Clear();
                        GameManager.Instance.Selected[i].SetActive(false);
                        GameManager.Instance.DollsImage[i].SetActive(false);
                    }

                    if (GameManager.Instance.CookieUI[i].text != "")
                    {
                        GameManager.Instance.CookieUI[i].text = "";
                    }

                }

                GameManager.Instance.SelectedDolls.Clear();
                GameManager.Instance.SelectedUI.Clear();
                GameManager.Instance.Check_Dolls.Clear();
            }
            break;

            case 6:
            {
                int ResultValue_DollID;

                /// 해당 아이디를 가진 인형이 진열대 어디에 있는지 확인하고 해당 진열대를 비워주기 위해 처리 중
                if (dollID.Contains(info.ResultValue3.ToString()))
                {
                    ResultValue_DollID = saveDollID.FirstOrDefault(x => x.Value == info.ResultValue3.ToString()).Key;

                    GameManager.Instance.CookieUI[ResultValue_DollID].text = "";
                    GameManager.Instance.DollText_text[ResultValue_DollID].text = "";

                    GameManager.Instance.dollInfo[ResultValue_DollID] = null;
                    GameManager.Instance.SelectedDolls.Remove(GameManager.Instance.CookieUI[ResultValue_DollID]);
                    GameManager.Instance.Selected[ResultValue_DollID].SetActive(false);
                    GameManager.Instance.DollsImage[ResultValue_DollID].SetActive(false);

                    GameManager.Instance.Doll.text = (int.Parse(GameManager.Instance.Doll.text) - 1).ToString();
                    GameManager.Instance.CurrentDoll.text = (int.Parse(GameManager.Instance.CurrentDoll.text) - 1).ToString();
                    saveDollID.Remove(ResultValue_DollID);
                    dollID.Remove(info.ResultValue3.ToString());

                    SoundManager.Instance.PlaySE("DollChange");
                }
                else
                {
                    return;
                }
            }
            break;

            case 7:
            {
                if (GameManager.Instance.isTodayCapture == false)
                {
                    GameManager.Instance.isTodayCapture = true;
                }
            }
            break;

            case 8:
            {
                if (GameManager.Instance.isTodayMakeCookie == false)
                {
                    GameManager.Instance.isTodayMakeCookie = true;
                }
            }
            break;

            case 9:
            {
                isGameOver = true;
                GameManager.Instance.HideUI.SetActive(false);
                SoundManager.Instance.StopBGM("MainBGM");
                SoundManager.Instance.StopAllSE();
                if ((info.ResultValue3).ToString() == "EndingScene4" || (info.ResultValue3).ToString() == "EndingScene3" || (info.ResultValue3).ToString() == "EndingScene2")
                {
                    SoundManager.Instance.PlayBGM("NormalEnding_BGM");
                }
                else
                {
                    SoundManager.Instance.PlayBGM("GoodEndingBGM");
                }
                SceneManager.LoadScene((info.ResultValue3).ToString());
            }
            break;

            case 0:
            default:
            {

            }
            break;
        }
    }

    // 선택지의 결과값이 모두 없다면 다음 이벤트를 찾기 위해 인덱스값을 늘려준다. 
    // NextEvent를 건너뛰기 위해 이벤트 아이디가 4문자인 ID를 찾는다.
    private void FindEventID()
    {
        bool _findNextEventID = false;

        while (_findNextEventID == false)
        {
            NowEventIndex++;
            EventInfo _nextInfo = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextInfo.ID.Length == 4)
            {
                _findNextEventID = true;
            }
        }
    }

    // 이전 이벤트를 하지 않고 날짜를 바꿀 때 다음날에 해당하는 이벤트 인덱스로 넘겨주기 위한 함수
    public void FindNextOpenDate()
    {
        bool _findEventOpenDate = false;
        int i = 0;

        while (_findEventOpenDate == false)
        {
            EventInfo _nextOpenDateInfo = ParsingEvent.EventInfoList[NowEventIndex + i];

            if (_nextOpenDateInfo.OpenDate == GameManager.Instance.GetOpenDate())
            {
                _findEventOpenDate = true;
                NowEventIndex += i;
            }
            else
            {
                i++;
            }

        }
    }

    // 첫날에 인형 포획 후 쿠키 제조를 하지않고 다음날로 넘어갈 때의 예외처리
    public void UntilTheOpenType1ToFirstDay()
    {
        bool _findNightEvent = false;

        while (_findNightEvent == false)
        {
            EventInfo _nextEvent = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextEvent.OpenType1 == 1)
            {
                _findNightEvent = true;
            }
            else
            {
                NowEventIndex++;
            }
        }
    }

    // selectiontext에 nextevent가 있다면 해당 아이디랑 이벤트 아이디가 같아질 때 까지 인덱스를 늘려준다.
    public void FindNextEvent1ID()
    {
        bool _findEventID = false;

        while (_findEventID == false)
        {
            EventInfo _nextEventIDInfo = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextEventIDInfo.ID == nextEvent1ID[0])
            {
                _findEventID = true;
            }
            else
            {
                NowEventIndex++;
            }
        }
    }

    public void UntilTheOpenDate11()
    {
        bool _findOpenDateID = false;

        while (_findOpenDateID == false)
        {
            EventInfo _nextEventIDInfo = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextEventIDInfo.OpenDate == 11)
            {
                _findOpenDateID = true;
            }
            else
            {
                NowEventIndex++;
            }
        }
    }

    public void UntilTheOpenDate14()
    {
        bool _findOpenDateID = false;

        while (_findOpenDateID == false)
        {
            EventInfo _nextEventIDInfo = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextEventIDInfo.OpenDate == 14)
            {
                _findOpenDateID = true;
            }
            else
            {
                NowEventIndex++;
            }
        }
    }

    public void UntilTheOpenDate16()
    {
        bool _findOpenDateID = false;

        while (_findOpenDateID == false)
        {
            EventInfo _nextEventIDInfo = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextEventIDInfo.OpenDate == 16)
            {
                _findOpenDateID = true;
            }
            else
            {
                NowEventIndex++;
            }
        }
    }

    public void UntilTheOpenDate17()
    {
        bool _findOpenDateID = false;

        while (_findOpenDateID == false)
        {
            EventInfo _nextEventIDInfo = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextEventIDInfo.OpenDate == 17)
            {
                _findOpenDateID = true;
            }
            else
            {
                NowEventIndex++;
            }
        }
    }

    public void UntilTheOpenDate18()
    {
        bool _findOpenDateID = false;

        while (_findOpenDateID == false)
        {
            EventInfo _nextEventIDInfo = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextEventIDInfo.OpenDate == 18)
            {
                _findOpenDateID = true;
            }
            else
            {
                NowEventIndex++;
            }
        }
    }

    public void UntilTheOpenDate19()
    {
        bool _findOpenDateID = false;

        while (_findOpenDateID == false)
        {
            EventInfo _nextEventIDInfo = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextEventIDInfo.OpenDate == 19)
            {
                _findOpenDateID = true;
            }
            else
            {
                NowEventIndex++;
            }
        }
    }

    public void FindNextEvent2ID()
    {
        bool _findEventID = false;

        while (_findEventID == false)
        {
            EventInfo _nextEventIDInfo = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextEventIDInfo.ID == nextEvent2ID[0])
            {
                _findEventID = true;
            }
            else
            {
                NowEventIndex++;
            }
        }

    }

    public void FindNextEvent3ID()
    {
        bool _findEventID = false;

        while (_findEventID == false)
        {
            EventInfo _nextEventIDInfo = ParsingEvent.EventInfoList[NowEventIndex];

            if (_nextEventIDInfo.ID == nextEvent3ID[0])
            {
                _findEventID = true;
            }
            else
            {
                NowEventIndex++;
            }
        }

    }

    // 밤이 됐을 때 진행되는 이벤트 처리를 하기위한 함수
    public void FindNightEvent()
    {
        // 현재 이벤트 리스트에서 한칸 증가한 이벤트의 opendDate가 현재 인게임의 날짜와 다르면 다시 낮으로 바꿔주고 이벤트를 종료시켜준다.
        if (ParsingEvent.EventInfoList[NowEventIndex + 1].ID != "" && ParsingEvent.EventInfoList[NowEventIndex + 1].OpenDate != 0
            && ParsingEvent.EventInfoList[NowEventIndex + 1].OpenDate != int.Parse(GameManager.Instance.UIToday[1].text))
        {
            if (GameManager.Instance.GetOpenDate() == 19)
            {
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                NowEventIndex++;
            }
            else
            {
                DayAndNight.dayandNight.isDay = false;
                DayAndNight.dayandNight.isNight = false;
                DayAndNight.dayandNight.isStart = true;
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                NowEventIndex++;
            }
        }
        else
        {
            // 19일엔 아침이 되는거 막아주기
            if (GameManager.Instance.GetOpenDate() == 19)
            {
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                FindEventID();
            }
            else
            {
                DayAndNight.dayandNight.isDay = false;
                DayAndNight.dayandNight.isNight = false;
                DayAndNight.dayandNight.isStart = true;
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                FindEventID();

            }
        }
    }

    public void PlayNextEventSound()
    {
        switch (ParsingEvent.EventInfoList[NowEventIndex].ID)
        {
            case "M4051":
            {
                SoundManager.Instance.PlayEvent("AngryCrowd");
            }
            break;

            case "M4071":
            {
                SoundManager.Instance.PlayEvent("AngryCrowd");
            }
            break;

            case "M4032":
            {
                SoundManager.Instance.PlayDelaySound("Attacked");
            }
            break;

            case "M4057":
            {
                SoundManager.Instance.PlayDelaySound("CookieUnderTheFeet");
            }
            break;

            case "M401":
            {
                SoundManager.Instance.PlayBGM("FinalEvent");
            }
            break;

            case "M4076":
            {
                SoundManager.Instance.PlayDelaySound("HandGunShooting");

            }
            break;

            case "M4053":
            {
                SoundManager.Instance.PlayDelaySound("Reloading");

            }
            break;

            case "M4073":
            {
                SoundManager.Instance.PlayDelaySound("Reloading");

            }
            break;

            case "M4065":
            {
                SoundManager.Instance.PlayEvent("RunFromChasers");

            }
            break;

            case "M4075":
            {
                SoundManager.Instance.PlayEvent("Shooting");

            }
            break;

            case "M4056":
            {
                SoundManager.Instance.PlayDelaySound("WalkToOutSide");

            }
            break;
        }

    }

    public void SelectionText1()
    {
        // Result가 있다면...
        if (ParsingEvent.EventInfoList[NowEventIndex].Result1 != 0)
        {
            Check_Result1(ParsingEvent.EventInfoList[NowEventIndex]);

            if (ParsingEvent.EventInfoList[NowEventIndex].NextEvent1 == "" && GameManager.Instance.GetOpenDate() >= 11)
            {
                if (DayAndNight.dayandNight.isNight == true)
                {
                    DayAndNight.dayandNight.isNight = false;
                    DayAndNight.dayandNight.isDay = true;
                    DayAndNight.dayandNight.isStart = false;
                }
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                FindEventID();
            }
            else if (ParsingEvent.EventInfoList[NowEventIndex].NextEvent1 != "")
            {
                FindNextEvent1ID();
            }
            else if (DayAndNight.dayandNight.isDay == true)
            {
                FindNightEvent();
            }
            else
            {
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                NowEventIndex++;
            }

        }
        else if (ParsingEvent.EventInfoList[NowEventIndex].NextEvent1 != "")
        {

            // 그렇다면 다음 이벤트가 있다는 뜻이다.
            //NowEventIndex++;
            FindNextEvent1ID();
        }
        else
        {
            // 둘 다 없으면 이벤트 종료
            if (DayAndNight.dayandNight.isDay == true)
            {
                FindNightEvent();
            }
            else
            {
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                FindEventID();
            }
        }
        sendTime = 2;
        ResetButtonStateAll();
    }

    public void SelectionText2()
    {
        // Result가 없다면...
        if (ParsingEvent.EventInfoList[NowEventIndex].Result2 != 0)
        {
            Check_Result2(ParsingEvent.EventInfoList[NowEventIndex]);

            if (ParsingEvent.EventInfoList[NowEventIndex].NextEvent1 == "" && GameManager.Instance.GetOpenDate() >= 11)
            {
                if (DayAndNight.dayandNight.isNight == true)
                {
                    DayAndNight.dayandNight.isNight = false;
                    DayAndNight.dayandNight.isDay = true;
                    DayAndNight.dayandNight.isStart = false;
                }

                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                FindEventID();
            }
            else if (ParsingEvent.EventInfoList[NowEventIndex].NextEvent2 != "null")
            {
                FindNextEvent2ID();
            }
            else if (DayAndNight.dayandNight.isDay == true)
            {
                FindNightEvent();
            }
            else
            {
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                NowEventIndex++;
            }
        }
        else if (ParsingEvent.EventInfoList[NowEventIndex].NextEvent2 != "null")
        {
            // 그렇다면 다음 이벤트가 있다는 뜻이다.
            //NowEventIndex++;
            FindNextEvent2ID();
        }
        else
        {
            // 둘 다 없으면 이벤트 종료
            if (DayAndNight.dayandNight.isDay == true)
            {
                FindNightEvent();
            }
            else
            {
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                FindEventID();
            }
        }
        sendTime = 2;
        ResetButtonStateAll();
    }

    public void SelectionText3()
    {
        // Result가 있다면...
        if (ParsingEvent.EventInfoList[NowEventIndex].Result3 != 0)
        {
            Check_Result3(ParsingEvent.EventInfoList[NowEventIndex]);

            if (ParsingEvent.EventInfoList[NowEventIndex].NextEvent1 == "" && GameManager.Instance.GetOpenDate() >= 11)
            {
                if (DayAndNight.dayandNight.isNight == true)
                {
                    DayAndNight.dayandNight.isNight = false;
                    DayAndNight.dayandNight.isDay = true;
                    DayAndNight.dayandNight.isStart = false;
                }

                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                FindEventID();
            }
            else if (ParsingEvent.EventInfoList[NowEventIndex].NextEvent3 != "null")
            {
                FindNextEvent3ID();
            }
            else if (DayAndNight.dayandNight.isDay == true)
            {
                FindNightEvent();
            }
            else
            {
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                NowEventIndex++;
            }
        }
        else if (ParsingEvent.EventInfoList[NowEventIndex].NextEvent3 != "null")
        {
            // 그렇다면 다음 이벤트가 있다는 뜻이다.
            //NowEventIndex++;
            FindNextEvent3ID();
        }
        else
        {
            // 둘 다 없으면 이벤트 종료
            if (DayAndNight.dayandNight.isDay == true)
            {
                FindNightEvent();
            }
            else
            {
                isEvent = true;
                EventPenal.SetActive(false);
                DestroyedEventText();
                FindEventID();
            }
        }
        sendTime = 2;       // 버튼을 누르면 다음 이벤트 텍스트가 출력할 수 있게 해주기 위해 
        ResetButtonStateAll();
    }

    private void ResetButtonStateAll()
    {
        EventButton1Exist = false;
        EventButton2Exist = false;
        EventButton3Exist = false;

        // 버튼을 클릭하고 나면 해당 선택지의 다음 이벤트 아이디를 저장하는 리스트를 비워준다
        // 다음 이벤트가 연달아 나와서 또 다시 선택지를 선택해야 할 수 있기 때문
        nextEvent1ID.Clear();
        nextEvent2ID.Clear();
        nextEvent3ID.Clear();
    }

    // 이벤트 하나가 끝날때 다음 이벤트가 발생했을 때 대화가 남지 않게 해주기 위해 자식 오브젝트들을 삭제해준다.
    private void DestroyedEventText()
    {
        Transform[] eventText = eventContent.GetComponentsInChildren<Transform>();

        if (eventText != null)
        {
            for (int i = 1; i < eventText.Length; i++)
            {
                if (eventText[i] != transform)
                {
                    Destroy(eventText[i].gameObject);
                }
            }
        }

        GameManager.Instance.PressNextDayButton();
        GameManager.Instance.PopUpBackGroundOff();
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Image eventMessageBox;
    public Button selectionEventMessageBox;
}
