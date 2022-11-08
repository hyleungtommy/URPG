using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG
{
    public static class Util
    {
        public static void debug(string log)
        {
            Debug.Log(log);
        }

        public static string printStringArray(string[] array)
        {
            return String.Join("",
             new List<string>(array)
             .ConvertAll(i => i.ToString())
             .ToArray());
        }
        public static string printIntArray(int[] array)
        {
            return String.Join("",
             new List<int>(array)
             .ConvertAll(i => i.ToString())
             .ToArray());
        }


        public static int[] fromIntToStringArray(string[] array)
        {
            List<int> l = new List<int>();
            for (int i = 0; i < array.Length; i++)
            {
                l.Add(int.Parse(array[i]));
            }
            return l.ToArray();

        }

        public static int getRequireEXPForLevel(int level)
        {
            float expNeed = Mathf.Floor(level + 300 * Mathf.Pow(1.9f, ((float)level / 4))) / 3;
            return Mathf.FloorToInt(expNeed);
        }

        public static int getFirstDigit(int i)
        {
            while (i >= 10)
                i /= 10;
            return i;
        }

        public static float getSumOfArray(int[] a)
        {
            float sum = 0.0f;
            for (int i = 0; i < a.Length; i++)
                sum += a[i];
            return sum;
        }

        public static int getRandomIndexFrom(int[] a)
        {
            int index = -1;
            float sum = getSumOfArray(a);
            float rnd = UnityEngine.Random.Range(0.0f, sum);
            for (int i = 0; i < a.Length; i++)
            {
                if (rnd < a[i])
                {
                    index = i;
                    break;
                }
                rnd -= a[i];
            }
            return index;
        }

        public static float calculateSum(int[] a)
        {
            float sum = 0.0f;
            for (int i = 0; i < a.Length; i++)
                sum += a[i];
            return sum;
        }

        public static int getRandomIndexFrom(int[] a, float sum)
        {
            int index = -1;
            float rnd = UnityEngine.Random.Range(0.0f, sum);
            for (int i = 0; i < a.Length; i++)
            {
                if (rnd < a[i])
                {
                    index = i;
                    break;
                }
                rnd -= a[i];
            }
            return index;
        }

        public static int calculateProductionSkillEXPNeed(int level)
        {
            //float expNeed = Mathf.Floor(level + 30 * Mathf.Pow(1.9f, ((float)level / 4))) / 3;
            return level * 10;
        }

        public static float ceilTo10(int n)
        {
            return (n / 10) * 10 + 10;
        }

        public static string ToRomanNum(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRomanNum(number - 1000);
            if (number >= 900) return "CM" + ToRomanNum(number - 900);
            if (number >= 500) return "D" + ToRomanNum(number - 500);
            if (number >= 400) return "CD" + ToRomanNum(number - 400);
            if (number >= 100) return "C" + ToRomanNum(number - 100);
            if (number >= 90) return "XC" + ToRomanNum(number - 90);
            if (number >= 50) return "L" + ToRomanNum(number - 50);
            if (number >= 40) return "XL" + ToRomanNum(number - 40);
            if (number >= 10) return "X" + ToRomanNum(number - 10);
            if (number >= 9) return "IX" + ToRomanNum(number - 9);
            if (number >= 5) return "V" + ToRomanNum(number - 5);
            if (number >= 4) return "IV" + ToRomanNum(number - 4);
            if (number >= 1) return "I" + ToRomanNum(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        public static string getThreeDigitNumber(int num)
        {
            if (num < 10) return "00" + num;
            else if (num > 10 && num < 100) return "0" + num;
            else return num.ToString();
        }


    }
}