using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.IO;

public class DatabaseController
{

    string DatabaseDirectory;// = "Assets/StreamingAssets/";

    string schoolName = "default";
    string date;
    string activeDbName;
    DirectoryInfo directoryInfo;
    FileInfo[] files;

    public DatabaseController()
    {

        date = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss_tt");

        DatabaseDirectory = Application.persistentDataPath;
        directoryInfo = new DirectoryInfo(DatabaseDirectory);
        files = directoryInfo.GetFiles("*.db");

    }

    public void SetDatabase(string databaseName)
    {
        PlayerPrefs.SetString("activeDatabase", databaseName);
        MessageBox.ins.ShowOk("Db is set to " + databaseName, MessageBox.MsgIcon.msgInformation, null);
    }

    public void MakeBackUp()
    {
        activeDbName = PlayerPrefs.GetString("activeDatabase");
        if (File.Exists(DatabaseDirectory + schoolName + "-" + date + ".db"))
        {
            //avoid making several backup with an interval of seconds
            //return please wait for 1 seconds to backup again
            MessageBox.ins.ShowOk("Please try again after 10 seconds.", MessageBox.MsgIcon.msgInformation, null);
        }
        else
        {
            //FileUtil.CopyFileOrDirectory ("sourcepath/YourFileOrFolder", "destpath/YourFileOrFolder");
            //#if UNITY_EDITOR
            //            FileUtil.CopyFileOrDirectory(DatabaseDirectory + activeDbName, DatabaseDirectory + schoolName + "-" + date + ".db");
            //#endif
            File.Copy(DatabaseDirectory + "/"+ activeDbName, DatabaseDirectory + "/" + schoolName + "-" + date + ".db");
            MessageBox.ins.ShowOk(schoolName + "-" + date + ".db" + " Created!", MessageBox.MsgIcon.msgInformation, null);
        }

    }

    public List<string> GetFileNames()
    {
        List<string> filenames = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
#if UNITY_EDITOR
            filenames.Add(files[i].ToString().Remove(0, files[i].ToString().Length - (schoolName + "-" + date + ".db").ToString().Length));
#else
            filenames.Add(files[i].ToString());
#endif
        }
        return filenames;
    }

    public bool FindDatabase(string db)
    {
        if (File.Exists(DatabaseDirectory + "/" + db))
        {
            return true;
        }

        return false;
    }

    public void CreateDB(string dbName)
    {
        Directory.CreateDirectory(DatabaseDirectory + "/system");
        if (!File.Exists(DatabaseDirectory + "/system/" + dbName))
        {
            File.Create(DatabaseDirectory + "/system/" + dbName);
        }
       

       
    }
}
