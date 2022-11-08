using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public static class SaveManager
    {
        public static void saveValue(string savekey, string value)
        {
            PlayerPrefs.SetString(savekey, value);
        }

        public static void saveValue(string savekey, int value)
        {
            PlayerPrefs.SetInt(savekey, value);
        }

        public static void saveValue(string savekey, float value)
        {
            PlayerPrefs.SetFloat(savekey, value);
        }

        public static void saveValue(string savekey, bool value)
        {
            int testInt = (value ? 1 : 0);
            PlayerPrefs.SetInt(savekey, testInt);
        }

        public static string getString(string savekey)
        {
            return PlayerPrefs.GetString(savekey, "");
        }

        public static int getInt(string savekey)
        {
            return PlayerPrefs.GetInt(savekey, 0);
        }

        public static float getFloat(string savekey)
        {
            return PlayerPrefs.GetFloat(savekey, 0.0f);
        }

        public static bool getBool(string savekey)
        {
            int boolValue;
            boolValue = PlayerPrefs.GetInt(savekey, 0);
            return boolValue == 1 ? true : false;
        }

        public static void save()
        {
            PlayerPrefs.Save();
        }

        public static void reset()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}