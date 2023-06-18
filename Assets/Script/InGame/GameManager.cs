using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 다른 스크립트가 붙어있는 오브젝트마다 GetComponent해주기 힘드니까
/// 하나의 오브젝트로 관리를 해주기 위해 싱글톤을 사용했다.
/// 게임의 대부분의 로직을 여기서 처리해준다.
/// 여기서 함수를 만들고 다른 스크립트에서 필요한 함수를 가져다가 쓴다.
/// 
/// 2022.08.02 Ocean Project1 Room
/// </summary>

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //SoundManager _sound = new SoundManager();

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                {
                    Debug.Log("no Singletone obj");
                }
            }
            return _instance;
        }
    }

    ///// 사운드 매니저
    //public static SoundManager Sound
    //{
    //    get
    //    {
    //        return Instance._sound;
    //    }
    //}

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }


        for (int i = 0; i < 9; i++)
        {
            dollInfo.Add(null);
        }

    }
    private void Start()
    {
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (InteractionEvent.Instance.isEvent == true)
        {
            //SoundManager.Instance.PlayBGM("Main_BGM");
            SoundManager.Instance.FadeInSound("Main_BGM");
        }
        else
        {
            SoundManager.Instance.FadeOutSound("Main_BGM");
        }
        Dollnum = int.Parse(Doll.text);

        EndingScene1();
        //EndingScene2();
        //EndingScene3();
        //EndingScene4();
    }

    /// 목표수량, 현재 만든 수량, 오늘의 날짜, 내일 날짜 등등
    [System.NonSerialized]
    public static int MaxinumDoll = 9;
    [System.NonSerialized]
    public int Dollnum;                         // 현재 내가 가지고 있는 인형의 수
    [System.NonSerialized]
    public static int _DispatchFee = 900;
    [System.NonSerialized]
    public int _Profit;
    private int tempCookies;
    private int tempCoin;
    [System.NonSerialized]
    public int CookieGet;                       // 선택된 인형이 만들 수있는 쿠키의 수
    [System.NonSerialized]
    public int useCoin;                         // 선택된 인형을 쿠키로 만들 때 사용되는 돈
    [System.NonSerialized]
    public float timer;
    /// UI
    public TextMeshProUGUI[] UIToday;
    public TextMeshProUGUI CurrentCookies;
    public TextMeshProUGUI Goal_Cookies;
    public TextMeshProUGUI D_day;
    public TextMeshProUGUI Coin;
    public TextMeshProUGUI Doll;

    /// 발주서 내용
    public TextMeshProUGUI OrderContents;
    public TextMeshProUGUI CoName;

    /// 정산서 내용
    public TextMeshProUGUI ContractMoney;                  // 계약금
    public TextMeshProUGUI Fixed_Rent;                     // 부지사용료
    public TextMeshProUGUI Fixed_ZFee;                     // 좀건비
    public TextMeshProUGUI Fixed_Food;                     // 사료비
    public TextMeshProUGUI Fixed_EFee;                     // 장비 유지비
    public GameObject Penalty_text;
    public TextMeshProUGUI Penalty;                        // 변동 지출비(계약파기금 등등)
    public TextMeshProUGUI Profit;                         // 총 수익

    /// 포획실 눌렀을 때
    public GameObject CapturePopUp;
    public TextMeshProUGUI Capture_Contents;
    public TextMeshProUGUI DispatchFee;
    public Button OkButton;
    public Button CancleButton;                 // 포획을 취소하는 버튼
    public GameObject Capturing;                // 포획중을 알리는 UI
    public GameObject CompleteCapture;          // 포획을 완료한 후 나올 UI 팝업
    public TextMeshProUGUI Complete_DollCount;             // 포획완료  UI에 뜰 포획에 성공한 인형 갯수

    /// ErrorMessage
    public GameObject ErrorPopUp;
    public TextMeshProUGUI ErrorMessage;
    public Button ErrorButton;

    /// 쿠키제조를 눌렀을 때
    public GameObject CookieRoomPopUp;
    public GameObject MakingCookies;
    public Button Make_Ok;
    public Button Make_Cancle;
    public TextMeshProUGUI MaxDoll;
    public TextMeshProUGUI CurrentDoll;
    public GameObject Complete_Making;
    public TextMeshProUGUI Complete_Cookies;
    public GameObject[] CheckButton;
    public Button[] CancleCheckButtons;
    public GameObject[] Selected;
    public GameObject[] DollsImage;             // 인형을 파싱할 때 인형의 머리카락과 옷, 상처 등을 보여주는 
    public GameObject[] DollText;               // 인형이 선택됐을 때 대사를 보여주기 위한 오브젝트
    public TextMeshProUGUI[] DollText_text;                // 인형이 선택됐을 때 해당 대사를 써주기 위한 변수
    public TextMeshProUGUI TotalCoin;
    public TextMeshProUGUI TotalCookies;
    public GameObject CookieAnimation;

    /// 팝업 UI들을 관리해주기 위해 사용하는 플래그들
    private bool isOrder = false;               // 발주서 팝업을 뜨게 해주는 플래그
    [System.NonSerialized]
    public bool isResult = false;               // 정산서 팝업이 뜨게 해주는 플래그
    private bool isSucceed = false;             // 정해진 날까지 목표수량을 달성했는지 알려주는 플래그
    [System.NonSerialized]
    public bool isTodayCapture = false;         // 포획을 하고나면 그 날은 더이상 못한다는 에러 메세지를 띄우는 플래그
    [System.NonSerialized]
    public bool isPopUp = false;                // 팝업을 띄워야 할 때 사용하는 플래그
    [System.NonSerialized]
    public bool isCapturing = false;            // 포획중에 다시 건물을 못누르게 하는 플래그
    [System.NonSerialized]
    public bool isMakingCookie = false;
    [System.NonSerialized]
    public bool isTodayMakeCookie = false;      // 쿠키를 제조하고 나면 그 날은 더이상 못한다는 에러 메세지를 띄워주는 플래그
    [System.NonSerialized]
    public bool isSelected = false;
    [System.NonSerialized]
    public bool isErrorPopUp = false;           // 다른 팝업창 위에 오류팝업이 떴을 때 백그라운드를 꺼주지 않기 위한 플래그, 두 개의 팝업이 떠있어야 할 경우 사용

    /// 만든 함수들을 적용시킬 오브젝트들
    public GameObject Order;
    public GameObject Calculate;
    public GameObject PopUpBackGround;
    public GameObject ResultSucceed;
    public GameObject ResultFailed;
    public Button nextdayButton;
    public Button CalculateButton;
    public TextMeshProUGUI[] CookieUI;
    public GameObject Parsing;
    public GameObject _Result;
    public GameObject HideUI;

    /// 발주서에 있어야 할 내용 정보
    public OrderInfo orderInfo;

    /// 정산서에 있어야 할 내용 정보
    public ResultInfo resultInfo;

    /// 잡아온 인형의 정보
    public List<DollInfo> dollInfo = new List<DollInfo>(9);
    public Dictionary<TextMeshProUGUI, int> SelectedDolls = new Dictionary<TextMeshProUGUI, int>();
    public List<DollInfo> Check_Dolls = new List<DollInfo>();
    public List<int> SelectedUI = new List<int>();


    public void EndingScene1()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.LogWarning("엔딩씬 1");
            UIToday[1].text = "19";
            InteractionEvent.Instance.NowEventIndex = 146;
            InteractionEvent.Instance.saveEventID.Add("M1043", "완료");
            InteractionEvent.Instance.saveEventID.Add("M3034", "완료");
            //InteractionEvent.Instance.isEvent = false;
        }
    }

    public void EndingScene3()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Debug.LogWarning("엔딩씬 3");
            UIToday[1].text = "19";
            InteractionEvent.Instance.NowEventIndex = 146;
            InteractionEvent.Instance.saveEventID.Add("M1042", "완료");
            InteractionEvent.Instance.saveEventID.Add("M3034", "완료");
            //InteractionEvent.Instance.isEvent = false;
        }
    }

    public void EndingScene2()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.LogWarning("엔딩씬 2");
            UIToday[1].text = "19";
            InteractionEvent.Instance.NowEventIndex = 146;
            InteractionEvent.Instance.saveEventID.Add("M1043", "완료");
            InteractionEvent.Instance.saveEventID.Add("M3034", "완료");

        }
    }

    public void EndingScene4()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.LogWarning("엔딩씬 4");
            UIToday[1].text = "19";
            InteractionEvent.Instance.NowEventIndex = 146;
            InteractionEvent.Instance.saveEventID.Add("M1043", "완료");
            InteractionEvent.Instance.saveEventID.Add("M4022", "완료");
            //InteractionEvent.Instance.isEvent = false;
        }
    }
    /// 다음날로 버튼을 누르면 발주서가 보이고 다음날 버튼을 비활성화 한다
    public void PopUpOrder()
    {
        SoundManager.Instance.PlaySE("ContractOpen_Click");
        Debug.Log("소리재생 완료");
        Order.SetActive(true);
        PopUpBackGroundOn();
        Order_Contents();
        NoPressNextDayButton();
        isOrder = true;
        isPopUp = true;
    }

    /// 다음날 버튼을 누르지 못하게 하는 함수
    public void NoPressNextDayButton()
    {
        nextdayButton.interactable = false;
    }

    public void PressNextDayButton()
    {
        nextdayButton.interactable = true;
    }

    /// order의 정보를 가져와서 발주서에 적어주는 함수
    public void Order_Contents()
    {
        if (UIToday[1].text == "1")
        {
            orderInfo.DueDate = "5월 5일";
            orderInfo.CookieCount = 200;
            orderInfo.ContractMoney = 7000;
            orderInfo.CoName = "좀비초교";

            OrderContents.text = string.Format("단체소풍날에 먹을 쿠키가 필요합니다.\n{0}까지 쿠키를 {1}개 전달해주세요.\n계약금 : {2}Z", orderInfo.DueDate, orderInfo.CookieCount, orderInfo.ContractMoney);
            CoName.text = orderInfo.CoName;
        }
        else if (UIToday[1].text == "6")
        {
            orderInfo.DueDate = "5월 10일";
            orderInfo.CookieCount = 250;
            orderInfo.ContractMoney = 8500;
            orderInfo.CoName = "좀비 주식회사";

            OrderContents.text = string.Format("회사 단합 운동회에서 먹을 쿠키가 필요합니다.\n{0}까지 쿠키를 {1}개 전달해주세요.\n계약금 : {2}Z", orderInfo.DueDate, orderInfo.CookieCount, orderInfo.ContractMoney);
            CoName.text = orderInfo.CoName;
        }
        else if (UIToday[1].text == "11")
        {
            orderInfo.DueDate = "5월 15일";
            orderInfo.CookieCount = 400;
            orderInfo.ContractMoney = 12000;
            orderInfo.CoName = "좀비 바게트";

            OrderContents.text = string.Format("점포에서 판매할 쿠키가 필요합니다.\n{0}까지 쿠키를 {1}개 전달해주세요.\n계약금 : {2}Z", orderInfo.DueDate, orderInfo.CookieCount, orderInfo.ContractMoney);
            CoName.text = orderInfo.CoName;
        }
        else if (UIToday[1].text == "16")
        {
            orderInfo.DueDate = "5월 20일";
            orderInfo.CookieCount = 700;
            orderInfo.ContractMoney = 30000;
            orderInfo.CoName = "ZJ 식품";

            OrderContents.text = string.Format("브랜드로 점포들에 판매할 쿠키를 발주 신청합니다.\n{0}까지 쿠키를 {1}개를 제조해주세요.\n계약금 : {2}Z", orderInfo.DueDate, orderInfo.CookieCount, orderInfo.ContractMoney);
            CoName.text = orderInfo.CoName;
        }

    }

    // 리스트의 인덱스를 찾아 해당 인덱스의 정보를 지워주기 위한 함수.
    public int FindListIndex(List<int> list, int val)
    {
        if (list == null)
        {
            return -1;
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == val)
            {
                return i;
            }
        }

        return -1;
    }

    public void Off_Order()
    {
        if (int.Parse(UIToday[1].text) < 20 && isResult == false)
        {
            if (isOrder)
            {
                Order.SetActive(false);
                ResultFailed.SetActive(false);
                ResultSucceed.SetActive(false);
                PopUpBackGroundOff();
                PressNextDayButton();
                isOrder = false;
                //isTodayCapture = false;
                //isTodayMakeCookie = false;
                isPopUp = false;
                Change_InfoOrder();

            }
        }
        else if (int.Parse(UIToday[1].text) <= 20 && isResult == true)
        {
            Order.SetActive(false);
            ResultFailed.SetActive(false);
            ResultSucceed.SetActive(false);
            PopUpBackGroundOff();
            PressNextDayButton();
            isOrder = false;
            //isTodayCapture = false;
            //isTodayMakeCookie = false;
            isPopUp = false;

            Calculate_Result();
        }
    }


    /// 발주서 정보에 따라 다시 설정되는 목표 수량과 D-day날짜 그리고 계약금을 바꿔주는 함수
    public void Change_InfoOrder()
    {
        Goal_Cookies.text = string.Format(" {0}", orderInfo.CookieCount);

        if (orderInfo.DueDate == "5월 5일")
        {
            D_day.text = " 5 / 5";
        }
        else if (orderInfo.DueDate == "5월 10일")
        {
            D_day.text = " 5 / 10";
        }
        else if (orderInfo.DueDate == "5월 15일")
        {
            D_day.text = " 5 / 15";
        }
        else if (orderInfo.DueDate == "5월 20일")
        {
            D_day.text = " 5 / 20";
        }
    }

    /// 팝업이 떴을 때 배경의 밝기가 내려갈 수 있게 하나의 불투명한 이미지를 껐다 켜준다.
    public void PopUpBackGroundOn()
    {
        PopUpBackGround.SetActive(true);
    }

    public void PopUpBackGroundOff()
    {
        PopUpBackGround.SetActive(false);
    }

    /// 디데이 날짜에 나오는 정산서 UI 
    public void Result()
    {
        isResult = true;

        PopUpOrder();

        // 만약 목표 수량을 채웠으면 성공했다는 이미지를 아니라면 실패했다는 이미지를 띄워준다.
        if (int.Parse(CurrentCookies.text) >= orderInfo.CookieCount)
        {

            SoundManager.Instance.PlaySE("Stamp");
            ResultSucceed.SetActive(true);
            isSucceed = true;
        }
        else
        {
            SoundManager.Instance.PlaySE("Stamp");
            ResultFailed.SetActive(true);
            isSucceed = false;
        }
    }

    /// 정산서에 있는 내용과 현재 내가 소지하고 있는 재화나 물건을 계산해주는 함수이다.
    public void Calculate_Result()
    {
        SoundManager.Instance.PlaySE("ContractOpen_Click");

        Calculate.SetActive(true);
        PopUpBackGroundOn();
        isPopUp = true;

        if (isSucceed == true)
        {
            ContractMoney.text = orderInfo.ContractMoney.ToString();
            Fixed_Rent.text = resultInfo.Rent.ToString();
            Fixed_ZFee.text = resultInfo.ZFee.ToString();
            Fixed_Food.text = resultInfo.Food.ToString();
            Fixed_EFee.text = resultInfo.Cfee.ToString();
            _Profit = orderInfo.ContractMoney + (resultInfo.Rent + resultInfo.ZFee + resultInfo.Food + resultInfo.Cfee);
            Profit.text = _Profit.ToString();
        }
        else
        {
            ContractMoney.text = "";
            Fixed_Rent.text = resultInfo.Rent.ToString();
            Fixed_ZFee.text = resultInfo.ZFee.ToString();
            Fixed_Food.text = resultInfo.Food.ToString();
            Fixed_EFee.text = resultInfo.Cfee.ToString();
            Penalty_text.SetActive(true);
            Penalty.text = Penalty_Calculate(UIToday[1].text).ToString();

            _Profit = (resultInfo.Rent + resultInfo.ZFee + resultInfo.Food + resultInfo.Cfee + Penalty_Calculate(UIToday[1].text));
            Profit.text = _Profit.ToString();
        }
    }

    private int Penalty_Calculate(string day)
    {
        int _Penalty = 0;

        switch (day)
        {
            case "5":
            {
                Penalty.text = "-4500";
                _Penalty = int.Parse(Penalty.text);
            }
            break;

            case "10":
            {
                Penalty.text = "-5000";
                _Penalty = int.Parse(Penalty.text);
            }
            break;

            case "15":
            {
                Penalty.text = "-9000";
                _Penalty = int.Parse(Penalty.text);
            }
            break;

            case "20":
            {
                Penalty.text = "-15000";
                _Penalty = int.Parse(Penalty.text);
            }
            break;
        }

        return _Penalty;
    }

    /// 정산서의 버튼을 누르면 실행 할 함수
    public void PressContinueButton()
    {
        SoundManager.Instance.PlaySE("ContractOpen_Click");
        isPopUp = false;

        UIToday[1].text = (int.Parse(UIToday[1].text) + 1).ToString();
        Calculate.SetActive(false);
        PopUpBackGroundOff();
        isResult = false;
        _Result.GetComponent<Result>().isResult = false;

        if (isSucceed == true)
        {
            CurrentCookies.text = (int.Parse(CurrentCookies.text) - orderInfo.CookieCount).ToString();
            Coin.text = (int.Parse(Coin.text) + _Profit).ToString();
            SoundManager.Instance.PlaySE("CoinChange");
        }
        else
        {
            Coin.text = (int.Parse(Coin.text) + _Profit).ToString();
            Penalty_text.SetActive(false);
            SoundManager.Instance.PlaySE("CoinChange");

            if (int.Parse(Coin.text) <= 0)
            {
                Coin.text = "0";
                // 게임 오버 화면으로 넘어가기
                InteractionEvent.Instance.isGameOver = true;
                HideUI.SetActive(false);
                SoundManager.Instance.StopBGM("MainBGM");
                SoundManager.Instance.StopAllSE();
                SoundManager.Instance.PlayBGM("GameOver_BGM");
                SceneManager.LoadScene("GameOver");
            }
        }

    }

    /// 포획실을 눌렀을 때 현대 소지하고 있는 인형수가 전체 인형수보다 작으면 포획을 한다는 팝업이 나오고 아니면 공간이 부족하다는 메세지가 나오게 하기
    public void Start_Captrue()
    {
        if (Dollnum < MaxinumDoll && !isTodayCapture && !isPopUp)
        {
            CapturePopUp.SetActive(true);
            PopUpBackGroundOn();
            NoPressNextDayButton();
            Capture_Contents.text = string.Format("{0}", MaxinumDoll - Dollnum);
            DispatchFee.text = string.Format("{0}Z", _DispatchFee);
            isPopUp = true;
        }
        // 오류 팝업 : 오늘은 더 이상 파견을 나갈 수 없습니다.
        // isTodayCapture조건을 먼저 둬야 하루에 한번만 판견을 나가게 할 수 있다.
        if (isTodayCapture && !isPopUp)
        {
            SoundManager.Instance.PlaySE("ErrorPopUp");
            ErrorPopUp.SetActive(true);
            PopUpBackGroundOn();
            NoPressNextDayButton();
            ErrorMessage.text = "오늘은 더 이상 파견을 나갈 수 없습니다.";
            isPopUp = true;
        }
        // 오류팝업 : 진열대에 공간이 부족합니다.
        else if (Dollnum >= MaxinumDoll && !isPopUp)
        {
            SoundManager.Instance.PlaySE("ErrorPopUp");
            ErrorPopUp.SetActive(true);
            PopUpBackGroundOn();
            NoPressNextDayButton();
            ErrorMessage.text = "진열대 공간이 부족합니다.";
            isPopUp = true;
        }
    }

    /// 인형을 포획한 후 몇 개의 인형을 포획했는지 UI에 띄워주는 함수
    /// 최대 소지할 수 있는 인형에서 현재 소지하고 있는 인형 수를 뺀 만큼 포획한다.
    public void End_Captrue()
    {
        CompleteCapture.SetActive(true);

        AddDolls();

        SoundManager.Instance.PlaySE("DollChange");
        Complete_DollCount.text = string.Format(" {0}", MaxinumDoll - Dollnum);

        Invoke("CaptureHide", 1f);
    }

    /// 최대 인형 수에서 현재 소지하고 있는 인형의 갯수만큼 뺀 자리를 채운다.
    public void AddDolls()
    {
        int Index = MaxinumDoll - Dollnum;
        string tempGender;

        for (int i = 0; i < 9; i++)
        {
            if (dollInfo[i] == null)
            {
                if (int.Parse(UIToday[1].text) == 1)
                {
                    // 8번을 돌았는데도 해당 특수인형이 없을 수 도있으니 억지로 넣어준다.
                    if (i == 8)
                    {
                        // 리스트에서 conditionDate가 1인 인형을 찾아서 해당하는 정보는 넣어준다.
                        int tempIndex = ParsingJson.DollInfoList.FindIndex(x => x.Condition_Date == 1);
                        dollInfo[i] = ParsingJson.DollInfoList[tempIndex];
                    }
                    else
                    {
                        // 나머지는 랜덤으로 가져오기
                        dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];
                    }


                    if (dollInfo[i].type == 1)
                    {
                        if ((dollInfo[i].Condition_Date == int.Parse(UIToday[1].text)) && dollInfo[i].Condition_Event == "null")
                        {
                            // 이미 해당 특수 인형이 있다면 나머지 특수인형은 일반으로 돌려주기
                            if (InteractionEvent.Instance.saveDollID.ContainsValue(dollInfo[i].ID))
                            {
                                while (dollInfo[i].type != 0)
                                {
                                    dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];
                                }
                            }
                        }
                        else
                        {
                            while (dollInfo[i].type != 0)
                            {
                                dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];
                            }
                        }
                    }

                    InteractionEvent.Instance.dollID.Add(dollInfo[i].ID);

                    // 다른 일반 인형은 포획할 때 마다 중복이 나올 수 있다.
                    //if (InteractionEvent.Instance.saveDollID.ContainsValue(dollInfo[i].ID))
                    //{
                    //    InteractionEvent.Instance.saveDollID.Add(i, dollInfo[i].ID +);
                    //}
                    //else
                    //{
                    //}
                    InteractionEvent.Instance.saveDollID.Add(i, dollInfo[i].ID);

                    DollsImage[i].SetActive(true);

                    PartsHair(i);
                    PartsDress(i);
                    PartsScars(i);
                    PartsSmile(i);
                }
                else if (int.Parse(UIToday[1].text) == 9)
                {
                    // 리스트에서 conditionDate가 9인 인형을 찾아서 해당하는 정보는 넣어준다.
                    // 9를 가진 인형들이 총 9개라 list의 findIndex는 같은 값을 가진게 있다면 제일 처음의 값을 
                    // 반환해준다니 거기서부터 i를 더해줘서 9개를 파싱해온다.
                    int tempIndex = ParsingJson.DollInfoList.FindIndex(x => x.Condition_Date == 9);

                    dollInfo[i] = ParsingJson.DollInfoList[tempIndex + i];

                    InteractionEvent.Instance.dollID.Add(dollInfo[i].ID);

                    // if (InteractionEvent.Instance.saveDollID.ContainsKe(dollInfo[i].ID))
                    // {
                    //     InteractionEvent.Instance.saveDollID.Add(dollInfo[i].ID + "중복", i);
                    // }
                    // else
                    // {
                    // }
                    InteractionEvent.Instance.saveDollID.Add(i, dollInfo[i].ID);

                    DollsImage[i].SetActive(true);

                    PartsHair(i);
                    PartsDress(i);
                    PartsScars(i);
                    PartsSmile(i);

                }
                else if (int.Parse(UIToday[1].text) == 11)
                {
                    int tempIndex = ParsingJson.DollInfoList.FindIndex(x => x.Condition_Date == 11);
                    // 11을 가지고있는 인형은 특정 이벤트도 실행을 했어야지만 얻을 수 있다.
                    if (InteractionEvent.Instance.saveEventID.ContainsKey(ParsingJson.DollInfoList[tempIndex].Condition_Event))
                    {
                        dollInfo[i] = ParsingJson.DollInfoList[tempIndex + i];

                        InteractionEvent.Instance.dollID.Add(dollInfo[i].ID);

                        //if (InteractionEvent.Instance.saveDollID.ContainsKey(dollInfo[i].ID))
                        //{
                        //    InteractionEvent.Instance.saveDollID.Add(dollInfo[i].ID + "중복", i);
                        //}
                        //else
                        //{
                        //}
                        InteractionEvent.Instance.saveDollID.Add(i, dollInfo[i].ID);

                        DollsImage[i].SetActive(true);

                        PartsHair(i);
                        PartsDress(i);
                        PartsScars(i);
                        PartsSmile(i);

                    }
                    else
                    {
                        dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];

                        if (dollInfo[i].type == 1)
                        {
                            while (dollInfo[i].type != 0)
                            {
                                dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];
                            }
                        }

                        InteractionEvent.Instance.dollID.Add(dollInfo[i].ID);

                        //if (InteractionEvent.Instance.saveDollID.ContainsKey(dollInfo[i].ID))
                        //{
                        //    InteractionEvent.Instance.saveDollID.Add(dollInfo[i].ID + "중복", i);
                        //}
                        //else
                        //{
                        //}
                        InteractionEvent.Instance.saveDollID.Add(i, dollInfo[i].ID);

                        DollsImage[i].SetActive(true);

                        PartsHair(i);
                        PartsDress(i);
                        PartsScars(i);
                        PartsSmile(i);
                    }
                }
                else if (int.Parse(UIToday[1].text) == 16)
                {
                    // 8번을 돌았는데도 해당 특수인형이 없을 수 도있으니 억지로 넣어준다.
                    //if (i == 8)
                    //{
                    //    // 리스트에서 conditionDate가 1인 인형을 찾아서 해당하는 정보는 넣어준다.
                    //    int tempIndex = ParsingJson.DollInfoList.FindIndex(x => x.Condition_Date == 16);
                    //    dollInfo[i] = ParsingJson.DollInfoList[tempIndex];
                    //}
                    //else
                    //{
                    //    // 나머지는 랜덤으로 가져오기
                    //    dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];
                    //}

                    int tempIndex = ParsingJson.DollInfoList.FindIndex(x => x.Condition_Date == 16);

                    dollInfo[i] = ParsingJson.DollInfoList[tempIndex];

                    if (dollInfo[i].type == 1)
                    {
                        if ((dollInfo[i].Condition_Date == int.Parse(UIToday[1].text)) && dollInfo[i].Condition_Event == "null")
                        {
                            // 이미 해당 특수 인형이 있다면 나머지 특수인형은 일반으로 돌려주기
                            if (InteractionEvent.Instance.saveDollID.ContainsValue(dollInfo[i].ID))
                            {
                                while (dollInfo[i].type != 0)
                                {
                                    dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];
                                }
                            }
                        }
                        else
                        {
                            while (dollInfo[i].type != 0)
                            {
                                dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];
                            }
                        }
                    }

                    InteractionEvent.Instance.dollID.Add(dollInfo[i].ID);

                    // 다른 일반 인형은 포획할 때 마다 중복이 나올 수 있다.
                    //if (InteractionEvent.Instance.saveDollID.ContainsKey(dollInfo[i].ID))
                    //{
                    //    InteractionEvent.Instance.saveDollID.Add(dollInfo[i].ID + "중복", i);
                    //}
                    //else
                    //{
                    //}
                    InteractionEvent.Instance.saveDollID.Add(i, dollInfo[i].ID);

                    DollsImage[i].SetActive(true);

                    PartsHair(i);
                    PartsDress(i);
                    PartsScars(i);
                    PartsSmile(i);
                }
                else
                {
                    // 특정 조건이 아니면 다 랜덤으로 가져온다.
                    dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];

                    if (dollInfo[i].type == 1)
                    {
                        while (dollInfo[i].type != 0)
                        {
                            dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];
                        }
                    }
                    InteractionEvent.Instance.dollID.Add(dollInfo[i].ID);

                    //if (InteractionEvent.Instance.saveDollID.ContainsKey(dollInfo[i].ID))
                    //{
                    //    InteractionEvent.Instance.saveDollID.Add(dollInfo[i].ID + "중복", i);
                    //}
                    //else
                    //{
                    //}
                    InteractionEvent.Instance.saveDollID.Add(i, dollInfo[i].ID);

                    DollsImage[i].SetActive(true);

                    PartsHair(i);
                    PartsDress(i);
                    PartsScars(i);
                    PartsSmile(i);
                }
            }
        }

        for (int y = 0; y < 9; y++)
        {
            if (CookieUI[y].text == "")
            {
                while (CookieUI[y].text == "")
                {
                    if (dollInfo[y].Gender == 1)
                    {
                        tempGender = "여자";
                    }
                    else
                    {
                        tempGender = "남자";
                    }

                    CookieUI[y].text = string.Format("{0} / {1} / {2}", tempGender, dollInfo[y].Age, dollInfo[y].Cookie_Get);
                    SelectedDolls.Add(CookieUI[y], y);
                }
            }
        }

        CurrentDoll.text = (int.Parse(CurrentDoll.text) - Index).ToString();
    }

    // 파싱한 인형 데이터의 값에 따라 인형의 머리카락을 나타내주기위해 사용
    // 해당 인형의 자식 오브젝트를 찾아 스트라이트 이미지를 바꿔준다.
    private void PartsHair(int num)
    {
        if (dollInfo[num].DollParts_Hair == "hair1")
        {
            DollsImage[num].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/hair1", typeof(Sprite)) as Sprite;
            //DollHair.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprite/Object/Doll/hair1", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Hair == "hair2")
        {
            DollsImage[num].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/hair2", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Hair == "hair3")
        {
            DollsImage[num].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/hair3", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Hair == "hair4")
        {
            DollsImage[num].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/hair4", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Hair == "hair5")
        {
            DollsImage[num].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/hair5", typeof(Sprite)) as Sprite;
        }
    }

    // 파싱한 인형 데이터의 값에 따라 인형의 옷을 나타내주기위해 사용
    private void PartsDress(int num)
    {
        if (dollInfo[num].DollParts_Clothes == "dress1")
        {
            DollsImage[num].transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/dress1", typeof(Sprite)) as Sprite;
            //DollHair.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprite/Object/Doll/hair1", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Clothes == "dress2")
        {
            DollsImage[num].transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/dress2", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Clothes == "dress3")
        {
            DollsImage[num].transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/dress3", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Clothes == "dress4")
        {
            DollsImage[num].transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/dress4", typeof(Sprite)) as Sprite;
        }

    }

    // 파싱한 인형 데이터의 값에 따라 인형의 얼굴을 나타내주기위해 사용
    private void PartsSmile(int num)
    {
        if (dollInfo[num].DollParts_Face == "smile1")
        {
            DollsImage[num].transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/smile1", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Face == "smile2")
        {
            DollsImage[num].transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/smile2", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Face == "smile3")
        {
            DollsImage[num].transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/smile3", typeof(Sprite)) as Sprite;
        }
    }

    // 파싱한 인형 데이터의 값에 따라 인형의 상처를 나타내주기위해 사용
    private void PartsScars(int num)
    {
        if (dollInfo[num].DollParts_Scar == "scar1")
        {
            DollsImage[num].transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/scar1", typeof(Sprite)) as Sprite;
            //DollHair.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprite/Object/Doll/hair1", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Scar == "scar2")
        {
            DollsImage[num].transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/scar2", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Scar == "scar3")
        {
            DollsImage[num].transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/scar3", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Scar == "scar4")
        {
            DollsImage[num].transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/scar4", typeof(Sprite)) as Sprite;
        }
        else if (dollInfo[num].DollParts_Scar == "scar5")
        {
            DollsImage[num].transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load("Sprite/Object/Doll/scar5", typeof(Sprite)) as Sprite;
        }
    }

    public void Start_MakeCookies()
    {
        if (!isTodayMakeCookie && !isPopUp)
        {
            CurrentDoll.text = Dollnum.ToString();      // 현재 남은 쿠키가 몇 인지 알려주기 위해 현재 소지하고 있는 인형의 수를 넣어줬다.
            CookieRoomPopUp.SetActive(true);
            PopUpBackGroundOn();
            NoPressNextDayButton();
            isPopUp = true;
        }

        if (isTodayMakeCookie && !isPopUp)
        {
            SoundManager.Instance.PlaySE("ErrorPopUp");
            ErrorPopUp.SetActive(true);
            PopUpBackGroundOn();
            NoPressNextDayButton();
            ErrorMessage.text = "쿠키 제조는 하루에\n한 번만 가능합니다.";
            isPopUp = true;
        }
    }

    public void End_MakeCookies()
    {
        Complete_Making.SetActive(true);

        Selected_Dolls();

        Invoke("Hide", 1f);
    }

    /// 선택된 인형들의 정보를 지워주고 그 인형의 count대로 쿠키를 만들어준다.
    public void Selected_Dolls()
    {
        int _SelectedCookie = 0;
        int _tempUI;
        int _Value;

        SoundManager.Instance.PlaySE("CookieChange");
        SoundManager.Instance.PlaySE("DollChange");

        Complete_Cookies.text = string.Format("{0}", CookieGet);

        tempCookies = int.Parse(CurrentCookies.text) + int.Parse(Complete_Cookies.text);
        CurrentCookies.text = tempCookies.ToString();

        tempCoin = int.Parse(Coin.text) - useCoin;
        Coin.text = tempCoin.ToString();
        SoundManager.Instance.PlaySE("CoinChange");

        _SelectedCookie = int.Parse(Doll.text) - Check_Dolls.Count;
        Doll.text = _SelectedCookie.ToString();

        for (int j = 0; j < Check_Dolls.Count; j++)
        {
            _tempUI = SelectedUI[j];
            CookieUI[_tempUI].text = "";
            DollText_text[_tempUI].text = "";
            SelectedDolls.TryGetValue(CookieUI[_tempUI], out _Value);

            if (InteractionEvent.Instance.dollID.Contains(dollInfo[_Value].ID))
            {
                InteractionEvent.Instance.dollID.Remove(dollInfo[_Value].ID);
            }

            if (InteractionEvent.Instance.saveDollID.ContainsValue(dollInfo[_Value].ID))
            {
                InteractionEvent.Instance.saveDollID.Remove(_Value);
            }
            //else if(InteractionEvent.Instance.saveDollID.ContainsKey(dollInfo[_Value].ID + "중복"))
            //{
            //    InteractionEvent.Instance.saveDollID.Remove(dollInfo[_Value].ID + "중복");
            //}

            dollInfo[_Value] = null;
            SelectedDolls.Remove(CookieUI[_tempUI]);
            Selected[_tempUI].SetActive(false);
            DollsImage[_tempUI].SetActive(false);
        }
        SelectedUI.Clear();
        Check_Dolls.Clear();
        CookieGet = 0;
        useCoin = 0;
        TotalCookies.text = string.Format("{0}", CookieGet);
        TotalCoin.text = string.Format("{0}", useCoin);
    }

    /// 인형을 포획한 후 완료 UI가 1초 후 사라지게 해주는 함수 
    /// 포획한 인형의 정보를 쿠키 제조실 UI에 뜰 수 있게 해주기
    private void Hide()
    {
        Complete_Making.SetActive(false);
        isTodayMakeCookie = true;
    }

    private void CaptureHide()
    {
        CompleteCapture.SetActive(false);
        isTodayCapture = true;
    }

    public int GetOpenDate()
    {
        return int.Parse(UIToday[1].text);
    }
}

