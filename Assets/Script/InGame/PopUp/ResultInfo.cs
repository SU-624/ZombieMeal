using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 정산서에 표시해야하는 정보들
/// </summary>
public class ResultInfo : MonoBehaviour
{
    public int Rent = -4000;     // 부지 사용료
    public int ZFee = -2000;     // 좀건비
    public int Food = -1000;     // 사료비
    public int Cfee = -1000;     // 본사 수수료

    public int EFee = -1000;     // 장비 유지비
    public int SFee = -1000;     // 운송비

    public int TerFee1 = -4500;
    public int TerFee2 = -5000;
    public int TerFee3 = -9000;
    public int TerFee4 = -15000;
}