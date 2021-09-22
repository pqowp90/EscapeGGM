using System.Text; // StringBuilder 쓰기 위함
using UnityEngine;
public class MoneyUnitString
{
    static string[] unitSymbol = new string[] { "", "만", "억", "조", "경", "해" };
 
    // long 보다 double이 최대 값이 커서 double  사용
    public static string ToString(double value)
    {
        if( value == 0 ) { return "0"; }
            
        int unitID = 0;
            
        string number = string.Format("{0:# #### #### #### #### ####}", value).TrimStart();
        string[] splits = number.Split(' ');

        StringBuilder sb = new StringBuilder();
 
        for (int i = splits.Length; i > 0; i--)
        {
            int digits = 0;
            if (int.TryParse(splits[i - 1], out digits))
            {
                // 앞자리가 0이 아닐때
                if (digits != 0)
                {
                    sb.Insert(0, $"{ digits}{ unitSymbol[unitID] }");
                }
            }
            else
            {
                // 마이너스나 숫자외 문자
                sb.Insert(0, $"{ splits[i - 1] }");
            }
            unitID++;
        }
        return sb.ToString();            
    }
}

