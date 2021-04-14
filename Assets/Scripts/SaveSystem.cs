using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveSetting(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        PlayerPrefs.Save();
    }

    public static void ClearSettings()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public static float GetSetting(string name)
    {
        float value = PlayerPrefs.GetFloat(name, 1);

        return value;
    }
}
