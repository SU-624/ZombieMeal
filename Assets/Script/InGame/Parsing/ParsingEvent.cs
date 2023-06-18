using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

public class EventInfo
{
    public string ID;
    public string EventTitle;
    public int OpenDate;
    public int OpenType1;
    public string Value1;
    public int OpenType2;
    public string Value2;
    public string EventText;
    public string SelectionText1;
    public int Result1;
    public string ResultValue1;
    public string NextEvent1;
    public string SelectionText2;
    public int Result2;
    public int ResultValue2;
    public string NextEvent2;
    public string SelectionText3;
    public int Result3;
    public int ResultValue3;
    public string NextEvent3;
    public bool isEventChecked;

    public EventInfo(string ID, string EventTitle, int OpenDate, int OpenType1, string Value1, int OpenType2, string Value2,
        string EventText, string SelectionText1, int Result1, string ResultValue1, string NextEvent1, string SelectionText2,
        int Result2, int ResultValue2, string NextEvent2, string SelectionText3, int Result3, int ResultValue3, string NextEvent3)
    {
        this.ID = ID;
        this.EventTitle = EventTitle;
        this.OpenDate = OpenDate;                   // null값 있을 수 있음
        this.OpenType1 = OpenType1;                 // null값 있을 수 있음
        this.Value1 = Value1;                       // null값 있을 수 있음
        this.OpenType2 = OpenType2;                 // null값 있을 수 있음
        this.Value2 = Value2;                       // null값 있을 수 있음
        this.EventText = EventText;
        this.SelectionText1 = SelectionText1;
        this.Result1 = Result1;                     // null값 있을 수 있음
        this.ResultValue1 = ResultValue1;           // null값 있을 수 있음
        this.NextEvent1 = NextEvent1;               // null값 있을 수 있음
        this.SelectionText2 = SelectionText2;       // null값 있을 수 있음
        this.Result2 = Result2;                     // null값 있을 수 있음
        this.ResultValue2 = ResultValue2;           // null값 있을 수 있음
        this.NextEvent2 = NextEvent2;               // null값 있을 수 있음
        this.SelectionText3 = SelectionText3;       // null값 있을 수 있음
        this.Result3 = Result3;                     // null값 있을 수 있음
        this.ResultValue3 = ResultValue3;           // null값 있을 수 있음
        this.NextEvent3 = NextEvent3;               // null값 있을 수 있음
        
        this.isEventChecked = false;
    }

    public EventInfo() { }
}

public class ParsingEvent : MonoBehaviour
{
    public static List<EventInfo> EventInfoList = new List<EventInfo>();

    JsonData EventData;

