/* Author: Anton Andersson */
using UnityEngine;

public static class ISConverter
{
    /// <summary>
    /// Converts an int into a string, only works for number of a maximum of 2 digits
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static string IntToString(int i)
    {
        string s = i.ToString();
        ValidateInput(s);

        if (s.Length == 1)
            return "0" + s;
        else
            return s;
    }

    public static int StringToInt(string s)
    {
        ValidateInput(s);

        //Debug.LogError("StringToInt not implemented, returned default value 0");
        return int.Parse(s);
    }

    private static bool ValidateInput(string input)
    {
        if(input == null) 
            return false;

        if (input.Length > 2 || input.Length < 1)
            return false;

        return true;

    }
}
