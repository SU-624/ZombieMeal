using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;


public class DollInfo
{
    public string ID;
    public int Condition_Date;
    public string Condition_Event;
    public int type;
    public int Gender;
    public int Age;
    public int Cookie_Get;
    public string DollText;
    public string DollParts_Hair;
    public string DollParts_Clothes;
    public string DollParts_Face;
    public string DollParts_Scar;
    public string DollParts_Body;
    public string DollParts_Cry;

    public DollInfo(string ID, int type, int Condition_Date, string Condition_Event,  int Gender, int Age, int Cookie_Get,
        string DollText, string DollParts_Hair, string DollParts_Clothes, string DollParts_Face, string DollParts_Scar, string DollParts_Body, string DollParts_Cry)
    {
        this.ID = ID;
        this.type = type;
        this.Condition_Date = Condition_Date;
        this.Condition_Event = Condition_Event;
        this.Gender = Gender;
        this.Age = Age;
        this.Cookie_Get = Cookie_Get;
        this.DollText = DollText;
        this.DollParts_Hair = DollParts_Hair;
        this.DollParts_Clothes = DollParts_Clothes;
        this.DollParts_Face = DollParts_Face;
        this.DollParts_Scar = DollParts_Scar;
        this.DollParts_Body = DollParts_Body;
        this.DollParts_Cry = DollParts_Cry;

    }

    public DollInfo() { }
}




public class ParsingJson : MonoBehaviour
{
    public static List<DollInfo> DollInfoList = new List<DollInfo>();
    public static List<DollInfo> MyDollInfo = new List<DollInfo>();

    JsonData DollData;

    void ParsingDollInfo(JsonData name, List<DollInfo> dollInfo)
    {
        for (int i = 0; i < name.Count; i++)
        {
            string tempID = name[i][0].ToString();
            string tempType = name[i][1].ToString();

            string tempConditionDate;
            try
            {
                tempConditionDate = name[i][2].ToString();
            }
            catch
            {
                tempConditionDate = "null";
            }

            string tempConditionEvent;
            try
            {
                tempConditionEvent = name[i][3].ToString();
            }
            catch
            {
                tempConditionEvent = "null";
            }

            string tempGender = name[i][4].ToString();
            string tempAge = name[i][5].ToString();
            string tempCount = name[i][6].ToString();

            string tempDollText;
            try
            {
                tempDollText = name[i][7].ToString();
            }
            catch
            {
                tempDollText = "null";
            }
            
            string tempDollParts_Hair;
            try
            {
                tempDollParts_Hair = name[i][8].ToString();
            }
            catch
            {
                tempDollParts_Hair = "null";
            }
            
            string tempDollParts_Clothes;
            try
            {
                tempDollParts_Clothes = name[i][9].ToString();
            }
            catch 
            {
                tempDollParts_Clothes = "null";
            }

            string tempDollParts_Face;
            try
            {
                tempDollParts_Face = name[i][10].ToString();
            }
            catch
            {
                tempDollParts_Face = "null";
            }

            string tempDollParts_Scar;
            try
            {
                tempDollParts_Scar = name[i][11].ToString();
            }
            catch
            {
                tempDollParts_Scar = "null";
            }

            string tempDollParts_Body;
            try
            {
                tempDollParts_Body = name[i][12].ToString();
            }
            catch
            {
                tempDollParts_Body = "null";
            }

            string tempDollParts_Cry;
            try
            {
                tempDollParts_Cry = name[i][13].ToString();
            }
            catch
            {
                tempDollParts_Cry = "null";
            }

            int tempConditionDate_;

            if(int.TryParse(tempConditionDate, out tempConditionDate_))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("null");
            }

            int tempType_ = int.Parse(tempType);

            int tempCount_ = int.Parse(tempCount);

            int tempGender_ = int.Parse(tempGender);
             
            int tempAge_ = int.Parse(tempAge);

            DollInfo tempInfo = new DollInfo(tempID, tempType_, tempConditionDate_, tempConditionEvent, tempGender_, tempAge_, tempCount_, tempDollText, tempDollParts_Hair
                , tempDollParts_Clothes, tempDollParts_Face, tempDollParts_Scar, tempDollParts_Body, tempDollParts_Cry);
            dollInfo.Add(tempInfo);
            Debug.Log("인형 데이터 파싱 완료");

        }
    }

    public void LoadBase()
    {
        //string JsonString;
        //string filePath;

        //filePath = Application.dataPath + "/Resources/Json/Dolldata3.json";

        //JsonString = File.ReadAllText(filePath);

        string filePath = Resources.Load("Json/DollData5").ToString();
        //TextAsset JsonString = Resources.Load("Json/DollData3.json") as TextAsset;

        DollData = JsonMapper.ToObject(filePath);
        //DollData = JsonMapper.ToObject(JsonString);
        ParsingDollInfo(DollData, DollInfoList);
    }

    private void Start()
    {
        LoadBase();
    }

}
