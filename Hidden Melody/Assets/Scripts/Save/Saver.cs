/* Author: Anton Andersson */
using System.Text;
using UnityEngine;

public class Saver
{
    StringBuilder stringBuilder = new StringBuilder();
    private void Reset() { stringBuilder.Clear(); }
    StringBuilder tempBuilder = new StringBuilder();
    private void ResetTemp() { tempBuilder.Clear(); }

    public string GetSave(Instrument[] instruments, string InstrumentID)
    {
        Reset();

        SaveInstruments(instruments, InstrumentID);

        Debug.Log(instruments.Length);
        Debug.Log(stringBuilder.ToString());
        return stringBuilder.ToString();
    }

    private void SaveInstruments(Instrument[] instruments, string InstrumentID)
    {
        int counter = 1;
        foreach (Instrument instrument in instruments)
        {
            tempBuilder.Append(InstrumentID);
            tempBuilder.Append(ISConverter.IntToString(counter));

            /* Save if instrument unlocked here */
            if (instrument.Collected)
                tempBuilder.Append("C");
            else
                tempBuilder.Append("U");

            /* Save abilities here */
            foreach(Ability ability in instrument.abilities)
            {
               if(ability.Unlocked)
                    tempBuilder.Append("x");
                else
                    tempBuilder.Append("-");
            }
   
            stringBuilder.AppendLine(tempBuilder.ToString());
            ResetTemp();
            counter++;
        }
    }
}
