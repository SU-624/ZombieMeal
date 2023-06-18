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

    // 11�ϳ� �������� �Ѿ �� �̺�Ʈ �ε����� �������� �ʾƼ� �������� �̺�Ʈ ���� �ȉ�
    private void Update()
    {
        sendTime += Time.deltaTime;

        if (sendTime > waitingTime && isGameOver == false)
        {
            RequirementEvent();
            sendTime = 0;
        }
    }

    /// ���� �����ϰ� �ִ� �̺�Ʈ�� �ε���(i)
    public int NowEventIndex;
    private float sendTime;
    private float waitingTime;

    /// �̺�Ʈ�� ����� ���� �ٸ��� ���ϰ� �ؾ��Ѵ�.
    public bool isEvent;
    public bool isGameOver;

    /// Ư�� ���ǿ� �̺�Ʈ �г��� ���ֱ� ���� ����
    public GameObject EventPenal;
    public GameObject eventContent, eventBox;
    public GameObject EventSelectionButton1;
    public GameObject EventSelectionButton2;
    public GameObject EventSelectionButton3;
    public TextMeshProUGUI EventTitle;

    /// ��ư�� �������� �� �̺�Ʈ�� �ݺ������� �޼����� ������ ���ϰ� �ϴ� �÷���
    public bool EventButton1Exist;
    public bool EventButton2Exist;
    public bool EventButton3Exist;

    /// ������ �����ؾ��� ���� �̺�Ʈ�� �����͸� ������ ����Ʈ
    public List<EventInfo> checkEvent = new List<EventInfo>();
    public Dictionary<string, string> saveEventID = new Dictionary<string, string>();   // � �̺�Ʈ�� �Ϸ� �ߴ��� �����ϱ����� ����, value1
    //public Dictionary<string, int> saveDollID = new Dictionary<string, int>();          // � �̺�Ʈ�� �Ϸ� �ߴ��� �����ϱ����� ����. value2
    public Dictionary<int, string> saveDollID = new Dictionary<int, string>();
    public List<string> dollID = new List<string>();
    public List<string> nextEvent1ID = new List<string>();                               // �������� ���� ���� �̺�Ʈ�� ã���ֱ����� �̺�Ʈ ���̵� ���ϱ� ���� ����Ʈ   
    public List<string> nextEvent2ID = new List<string>();                               // �������� ���� ���� �̺�Ʈ�� ã���ֱ����� �̺�Ʈ ���̵� ���ϱ� ���� ����Ʈ   
    public List<string> nextEvent3ID = new List<string>();
    public List<int> tempOpenDate = new List<int>();                                            // ������ ������ �� ���� �̺�Ʈ�� ������ �����ؼ� ���� �̺�Ʈ�� �پ�Ѿ �� �ְ� ���ֱ� ���� ����Ʈ

    // �������� ���� ���� �̺�Ʈ�� ã���ֱ����� �̺�Ʈ ���̵� ���ϱ� ���� ����Ʈ   

    // TODO:OCEAN - �̺�Ʈ�� �߻��ϸ� �ٸ� �۾��� ���� ���ϰ� ��������
    public bool IsEventExecuted()
    {
        //??
        // �̺�Ʈ�� ���� �� �߻��ϴ� ���ǵ��� �� �����ϸ� true�� ��ȯ�Ѵ�.

        return false;
    }

    // �̺�Ʈ UI�� �ؽ�Ʈ�� ������ִ� �Լ�
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
    /// �ֱ������� �̺�Ʈ üũ�� �ϱ� ���� �Լ�
    /// </summary>
    public void RequirementEvent()
    {
        // PSEUDO Code (�ѱ�)
        // ���� ����Ʈ�� �°�, ����Ÿ��1�� �°�, ����Ÿ��2�� �´� i��°
        // �̺�Ʈ������ ã�Ƽ�.....
        EventInfo _nowInfo = ParsingEvent.EventInfoList[NowEventIndex];
        CheckEvent(_nowInfo);

        // ��� �����͸� ��ȸ �� ������ ��ã�Ҵٸ� �׳� �̺�Ʈ�� ���°Ŵ�.
    }

    public void CheckEvent(EventInfo info)
    {
        // Ư���ϰ� ���� ����Ʈ�� �ƴ� ����Ÿ�Ը� �ִ� �̺�Ʈ�� ���� ����ó�� �����.
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
                    saveEventID.Add(info.ID, "�Ϸ�");
                }

                EventPenal.SetActive(true);
                GameManager.Instance.PopUpBackGroundOn();
                GameManager.Instance.NoPressNextDayButton();

                OneEventLine(info);
            }
        }
        // ù°�� ���� ��ȹ�� ������ ��Ű ������ ���� �ʾ��� �� ����ó��
        else if (GameManager.Instance.GetOpenDate() == 1 && info.ID == "M103" && DayAndNight.dayandNight.isNight == true)
        {
            // �ε����� OpenType1�� ���� ������ �÷��ְ� �̺�Ʈ ����
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
        // ���� �Ǿ���� ����Ǵ� �̺�Ʈ
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
        // �̷� ������ NextEvent�� ������ �����̶� NextEvent�� ������ �̺�Ʈ ����ǰ� �����.
        else if (info.OpenDate == 0 && info.OpenType1 == 0 && info.OpenType1 == 0 && info.ID != "")
        {
            if (!saveEventID.ContainsKey(info.ID) && info.ID != "")
            {
                saveEventID.Add(info.ID, "�Ϸ�");
            }
            PlayNextEventSound();
            OneEventLine(info);
        }
        // ���� ���� ���� �̺�Ʈ�� ������ �����̴� ������ �������ش�.
        else if (info.OpenDate == 0 && info.OpenType1 == 0 && info.OpenType1 == 0)
        {
            OneEventLine(info);
        }
        // �̺�Ʈ�� �����ʰ� �������� �Ѿ�� ���� ����ó��, �ΰ��� ��¥�� ������ �� ���� �̺�Ʈ�� �ε����� ������Ų��
        else if (GameManager.Instance.GetOpenDate() != info.OpenDate && info.OpenDate != 0)
        {
            if (info.OpenDate == 4 && GameManager.Instance.GetOpenDate() == 5)
            {
                // �� ���� �ƹ��͵� ���� �ʱ� ������ �Ѱ��ش�.
                return;
            }
            else if (info.OpenDate == 6 && GameManager.Instance.GetOpenDate() == 4)
            {
                // �� ���� �ƹ��͵� ���� �ʱ� ������ �Ѱ��ش�.
                return;
            }
            else if (info.OpenDate == 9 && GameManager.Instance.GetOpenDate() == 10)
            {
                // �ε����� OpenDate�� 11�� �� �� ���� ���������ش�.
                UntilTheOpenDate11();
            }
            else if (info.OpenDate == 11 && GameManager.Instance.GetOpenDate() == 12)
            {
                // �ε����� OpenDate�� 14�� �� ������ ���������ش�.
                UntilTheOpenDate14();
            }
            else if (info.OpenDate == 14 && (GameManager.Instance.GetOpenDate() == 11 || GameManager.Instance.GetOpenDate() == 12 || GameManager.Instance.GetOpenDate() == 13))
            {
                // �� ���� �ƹ��͵� ���� �ʱ� ������ �Ѱ��ش�.
                return;
            }
            else if (info.OpenDate == 14 && GameManager.Instance.GetOpenDate() == 15)
            {
                // �ε����� OpenDate�� 16�� �� ������ ���������ش�.
                UntilTheOpenDate16();
            }
            else if (info.OpenDate == 16 && GameManager.Instance.GetOpenDate() == 14)
            {
                // �� ���� �ƹ��͵� ���� �ʱ� ������ �Ѱ��ش�.
                return;
            }
            else if (info.OpenDate == 16 && GameManager.Instance.GetOpenDate() == 15)
            {
                // �� ���� �ƹ��͵� ���� �ʱ� ������ �Ѱ��ش�.
                return;
            }
            else if (info.OpenDate == 16 && GameManager.Instance.GetOpenDate() == 17)
            {
                // �ε����� OpenDate�� 17�� �� ������ ���������ش�.
                UntilTheOpenDate17();
            }
            else if (info.OpenDate == 17 && GameManager.Instance.GetOpenDate() == 18)
            {
                // �ε����� OpenDate�� 18�� �� ������ ���������ش�.
                UntilTheOpenDate18();
            }
            else if (info.OpenDate == 18 && GameManager.Instance.GetOpenDate() == 19)
            {
                // �ε����� OpenDate�� 19�� �� ������ ���������ش�.
                UntilTheOpenDate19();
            }
            else if (info.OpenDate == 11 && GameManager.Instance.GetOpenDate() == 9)
            {
                return;
            }
            // ���� �Ϸ翡 �����ϴ� �̺�Ʈ�� �ϳ��� �ִٸ� �� �̺�Ʈ�� OpenDate�� ���� ��¥�� �Ϸ� ���ѰͰ� ������ �׳� �Ѿ��.
            else if (info.OpenDate == GameManager.Instance.GetOpenDate() + 1)
            {
                return;
            }
            else
            {
                FindNextOpenDate();
            }
        }
        // 9���� �̺�Ʈ �� ���� �б��� ����ó��
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
                    saveEventID.Add(info.ID, "�Ϸ�");
                }

                SoundManager.Instance.PlayEvent("EventAlarm");
                EventPenal.SetActive(true);
                GameManager.Instance.PopUpBackGroundOn();
                GameManager.Instance.NoPressNextDayButton();

                OneEventLine(info);
            }
        }
        // �װ� �ƴ϶�� �Ϲ� �̺�Ʈ�̴� ��� ������ ��¥�� ���� �����ִ� �̺�Ʈ ������ ����ִ� ����Ʈ�� Ȯ�����ش�.
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
                    saveEventID.Add(info.ID, "�Ϸ�");
                }

                SoundManager.Instance.PlayEvent("EventAlarm");
                EventPenal.SetActive(true);
                GameManager.Instance.PopUpBackGroundOn();
                GameManager.Instance.NoPressNextDayButton();

                OneEventLine(info);
            }
        }
        // ���� ���ǵ��� ��� �������� ���ߴٸ� ������ �ٽ� ������ �����Ǳ� �������� �ε����� ������Ű�� �ʴ´�.
        // �׷��� �Ϸ簡 ���� ���ӻ��� ��¥�� �Ϸ� ������ �̺�Ʈ ������ ��� ����Ʈ�� �ε�����
        // ���ӻ��� ��¥�� ���� ��¥�� ���� ������ ���������ش�.
        else
        {
            return;
        }
    }

    // �Ľ��� json������ �� �پ� ó�����ֱ� ���� �Լ�
    private void OneEventLine(EventInfo info)
    {
        // ��ư�� �̹� ������ ���¶�� �н�
        if (EventButton1Exist == true ||
            EventButton2Exist == true ||
            EventButton3Exist == true)
        {
            return;
        }

        SendEventText(info.EventText);
        // ������ �´� �̺�Ʈ���
        // UI�� ��µ� �ϰ� �̰������� �Ѵ�.

        // ���� selectiontext1�� ������ 
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

        // ��ư�� �� ���� ������
        if (info.SelectionText1.Length != 0
            || info.SelectionText2.Length != 0
            || info.SelectionText3.Length != 0)
        {
            return;
        }
        // ��ư�� �� ���� ���ٸ�
        else
        {
            /// TODO:OCEAN �ڷ�ƾ���� �����̸� �ְ� �Ѵ�.

            NowEventIndex++;
        }

    }

    /// <summary>
    /// 3���� ������ ��� ���ٸ�(0) �̰��� ����üũ�� ���� �ʴ� �������̴�.
    /// Event Text�� �������� �о���� ���� ���ǹ�
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
            case 1: // �������� �̵� ��ư�� ������ ������ �Ǿ��� �� �̺�Ʈ ����
            {
                if (DayAndNight.dayandNight.isNight)
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 2: // OpenDate�� ��ȹ�� �Ϸ����� �� �̺�Ʈ ����
            {
                if (info.OpenDate == int.Parse(GameManager.Instance.UIToday[1].text) && GameManager.Instance.isTodayCapture)
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 3: // OpenDate�� ��Ű������ �Ϸ����� �� �̺�Ʈ ����
            {
                if (info.OpenDate == int.Parse(GameManager.Instance.UIToday[1].text) && GameManager.Instance.isTodayMakeCookie)
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 4: // �ش� ID�� �̺�Ʈ�� �Ϸᰡ �Ǿ��� �� �̺�Ʈ ����
            {
                if (info.Value1 != "")
                {
                    // ���� vlaue���� �������̸� ,�� ���ڿ��� �߶��ش�
                    bool isComma = info.Value1.Contains(",");

                    if (isComma)
                    {
                        string[] newValue = info.Value1.Split(',');
                        int newValueSize = newValue.Length;

                        // ���ڿ� ���̿� ���� ���ǹ� ó���� �ٸ��� ������ؼ� ���
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
            case 5: // �÷��̾ �����ϰ� �ִ� ������ value ������ �� �̺�Ʈ �߻�
            {
                if (int.Parse(GameManager.Instance.Coin.text) <= int.Parse(info.Value1))
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 6:  // �÷��̾ �����ϰ� �ִ� ������ value �̻��� �� �̺�Ʈ �߻�
            {
                if (int.Parse(GameManager.Instance.Coin.text) >= int.Parse(info.Value1))
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 7: // �÷��̾ �����ϰ� �ִ� �ηɼ��� value ������ �� �̺�Ʈ �߻�
            {
                if (GameManager.Instance.Dollnum <= int.Parse(info.Value1))
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 8: // �÷��̾ �����ϰ� �ִ� �ηɼ��� value �̻��� �� �̺�Ʈ �߻�
            {
                if (GameManager.Instance.Dollnum >= int.Parse(info.Value1))
                {
                    isOpenType1 = true;
                }
            }
            break;

            case 9: // �÷��̾ ���� �����ϰ� �ִ� �����߿� value�� ID�� ������ �̺�Ʈ �߻�
            {
                // value�� ����ִ� ID�� ��ųʸ��� �ִ��� Ȯ���Ѵ�.
                if (info.Value1 != "")
                {
                    // ���� vlaue���� �������̸� ,�� ���ڿ��� �߶��ش�
                    bool isComma = info.Value1.Contains(",");

                    if (isComma)
                    {
                        string[] newValue = info.Value1.Split(',');
                        int newValueSize = newValue.Length;

                        // ���ڿ� ���̿� ���� ���ǹ� ó���� �ٸ��� ������ؼ� ���
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

            case 10: // value ID�� �̺�Ʈ�� �Ϸ���� �ʾ��� �� �̺�Ʈ �߻�
            {
                //if (!saveEventID.ContainsKey(info.Value1))
                //{
                //    isOpenType1 = true;
                //}

                if (info.Value1 != "")
                {
                    // ���� vlaue���� �������̸� ,�� ���ڿ��� �߶��ش�
                    bool isComma = info.Value1.Contains(",");

                    if (isComma)
                    {
                        string[] newValue = info.Value1.Split(',');
                        int newValueSize = newValue.Length;

                        // ���ڿ� ���̿� ���� ���ǹ� ó���� �ٸ��� ������ؼ� ���
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

            case 11: // value ID�� ������ ���� �� �̺�Ʈ �߻�
            {
                List<string> dollID = new List<string>();

                // ���ο� ����Ʈ�� ������ְ� �ű⿡ ���� �����ϰ��ִ� ������ ������ �־��� �� 
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
            case 1: // �������� �̵� ��ư�� ������ ������ �Ǿ��� �� �̺�Ʈ ����
            {
                if (DayAndNight.dayandNight.isNight)
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 2: // OpenDate�� ��ȹ�� �Ϸ����� �� �̺�Ʈ ����
            {
                if (info.OpenDate == int.Parse(GameManager.Instance.UIToday[1].text) && GameManager.Instance.isTodayCapture)
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 3: // OpenDate�� ��Ű������ �Ϸ����� �� �̺�Ʈ ����
            {
                if (info.OpenDate == int.Parse(GameManager.Instance.UIToday[1].text) && GameManager.Instance.isTodayMakeCookie)
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 4: // �ش� ID�� �̺�Ʈ�� �Ϸᰡ �Ǿ��� �� �̺�Ʈ ����
            {
                if (info.Value2 != "")
                {
                    bool isComma = info.Value2.Contains(",");

                    if (isComma)
                    {
                        string[] newValue = info.Value2.Split(',');
                        int newValueSize = newValue.Length;

                        // ���ڿ� ���̿� ���� ���ǹ� ó���� �ٸ��� ������ؼ� ���
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

            case 5: // �÷��̾ �����ϰ� �ִ� ������ value ������ �� �̺�Ʈ �߻�
            {
                if (int.Parse(GameManager.Instance.Coin.text) <= int.Parse(info.Value2))
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 6:  // �÷��̾ �����ϰ� �ִ� ������ value �̻��� �� �̺�Ʈ �߻�
            {
                if (int.Parse(GameManager.Instance.Coin.text) >= int.Parse(info.Value2))
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 7: // �÷��̾ �����ϰ� �ִ� �ηɼ��� value ������ �� �̺�Ʈ �߻�
            {
                if (GameManager.Instance.Dollnum <= int.Parse(info.Value2))
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 8: // �÷��̾ �����ϰ� �ִ� �ηɼ��� value �̻��� �� �̺�Ʈ �߻�
            {
                if (GameManager.Instance.Dollnum >= int.Parse(info.Value2))
                {
                    isOpenType2 = true;
                }
            }
            break;

            case 9: // �÷��̾ ���� �����ϰ� �ִ� �����߿� value�� ID�� ������ �̺�Ʈ �߻�
            {
                // value�� ����ִ� ID�� ��ųʸ��� �ִ��� Ȯ���Ѵ�.
                if (info.Value2 != "")
                {
                    // ���� vlaue���� �������̸� ,�� ���ڿ��� �߶��ش�
                    bool isComma = info.Value2.Contains(",");

                    if (isComma)
                    {
                        string[] newValue = info.Value2.Split(',');
                        int newValueSize = newValue.Length;

                        // ���ڿ� ���̿� ���� ���ǹ� ó���� �ٸ��� ������ؼ� ���
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

            case 10: // value ID�� �̺�Ʈ�� �Ϸ���� �ʾ��� �� �̺�Ʈ �߻�
            {
                bool isComma = info.Value2.Contains(",");

                if (isComma)
                {
                    string[] newValue = info.Value2.Split(',');
                    int newValueSize = newValue.Length;

                    // ���ڿ� ���̿� ���� ���ǹ� ó���� �ٸ��� ������ؼ� ���
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

            case 11: // value ID�� ������ ���� �� �̺�Ʈ �߻�
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
                // ��� ������ ��������ؼ� ���õ� �����͵��� �ʱ�ȭ �����ش�.
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

                /// �ش� ���̵� ���� ������ ������ ��� �ִ��� Ȯ���ϰ� �ش� �����븦 ����ֱ� ���� ó�� ��
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
            #region _���������� Result2���� 6���� ������� �ʴ´�.
            //case 6:
            //{
            //    int ResultValue_DollID;

            //    /// �ش� ���̵� ���� ������ ������ ��� �ִ��� Ȯ���ϰ� �ش� �����븦 ����ֱ� ���� ó�� ��
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

                /// �ش� ���̵� ���� ������ ������ ��� �ִ��� Ȯ���ϰ� �ش� �����븦 ����ֱ� ���� ó�� ��
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

    // �������� ������� ��� ���ٸ� ���� �̺�Ʈ�� ã�� ���� �ε������� �÷��ش�. 
    // NextEvent�� �ǳʶٱ� ���� �̺�Ʈ ���̵� 4������ ID�� ã�´�.
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

    // ���� �̺�Ʈ�� ���� �ʰ� ��¥�� �ٲ� �� �������� �ش��ϴ� �̺�Ʈ �ε����� �Ѱ��ֱ� ���� �Լ�
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

    // ù���� ���� ��ȹ �� ��Ű ������ �����ʰ� �������� �Ѿ ���� ����ó��
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

    // selectiontext�� nextevent�� �ִٸ� �ش� ���̵�� �̺�Ʈ ���̵� ������ �� ���� �ε����� �÷��ش�.
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

    // ���� ���� �� ����Ǵ� �̺�Ʈ ó���� �ϱ����� �Լ�
    public void FindNightEvent()
    {
        // ���� �̺�Ʈ ����Ʈ���� ��ĭ ������ �̺�Ʈ�� opendDate�� ���� �ΰ����� ��¥�� �ٸ��� �ٽ� ������ �ٲ��ְ� �̺�Ʈ�� ��������ش�.
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
            // 19�Ͽ� ��ħ�� �Ǵ°� �����ֱ�
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
        // Result�� �ִٸ�...
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

            // �׷��ٸ� ���� �̺�Ʈ�� �ִٴ� ���̴�.
            //NowEventIndex++;
            FindNextEvent1ID();
        }
        else
        {
            // �� �� ������ �̺�Ʈ ����
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
        // Result�� ���ٸ�...
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
            // �׷��ٸ� ���� �̺�Ʈ�� �ִٴ� ���̴�.
            //NowEventIndex++;
            FindNextEvent2ID();
        }
        else
        {
            // �� �� ������ �̺�Ʈ ����
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
        // Result�� �ִٸ�...
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
            // �׷��ٸ� ���� �̺�Ʈ�� �ִٴ� ���̴�.
            //NowEventIndex++;
            FindNextEvent3ID();
        }
        else
        {
            // �� �� ������ �̺�Ʈ ����
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
        sendTime = 2;       // ��ư�� ������ ���� �̺�Ʈ �ؽ�Ʈ�� ����� �� �ְ� ���ֱ� ���� 
        ResetButtonStateAll();
    }

    private void ResetButtonStateAll()
    {
        EventButton1Exist = false;
        EventButton2Exist = false;
        EventButton3Exist = false;

        // ��ư�� Ŭ���ϰ� ���� �ش� �������� ���� �̺�Ʈ ���̵� �����ϴ� ����Ʈ�� ����ش�
        // ���� �̺�Ʈ�� ���޾� ���ͼ� �� �ٽ� �������� �����ؾ� �� �� �ֱ� ����
        nextEvent1ID.Clear();
        nextEvent2ID.Clear();
        nextEvent3ID.Clear();
    }

    // �̺�Ʈ �ϳ��� ������ ���� �̺�Ʈ�� �߻����� �� ��ȭ�� ���� �ʰ� ���ֱ� ���� �ڽ� ������Ʈ���� �������ش�.
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
