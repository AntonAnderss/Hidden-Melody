/* Author: Anton Andersson */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Loader
{
    struct Data
    {
        float version;
        string[] instruments;
        string[] checkpoints;
        string[] bosses;
    }

    public void LoadSave(string path, ref Instrument[] instruments)
    {
        string[] data = GetData(path);
    }

    void LoadInstruments(ref Instrument[] instrument, string[] data)
    {
        //
    }

    /* Get Data from File */
    Data ReformatData(string path)
    {
        string[] data = GetData(path);
        float version;
        List<string> instruments, checkpoints, bosses;  

        for(int i = 0; i < data.Length; i++)
        {

        }

        return 
    }

    string[] GetData(string path)
    {
        string[] result = new string[GetLineCount(path)]; 

        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            int i = 0;

            while ((line = sr.ReadLine()) != null) { result[i++] = line; }
        }

        return result;
    }

    int GetLineCount(string path) 
    {
        int i = 0;
        using (StreamReader sr = new StreamReader(path)) { while (sr.ReadLine() != null) { i++; } }
        return i;
    }

    /* Helper */

}
