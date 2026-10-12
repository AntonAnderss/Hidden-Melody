using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] GameObject player;
    [SerializeField] InstrumentManager instrumentManager;
    [SerializeField] Instrument[] instruments;

    private void LoadInfo()
    {
        instrumentManager = player.GetComponent<InstrumentManager>();
        instruments = instrumentManager.instruments;
    }

    [Header("Object ID")]
    [SerializeField] string InstrumentID = "IST";
    [SerializeField] string AbilityID = "ABI";
    [SerializeField] string CheckpointID = "POI";
    [SerializeField] string BossID = "BOS";

    /* File */
    private string User {  get { return Environment.UserName; } }
    private string Path { get { return @"C:\Users\"+ User + @"\Documents/Hidden Melody/HD.sav"; } }
    private string FolderPath { get { return @"C:\Users\" + User + @"\Documents/Hidden Melody"; } }

    public void Save()
    {
        if(!File.Exists(Path))
            CreateSaveFile();

        SaveInstruments();
    }

    public void CreateSaveFile()
    {
        /* Creates folder if it doesn't exist*/
        if(!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        /* Create / Override file */
        using (FileStream fileStream = File.Create(Path))
        {
            byte[] info = new UTF8Encoding(true).GetBytes("New File");
            fileStream.Write(info, 0, info.Length);
        }
    }

    private void SaveInstruments()
    {
        LoadInfo();

        int id = 1;
        foreach(Instrument instrument in instruments)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(InstrumentID);
            stringBuilder.Append(id);

            using (FileStream fileStream = File.Create(Path))
            {
                byte[] instrumentInfo = new UTF8Encoding(true).GetBytes(stringBuilder.ToString());
                fileStream.Write(instrumentInfo, 0, instrumentInfo.Length);
            }
        }
        

    }
}