    void ParsingEventInfo(JsonData name, List<EventInfo> eventInfo)
    {
        for (int i = 0; i < name.Count; i++)
        {
            string tempID = name[i][0].ToString();
            string tempEventTitle = name[i][1].ToString();

            string tempOpenDate;
            try
            {
                tempOpenDate = name[i][2].ToString();
            }
            catch (NullReferenceException e)
            {
                tempOpenDate = "null";
                Debug.Log(e);
            }

            string tempOpenType1;
            try
            {
                tempOpenType1 = name[i][3].ToString();
            }
            catch (NullReferenceException e)
            {
                tempOpenType1 = "null";
                Debug.Log(e);
            }

            string tempValue1;
            try
            {
                tempValue1 = name[i][4].ToString();
            }
            catch (NullReferenceException e)
            {
                tempValue1 = "null";
                Debug.Log(e);
            }

            string tempOpenType2;
            try
            {
                tempOpenType2 = name[i][5].ToString();
            }
            catch (NullReferenceException e)
            {
                tempOpenType2 = "null";
                Debug.Log(e);
            }

            string tempValue2;
            try
            {
                tempValue2 = name[i][6].ToString();
            }
            catch (NullReferenceException e)
            {
                tempValue2 = "null";
                Debug.Log(e);
            }

            string tempEventText = name[i][7].ToString();

            string tempSelectionText1;
            try
            {
                tempSelectionText1 = name[i][8].ToString();
            }
            catch (NullReferenceException e)
            {
                tempSelectionText1 = "";
                Debug.Log(e);
            }

            string tempResult1;
            try
            {
                tempResult1 = name[i][9].ToString();
            }
            catch (NullReferenceException e)
            {
                tempResult1 = "null";
                Debug.Log(e);
            }

            string tempResultValue1;
            try
            {
                tempResultValue1 = name[i][10].ToString();
            }
            catch
            {
                tempResultValue1 = "null";
            }

            string tempNextEvent1;
            try
            {
                tempNextEvent1 = name[i][11].ToString();
            }
            catch
            {
                tempNextEvent1 = "null";
            }

            string tempSelectionText2;
            try
            {
                tempSelectionText2 = name[i][12].ToString();
            }
            catch
            {
                tempSelectionText2 = "";
            }

            string tempResult2;
            try
            {
                tempResult2 = name[i][13].ToString();
            }
            catch
            {
                tempResult2 = "null";
            }

            string tempResultValue2;
            try
            {
                tempResultValue2 = name[i][14].ToString();
            }
            catch
            {
                tempResultValue2 = "null";
            }

            string tempNextEvent2;
            try
            {
                tempNextEvent2 = name[i][15].ToString();
            }
            catch
            {
                tempNextEvent2 = "null";
            }

            string tempSelectionText3;
            try
            {
                tempSelectionText3 = name[i][16].ToString();
            }
            catch
            {
                tempSelectionText3 = "";
            }

            string tempResult3;
            try
            {
                tempResult3 = name[i][17].ToString();
            }
            catch
            {
                tempResult3 = "null";
            }

            string tempResultValue3;
            try
            {
                tempResultValue3 = name[i][18].ToString();
            }
            catch
            {
                tempResultValue3 = "null";
            }

            string tempNextEvent3;
            try
            {
                tempNextEvent3 = name[i][19].ToString();
            }
            catch
            {
                tempNextEvent3 = "null";
            }

            int tempOpenDate_;
            if (int.TryParse(tempOpenDate, out tempOpenDate_))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log(tempOpenDate_);
            }

            int tempOpenType1_;
            if(int.TryParse(tempOpenType1,out tempOpenType1_))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }

            int tempOpenType2_;
            if (int.TryParse(tempOpenType2, out tempOpenType2_))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }

            int tempResult1_;
            if (int.TryParse(tempResult1, out tempResult1_))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }
            
            int tempResult2_;
            if (int.TryParse(tempResult2, out tempResult2_))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }

            int tempResultValue2_;
            if (int.TryParse(tempResultValue2, out tempResultValue2_))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }

            int tempResult3_;
            if (int.TryParse(tempResult3, out tempResult3_))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }
            
            int tempResultValue3_;
            if (int.TryParse(tempResultValue3, out tempResultValue3_))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }


            EventInfo tempInfo = new EventInfo(tempID, tempEventTitle, tempOpenDate_, tempOpenType1_, tempValue1, tempOpenType2_, tempValue2, tempEventText, tempSelectionText1, tempResult1_, tempResultValue1, tempNextEvent1, tempSelectionText2,
                tempResult2_, tempResultValue2_, tempNextEvent2, tempSelectionText3, tempResult3_, tempResultValue3_, tempNextEvent3);
            eventInfo.Add(tempInfo);
            Debug.Log("이벤트 데이터 파싱 완료");
        }

    }

    public void LoadBase_Event()
    {
        string filePath = Resources.Load("Json/EventData3").ToString();

        EventData = JsonMapper.ToObject(filePath);
        ParsingEventInfo(EventData, EventInfoList);
    }

    private void Start()
    {
        LoadBase_Event();
    }
}
