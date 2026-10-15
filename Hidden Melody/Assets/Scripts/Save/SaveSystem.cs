using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    Saver saver = new Saver();
    Loader loader = new Loader();

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

        LoadInfo();
        
        using (FileStream fileStream = File.OpenWrite(Path)) 
        {
            byte[] saveData = new UTF8Encoding(true).GetBytes(saver.GetSave(instruments, InstrumentID));

            fileStream.Write(saveData, 0, saveData.Length);

            Debug.Log("Saved");
        }
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
}
