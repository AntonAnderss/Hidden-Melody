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
        public string[] instruments;
        public string[] checkpoints;
        public string[] bosses;
    }

    public void LoadSave(string path, ref Instrument[] instruments,  string InstrumentID)
    {
        Data data = ReformatData(path, InstrumentID);
        StringBuilder sb = new StringBuilder();
        sb.Append("Instruments:\n");
        LoadInstruments(ref instruments, data.instruments);
    }

    void LoadInstruments(ref Instrument[] instrument, string[] data)
    {
        foreach(string instrumentData in data)
        {
            int index = ISConverter.StringToInt(GetIndex(instrumentData)) - 1; //Gets the index of the instrument in the array

            /* If instrument is collected*/
            char collectionStatus = GetInstrumentCollectionStatus(instrumentData); 
            if (IncorrectFormat(collectionStatus, 'C', 'U')) //Incorrect format
                Debug.LogError("Save file is incorrect for Instrument_" + index.ToString());
            instrument[index].Collected = collectionStatus == 'C' ? true : false;

            /* If each ability is unlocked */
            char[] abilityStatus = GetAbilitiesStatus(instrumentData);
            for (int i  = 0; i < abilityStatus.Length; i++)
            {
                char status = abilityStatus[i];
                if (IncorrectFormat(status, 'x', '-'))  //Incorrect format
                    Debug.LogError("Save file is incorrect");

                instrument[index].abilities[i].Unlocked = status == 'x' ? true : false;
            }
        }
    }

    /* Get Data from File */
    Data ReformatData(string path, string InstrumentID)
    {
        string[] data = GetData(path);
        List<string> instruments = new List<string>(), checkpoints = new List<string>(), bosses = new List<string>();

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i][0] == '#') //If comment, ignore line and continue
                continue;

            string id = GetID(data[i]);

            if(id == InstrumentID)
                instruments.Add(data[i]);
        }
        Data result = new Data();
        result.instruments = ListToArray(instruments);
        result.checkpoints = ListToArray(checkpoints);
        result.bosses = ListToArray(bosses);

        return result;
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
    string GetID(string line) { return line.Substring(0, 3); }
    string GetIndex(string line) { return line.Substring(3, 2); }
    string[] ListToArray(List<string> list) { return  list.ToArray(); }

    /* Instrument Helpers */
    char GetInstrumentCollectionStatus(string instrumentData) { return instrumentData[5]; }
    char[] GetAbilitiesStatus(string instrumentData) 
    {
        List<char> abilites = new List<char>();
        for (int i = 6; i < instrumentData.Length; i++)
            abilites.Add(instrumentData[i]);

        return abilites.ToArray();
    }

    bool IncorrectFormat(char c, char a, char b) { return c != a && c != b; }
    void PrintInstrumentInfo(string instrumentData)
    {
        string s = String.Empty;
        foreach (char c in GetAbilitiesStatus(instrumentData)) s += c;

        Debug.Log(GetID(instrumentData) + GetIndex(instrumentData) + GetInstrumentCollectionStatus(instrumentData) + s);
    }

}
