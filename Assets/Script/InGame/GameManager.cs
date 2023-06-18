using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �ٸ� ��ũ��Ʈ�� �پ��ִ� ������Ʈ���� GetComponent���ֱ� ����ϱ�
/// �ϳ��� ������Ʈ�� ������ ���ֱ� ���� �̱����� ����ߴ�.
/// ������ ��κ��� ������ ���⼭ ó�����ش�.
/// ���⼭ �Լ��� ����� �ٸ� ��ũ��Ʈ���� �ʿ��� �Լ��� �����ٰ� ����.
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

    ///// ���� �Ŵ���
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

    /// ��ǥ����, ���� ���� ����, ������ ��¥, ���� ��¥ ���
    [System.NonSerialized]
    public static int MaxinumDoll = 9;
    [System.NonSerialized]
    public int Dollnum;                         // ���� ���� ������ �ִ� ������ ��
    [System.NonSerialized]
    public static int _DispatchFee = 900;
    [System.NonSerialized]
    public int _Profit;
    private int tempCookies;
    private int tempCoin;
    [System.NonSerialized]
    public int CookieGet;                       // ���õ� ������ ���� ���ִ� ��Ű�� ��
    [System.NonSerialized]
    public int useCoin;                         // ���õ� ������ ��Ű�� ���� �� ���Ǵ� ��
    [System.NonSerialized]
    public float timer;
    /// UI
    public TextMeshProUGUI[] UIToday;
    public TextMeshProUGUI CurrentCookies;
    public TextMeshProUGUI Goal_Cookies;
    public TextMeshProUGUI D_day;
    public TextMeshProUGUI Coin;
    public TextMeshProUGUI Doll;

    /// ���ּ� ����
    public TextMeshProUGUI OrderContents;
    public TextMeshProUGUI CoName;

    /// ���꼭 ����
    public TextMeshProUGUI ContractMoney;                  // ����
    public TextMeshProUGUI Fixed_Rent;                     // ��������
    public TextMeshProUGUI Fixed_ZFee;                     // ���Ǻ�
    public TextMeshProUGUI Fixed_Food;                     // ����
    public TextMeshProUGUI Fixed_EFee;                     // ��� ������
    public GameObject Penalty_text;
    public TextMeshProUGUI Penalty;                        // ���� �����(����ı�� ���)
    public TextMeshProUGUI Profit;                         // �� ����

    /// ��ȹ�� ������ ��
    public GameObject CapturePopUp;
    public TextMeshProUGUI Capture_Contents;
    public TextMeshProUGUI DispatchFee;
    public Button OkButton;
    public Button CancleButton;                 // ��ȹ�� ����ϴ� ��ư
    public GameObject Capturing;                // ��ȹ���� �˸��� UI
    public GameObject CompleteCapture;          // ��ȹ�� �Ϸ��� �� ���� UI �˾�
    public TextMeshProUGUI Complete_DollCount;             // ��ȹ�Ϸ�  UI�� �� ��ȹ�� ������ ���� ����

    /// ErrorMessage
    public GameObject ErrorPopUp;
    public TextMeshProUGUI ErrorMessage;
    public Button ErrorButton;

    /// ��Ű������ ������ ��
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
    public GameObject[] DollsImage;             // ������ �Ľ��� �� ������ �Ӹ�ī���� ��, ��ó ���� �����ִ� 
    public GameObject[] DollText;               // ������ ���õ��� �� ��縦 �����ֱ� ���� ������Ʈ
    public TextMeshProUGUI[] DollText_text;                // ������ ���õ��� �� �ش� ��縦 ���ֱ� ���� ����
    public TextMeshProUGUI TotalCoin;
    public TextMeshProUGUI TotalCookies;
    public GameObject CookieAnimation;

    /// �˾� UI���� �������ֱ� ���� ����ϴ� �÷��׵�
    private bool isOrder = false;               // ���ּ� �˾��� �߰� ���ִ� �÷���
    [System.NonSerialized]
    public bool isResult = false;               // ���꼭 �˾��� �߰� ���ִ� �÷���
    private bool isSucceed = false;             // ������ ������ ��ǥ������ �޼��ߴ��� �˷��ִ� �÷���
    [System.NonSerialized]
    public bool isTodayCapture = false;         // ��ȹ�� �ϰ��� �� ���� ���̻� ���Ѵٴ� ���� �޼����� ���� �÷���
    [System.NonSerialized]
    public bool isPopUp = false;                // �˾��� ����� �� �� ����ϴ� �÷���
    [System.NonSerialized]
    public bool isCapturing = false;            // ��ȹ�߿� �ٽ� �ǹ��� �������� �ϴ� �÷���
    [System.NonSerialized]
    public bool isMakingCookie = false;
    [System.NonSerialized]
    public bool isTodayMakeCookie = false;      // ��Ű�� �����ϰ� ���� �� ���� ���̻� ���Ѵٴ� ���� �޼����� ����ִ� �÷���
    [System.NonSerialized]
    public bool isSelected = false;
    [System.NonSerialized]
    public bool isErrorPopUp = false;           // �ٸ� �˾�â ���� �����˾��� ���� �� ��׶��带 ������ �ʱ� ���� �÷���, �� ���� �˾��� ���־�� �� ��� ���

    /// ���� �Լ����� �����ų ������Ʈ��
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

    /// ���ּ��� �־�� �� ���� ����
    public OrderInfo orderInfo;

    /// ���꼭�� �־�� �� ���� ����
    public ResultInfo resultInfo;

    /// ��ƿ� ������ ����
    public List<DollInfo> dollInfo = new List<DollInfo>(9);
    public Dictionary<TextMeshProUGUI, int> SelectedDolls = new Dictionary<TextMeshProUGUI, int>();
    public List<DollInfo> Check_Dolls = new List<DollInfo>();
    public List<int> SelectedUI = new List<int>();


    public void EndingScene1()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.LogWarning("������ 1");
            UIToday[1].text = "19";
            InteractionEvent.Instance.NowEventIndex = 146;
            InteractionEvent.Instance.saveEventID.Add("M1043", "�Ϸ�");
            InteractionEvent.Instance.saveEventID.Add("M3034", "�Ϸ�");
            //InteractionEvent.Instance.isEvent = false;
        }
    }

    public void EndingScene3()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Debug.LogWarning("������ 3");
            UIToday[1].text = "19";
            InteractionEvent.Instance.NowEventIndex = 146;
            InteractionEvent.Instance.saveEventID.Add("M1042", "�Ϸ�");
            InteractionEvent.Instance.saveEventID.Add("M3034", "�Ϸ�");
            //InteractionEvent.Instance.isEvent = false;
        }
    }

    public void EndingScene2()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.LogWarning("������ 2");
            UIToday[1].text = "19";
            InteractionEvent.Instance.NowEventIndex = 146;
            InteractionEvent.Instance.saveEventID.Add("M1043", "�Ϸ�");
            InteractionEvent.Instance.saveEventID.Add("M3034", "�Ϸ�");

        }
    }

    public void EndingScene4()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.LogWarning("������ 4");
            UIToday[1].text = "19";
            InteractionEvent.Instance.NowEventIndex = 146;
            InteractionEvent.Instance.saveEventID.Add("M1043", "�Ϸ�");
            InteractionEvent.Instance.saveEventID.Add("M4022", "�Ϸ�");
            //InteractionEvent.Instance.isEvent = false;
        }
    }
    /// �������� ��ư�� ������ ���ּ��� ���̰� ������ ��ư�� ��Ȱ��ȭ �Ѵ�
    public void PopUpOrder()
    {
        SoundManager.Instance.PlaySE("ContractOpen_Click");
        Debug.Log("�Ҹ���� �Ϸ�");
        Order.SetActive(true);
        PopUpBackGroundOn();
        Order_Contents();
        NoPressNextDayButton();
        isOrder = true;
        isPopUp = true;
    }

    /// ������ ��ư�� ������ ���ϰ� �ϴ� �Լ�
    public void NoPressNextDayButton()
    {
        nextdayButton.interactable = false;
    }

    public void PressNextDayButton()
    {
        nextdayButton.interactable = true;
    }

    /// order�� ������ �����ͼ� ���ּ��� �����ִ� �Լ�
    public void Order_Contents()
    {
        if (UIToday[1].text == "1")
        {
            orderInfo.DueDate = "5�� 5��";
            orderInfo.CookieCount = 200;
            orderInfo.ContractMoney = 7000;
            orderInfo.CoName = "�����ʱ�";

            OrderContents.text = string.Format("��ü��ǳ���� ���� ��Ű�� �ʿ��մϴ�.\n{0}���� ��Ű�� {1}�� �������ּ���.\n���� : {2}Z", orderInfo.DueDate, orderInfo.CookieCount, orderInfo.ContractMoney);
            CoName.text = orderInfo.CoName;
        }
        else if (UIToday[1].text == "6")
        {
            orderInfo.DueDate = "5�� 10��";
            orderInfo.CookieCount = 250;
            orderInfo.ContractMoney = 8500;
            orderInfo.CoName = "���� �ֽ�ȸ��";

            OrderContents.text = string.Format("ȸ�� ���� �ȸ���� ���� ��Ű�� �ʿ��մϴ�.\n{0}���� ��Ű�� {1}�� �������ּ���.\n���� : {2}Z", orderInfo.DueDate, orderInfo.CookieCount, orderInfo.ContractMoney);
            CoName.text = orderInfo.CoName;
        }
        else if (UIToday[1].text == "11")
        {
            orderInfo.DueDate = "5�� 15��";
            orderInfo.CookieCount = 400;
            orderInfo.ContractMoney = 12000;
            orderInfo.CoName = "���� �ٰ�Ʈ";

            OrderContents.text = string.Format("�������� �Ǹ��� ��Ű�� �ʿ��մϴ�.\n{0}���� ��Ű�� {1}�� �������ּ���.\n���� : {2}Z", orderInfo.DueDate, orderInfo.CookieCount, orderInfo.ContractMoney);
            CoName.text = orderInfo.CoName;
        }
        else if (UIToday[1].text == "16")
        {
            orderInfo.DueDate = "5�� 20��";
            orderInfo.CookieCount = 700;
            orderInfo.ContractMoney = 30000;
            orderInfo.CoName = "ZJ ��ǰ";

            OrderContents.text = string.Format("�귣��� �����鿡 �Ǹ��� ��Ű�� ���� ��û�մϴ�.\n{0}���� ��Ű�� {1}���� �������ּ���.\n���� : {2}Z", orderInfo.DueDate, orderInfo.CookieCount, orderInfo.ContractMoney);
            CoName.text = orderInfo.CoName;
        }

    }

    // ����Ʈ�� �ε����� ã�� �ش� �ε����� ������ �����ֱ� ���� �Լ�.
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


    /// ���ּ� ������ ���� �ٽ� �����Ǵ� ��ǥ ������ D-day��¥ �׸��� ������ �ٲ��ִ� �Լ�
    public void Change_InfoOrder()
    {
        Goal_Cookies.text = string.Format(" {0}", orderInfo.CookieCount);

        if (orderInfo.DueDate == "5�� 5��")
        {
            D_day.text = " 5 / 5";
        }
        else if (orderInfo.DueDate == "5�� 10��")
        {
            D_day.text = " 5 / 10";
        }
        else if (orderInfo.DueDate == "5�� 15��")
        {
            D_day.text = " 5 / 15";
        }
        else if (orderInfo.DueDate == "5�� 20��")
        {
            D_day.text = " 5 / 20";
        }
    }

    /// �˾��� ���� �� ����� ��Ⱑ ������ �� �ְ� �ϳ��� �������� �̹����� ���� ���ش�.
    public void PopUpBackGroundOn()
    {
        PopUpBackGround.SetActive(true);
    }

    public void PopUpBackGroundOff()
    {
        PopUpBackGround.SetActive(false);
    }

    /// ���� ��¥�� ������ ���꼭 UI 
    public void Result()
    {
        isResult = true;

        PopUpOrder();

        // ���� ��ǥ ������ ä������ �����ߴٴ� �̹����� �ƴ϶�� �����ߴٴ� �̹����� ����ش�.
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

    /// ���꼭�� �ִ� ����� ���� ���� �����ϰ� �ִ� ��ȭ�� ������ ������ִ� �Լ��̴�.
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

    /// ���꼭�� ��ư�� ������ ���� �� �Լ�
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
                // ���� ���� ȭ������ �Ѿ��
                InteractionEvent.Instance.isGameOver = true;
                HideUI.SetActive(false);
                SoundManager.Instance.StopBGM("MainBGM");
                SoundManager.Instance.StopAllSE();
                SoundManager.Instance.PlayBGM("GameOver_BGM");
                SceneManager.LoadScene("GameOver");
            }
        }

    }

    /// ��ȹ���� ������ �� ���� �����ϰ� �ִ� �������� ��ü ���������� ������ ��ȹ�� �Ѵٴ� �˾��� ������ �ƴϸ� ������ �����ϴٴ� �޼����� ������ �ϱ�
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
        // ���� �˾� : ������ �� �̻� �İ��� ���� �� �����ϴ�.
        // isTodayCapture������ ���� �־� �Ϸ翡 �ѹ��� �ǰ��� ������ �� �� �ִ�.
        if (isTodayCapture && !isPopUp)
        {
            SoundManager.Instance.PlaySE("ErrorPopUp");
            ErrorPopUp.SetActive(true);
            PopUpBackGroundOn();
            NoPressNextDayButton();
            ErrorMessage.text = "������ �� �̻� �İ��� ���� �� �����ϴ�.";
            isPopUp = true;
        }
        // �����˾� : �����뿡 ������ �����մϴ�.
        else if (Dollnum >= MaxinumDoll && !isPopUp)
        {
            SoundManager.Instance.PlaySE("ErrorPopUp");
            ErrorPopUp.SetActive(true);
            PopUpBackGroundOn();
            NoPressNextDayButton();
            ErrorMessage.text = "������ ������ �����մϴ�.";
            isPopUp = true;
        }
    }

    /// ������ ��ȹ�� �� �� ���� ������ ��ȹ�ߴ��� UI�� ����ִ� �Լ�
    /// �ִ� ������ �� �ִ� �������� ���� �����ϰ� �ִ� ���� ���� �� ��ŭ ��ȹ�Ѵ�.
    public void End_Captrue()
    {
        CompleteCapture.SetActive(true);

        AddDolls();

        SoundManager.Instance.PlaySE("DollChange");
        Complete_DollCount.text = string.Format(" {0}", MaxinumDoll - Dollnum);

        Invoke("CaptureHide", 1f);
    }

    /// �ִ� ���� ������ ���� �����ϰ� �ִ� ������ ������ŭ �� �ڸ��� ä���.
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
                    // 8���� ���Ҵµ��� �ش� Ư�������� ���� �� �������� ������ �־��ش�.
                    if (i == 8)
                    {
                        // ����Ʈ���� conditionDate�� 1�� ������ ã�Ƽ� �ش��ϴ� ������ �־��ش�.
                        int tempIndex = ParsingJson.DollInfoList.FindIndex(x => x.Condition_Date == 1);
                        dollInfo[i] = ParsingJson.DollInfoList[tempIndex];
                    }
                    else
                    {
                        // �������� �������� ��������
                        dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];
                    }


                    if (dollInfo[i].type == 1)
                    {
                        if ((dollInfo[i].Condition_Date == int.Parse(UIToday[1].text)) && dollInfo[i].Condition_Event == "null")
                        {
                            // �̹� �ش� Ư�� ������ �ִٸ� ������ Ư�������� �Ϲ����� �����ֱ�
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

                    // �ٸ� �Ϲ� ������ ��ȹ�� �� ���� �ߺ��� ���� �� �ִ�.
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
                    // ����Ʈ���� conditionDate�� 9�� ������ ã�Ƽ� �ش��ϴ� ������ �־��ش�.
                    // 9�� ���� �������� �� 9���� list�� findIndex�� ���� ���� ������ �ִٸ� ���� ó���� ���� 
                    // ��ȯ���شٴ� �ű⼭���� i�� �����༭ 9���� �Ľ��ؿ´�.
                    int tempIndex = ParsingJson.DollInfoList.FindIndex(x => x.Condition_Date == 9);

                    dollInfo[i] = ParsingJson.DollInfoList[tempIndex + i];

                    InteractionEvent.Instance.dollID.Add(dollInfo[i].ID);

                    // if (InteractionEvent.Instance.saveDollID.ContainsKe(dollInfo[i].ID))
                    // {
                    //     InteractionEvent.Instance.saveDollID.Add(dollInfo[i].ID + "�ߺ�", i);
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
                    // 11�� �������ִ� ������ Ư�� �̺�Ʈ�� ������ �߾������ ���� �� �ִ�.
                    if (InteractionEvent.Instance.saveEventID.ContainsKey(ParsingJson.DollInfoList[tempIndex].Condition_Event))
                    {
                        dollInfo[i] = ParsingJson.DollInfoList[tempIndex + i];

                        InteractionEvent.Instance.dollID.Add(dollInfo[i].ID);

                        //if (InteractionEvent.Instance.saveDollID.ContainsKey(dollInfo[i].ID))
                        //{
                        //    InteractionEvent.Instance.saveDollID.Add(dollInfo[i].ID + "�ߺ�", i);
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
                        //    InteractionEvent.Instance.saveDollID.Add(dollInfo[i].ID + "�ߺ�", i);
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
                    // 8���� ���Ҵµ��� �ش� Ư�������� ���� �� �������� ������ �־��ش�.
                    //if (i == 8)
                    //{
                    //    // ����Ʈ���� conditionDate�� 1�� ������ ã�Ƽ� �ش��ϴ� ������ �־��ش�.
                    //    int tempIndex = ParsingJson.DollInfoList.FindIndex(x => x.Condition_Date == 16);
                    //    dollInfo[i] = ParsingJson.DollInfoList[tempIndex];
                    //}
                    //else
                    //{
                    //    // �������� �������� ��������
                    //    dollInfo[i] = ParsingJson.DollInfoList[Random.Range(0, ParsingJson.DollInfoList.Count)];
                    //}

                    int tempIndex = ParsingJson.DollInfoList.FindIndex(x => x.Condition_Date == 16);

                    dollInfo[i] = ParsingJson.DollInfoList[tempIndex];

                    if (dollInfo[i].type == 1)
                    {
                        if ((dollInfo[i].Condition_Date == int.Parse(UIToday[1].text)) && dollInfo[i].Condition_Event == "null")
                        {
                            // �̹� �ش� Ư�� ������ �ִٸ� ������ Ư�������� �Ϲ����� �����ֱ�
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

                    // �ٸ� �Ϲ� ������ ��ȹ�� �� ���� �ߺ��� ���� �� �ִ�.
                    //if (InteractionEvent.Instance.saveDollID.ContainsKey(dollInfo[i].ID))
                    //{
                    //    InteractionEvent.Instance.saveDollID.Add(dollInfo[i].ID + "�ߺ�", i);
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
                    // Ư�� ������ �ƴϸ� �� �������� �����´�.
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
                    //    InteractionEvent.Instance.saveDollID.Add(dollInfo[i].ID + "�ߺ�", i);
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
                        tempGender = "����";
                    }
                    else
                    {
                        tempGender = "����";
                    }

                    CookieUI[y].text = string.Format("{0} / {1} / {2}", tempGender, dollInfo[y].Age, dollInfo[y].Cookie_Get);
                    SelectedDolls.Add(CookieUI[y], y);
                }
            }
        }

        CurrentDoll.text = (int.Parse(CurrentDoll.text) - Index).ToString();
    }

    // �Ľ��� ���� �������� ���� ���� ������ �Ӹ�ī���� ��Ÿ���ֱ����� ���
    // �ش� ������ �ڽ� ������Ʈ�� ã�� ��Ʈ����Ʈ �̹����� �ٲ��ش�.
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

    // �Ľ��� ���� �������� ���� ���� ������ ���� ��Ÿ���ֱ����� ���
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

    // �Ľ��� ���� �������� ���� ���� ������ ���� ��Ÿ���ֱ����� ���
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

    // �Ľ��� ���� �������� ���� ���� ������ ��ó�� ��Ÿ���ֱ����� ���
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
            CurrentDoll.text = Dollnum.ToString();      // ���� ���� ��Ű�� �� ���� �˷��ֱ� ���� ���� �����ϰ� �ִ� ������ ���� �־����.
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
            ErrorMessage.text = "��Ű ������ �Ϸ翡\n�� ���� �����մϴ�.";
            isPopUp = true;
        }
    }

    public void End_MakeCookies()
    {
        Complete_Making.SetActive(true);

        Selected_Dolls();

        Invoke("Hide", 1f);
    }

    /// ���õ� �������� ������ �����ְ� �� ������ count��� ��Ű�� ������ش�.
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
            //else if(InteractionEvent.Instance.saveDollID.ContainsKey(dollInfo[_Value].ID + "�ߺ�"))
            //{
            //    InteractionEvent.Instance.saveDollID.Remove(dollInfo[_Value].ID + "�ߺ�");
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

    /// ������ ��ȹ�� �� �Ϸ� UI�� 1�� �� ������� ���ִ� �Լ� 
    /// ��ȹ�� ������ ������ ��Ű ������ UI�� �� �� �ְ� ���ֱ�
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

