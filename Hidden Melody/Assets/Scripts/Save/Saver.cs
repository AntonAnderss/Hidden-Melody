/* Author: Anton Andersson */
using System.Text;

public class Saver
{
    StringBuilder stringBuilder = new StringBuilder();
    private void Reset() { stringBuilder.Clear(); }
    StringBuilder tempBuilder = new StringBuilder();
    private void ResetTemp() { tempBuilder.Clear(); }

    const string InstrumentInfo = "#Instruments\n#C = Collected, U = Uncollected\n#(Abilities) x = Unlocked, - = Locked\n";
    const string CheckpointInfo = "#Checkpoints\n";
    const string BossInfo = "#Bosses\n";

    public string CreateSave(Instrument[] instruments, string InstrumentID)
    {
        Reset();
        ResetTemp();

        SaveInstruments(instruments, InstrumentID);
        SaveCheckpoints();
        SaveBosses();

        return stringBuilder.ToString();
    }

    private void SaveInstruments(Instrument[] instruments, string InstrumentID)
    {
        tempBuilder.Append(InstrumentInfo);

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
    private void SaveCheckpoints()
    {
        tempBuilder.Append(CheckpointInfo);
        stringBuilder.Append(tempBuilder.ToString());
        ResetTemp();
    }
    private void SaveBosses()
    {
        tempBuilder.Append(BossInfo);
        stringBuilder.Append(tempBuilder.ToString());
        ResetTemp();
    }
}
