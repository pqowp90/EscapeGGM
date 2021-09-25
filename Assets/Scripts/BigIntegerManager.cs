using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CAH.GameSystem.BigNumber
{ 
    /// <summary>
    /// BigInteger의 단위를 표현할 수 있는 클래스
    /// </summary>
    public static class BigIntegerManager
    {      
        private static readonly BigInteger _unitSize = 1000;  
        private static Dictionary<string, BigInteger> _unitsMap = new Dictionary<string, BigInteger>();
        private static Dictionary<string, int> _idxMap = new Dictionary<string, int>(); 
        private static readonly List<string> _units = new List<string>();
        private static int _unitCapacity = 5; 
        private static readonly int _asciiA = 65;
        private static readonly int _asciiZ = 90;
        private static bool isInitialize = false;
        private static void UnitInitialize(int capacity)
        {
            _unitCapacity += capacity;
            
            //Initialize 0~999
            _units.Clear();
            _unitsMap.Clear();
            _idxMap.Clear();
            _units.Add("");
            _unitsMap.Add("", 0);
            _idxMap.Add("", 0);
            
            
            //capacity만큼 사전생성, capacity가 1인경우 A~Z
            //capacity가 2인경우 AA~AZ
            //capacity 1마다 ascii 알파벳 26개 생성되는 원리
            for (int n = 0; n <= _unitCapacity; n++)
            {
                for (int i = _asciiA; i <= _asciiZ; i++)
                {
                    string unit = null;
                    if (n == 0) 
                        unit = ((char) i).ToString();
                    else
                    {
                        var nCount = (float)n / 26; 
                        var nextChar = _asciiA + n - 1;  
                        var fAscii = (char) nextChar;
                        var tAscii = (char) i;
                        unit = $"{fAscii}{tAscii}"; 
                    }  
                    _units.Add(unit); 
                    _unitsMap.Add(unit, BigInteger.Pow(_unitSize, _units.Count-1)); 
                    _idxMap.Add(unit, _units.Count-1);
                }
            }
            isInitialize = true;
        }


        private static int GetPoint(int value)
        {
                return (value % 1000) / 100; 
        }
        
        private static (int value, int idx, int point) GetSize(BigInteger value)
        {
            //단위를 구하기 위한 값으로 복사
            var currentValue = value; 
            var current = (value / _unitSize) % _unitSize;
            var idx = 0; 
            var lastValue = 0;
            // 현재 값이 999(unitSize) 이상인경우 나눠야함.
            while (currentValue > _unitSize -1)
            {
                var predCurrentValue = currentValue / _unitSize;
                if (predCurrentValue <= _unitSize - 1)
                    lastValue = (int)currentValue;
                currentValue = predCurrentValue;
                idx += 1; 
            }

            var point = GetPoint(lastValue);  
            var originalValue = currentValue * 1000; 
            while (_units.Count <= idx) 
                UnitInitialize(5);  
            return ((int)currentValue, idx, point);
        }

        /// <summary>
        /// 숫자를 단위로 리턴
        /// </summary>
        /// <param name="value">값</param>
        /// <returns></returns>
        public static string GetUnit(BigInteger value)
        {
            if (isInitialize == false) 
                UnitInitialize(5);
            
            var sizeStruct = GetSize(value);
            return $"{sizeStruct.value}.{sizeStruct.point}{_units[sizeStruct.idx]}"; 
        }  
        
        /// <summary>
        /// 단위를 숫자로 변경
        /// 10A = 10000으로 리턴
        /// 1.2A = 1200으로 리턴
        /// 소수점 1자리만 지원함
        /// </summary>
        /// <param name="unit">단위</param>
        /// <returns></returns>
        public static BigInteger UnitToValue(string unit)
        {       
            if (isInitialize == false) 
                UnitInitialize(5);
            
            var split = unit.Split('.');
            //소수점에 관한 연산 들어감
            if (split.Length >= 2)
            { 
                var value = BigInteger.Parse(split[0]); 
                var point = BigInteger.Parse((Regex.Replace(split[1], "[^0-9]", "")));
                var unitStr = Regex.Replace(split[1], "[^A-Z]", "");

                if (point == 0) return (_unitsMap[unitStr] * value);
                else
                {
                    var unitValue = _unitsMap[unitStr];
                    return (unitValue * value) + (unitValue/10) * point;
                }
          
            }
            //비소수 연산 들어감
            else
            {
                var value = BigInteger.Parse((Regex.Replace(unit, "[^0-9]", ""))); 
                var unitStr = Regex.Replace(unit, "[^A-Z]", ""); 
                while (_unitsMap.ContainsKey(unitStr) == false) 
                    UnitInitialize(5); 
                var result = _unitsMap[unitStr] * value;

                if (result == 0) 
                    return int.Parse((unit));
                else 
                    return result;
            } 
        }
    }
}