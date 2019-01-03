using BeardedManStudios;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System.IO;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using SQLite4Unity3d;
using System;
using SFB;

public class ClientSendFile : MonoBehaviour
{
    // kit
    [SerializeField]
    UnityEngine.UI.Text txtTest;
    [SerializeField]
    UnityEngine.UI.Text txtData;
    [SerializeField]
    UnityEngine.UI.Button btnSave;

    string importedData;

    public enum MessageGroup
    {
        Insert = 2,
        Update = 3,
        Book_UpdateReadCount = 4,
        Book_UpdateReadToMeCount = 5,
        Book_UpdateAutoReadCount = 6,
        Sync = 7,
        CSV = 9
    }

    Queue<NetworkData> networkQueue;

    //DataService dataService;

    private void Start()
    {        
        NetworkManager.Instance.Networker.binaryMessageReceived += ReceiveFile;
        networkQueue = new Queue<NetworkData>();

        // create database connection
        //dataService = new DataService();
        btnSave.onClick.AddListener(Save);
    }
    
    private void Save()
    {
        if (txtData.text == null || txtData.text == "")
            return;

        string path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "csv");
        File.WriteAllText(path, importedData, System.Text.Encoding.Default);
    }

    private void ReceiveFile(NetworkingPlayer player, Binary frame, NetWorker sender)
    {
        Debug.Log("frame group id:" + frame.GroupId);
        Debug.Log("Message group id of sync, " + MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Sync);

        if (frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateAutoReadCount &&
        frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateReadCount &&
        frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateReadToMeCount &&
        frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Insert &&
        frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Update &&
        frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Sync &&
        frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.CSV)
            return;

        // sync message
        if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Sync)
        {
            #region sync
            Debug.Log("Reading file! Sync");

            //StringBuilder("Reading file!");        

            // Read the string from the beginning of the payload
            string fileName = frame.StreamData.GetBasicType<string>();

            Debug.Log("sync file name " + fileName);

            MainThreadManager.Run(() => Debug.Log("File name is " + fileName + ", path: " + Application.persistentDataPath));

            MainThreadManager.Run(() =>
            {
                try
                {
                    // close any open database
                    DataService.Close();

                    if (File.Exists(Application.persistentDataPath + "/" + fileName))
                    {
                        Debug.LogError("The file " + fileName + " already exists!");
                        //StringBuilder("The file " + fileName + " already exists!"); 
                        File.Move(Application.persistentDataPath + "/" + fileName, Application.persistentDataPath + "/backup.db");
                        //return;
                    }

                    // Write the rest of the payload as the contents of the file and
                    // use the file name that was extracted as the file's name 
                    File.WriteAllBytes(string.Format("{0}/{1}", Application.persistentDataPath, fileName), frame.StreamData.CompressBytes());
                    // set the active db name
                    DataService.SetDbName(fileName);
                    // delete backup.db
                    File.Delete(Application.persistentDataPath + "/backup.db");
                    // load section selection
                    MainNetwork.Instance.LoadSectionSelection();
                    // get active db change name to backup
                    
                }

                catch (IOException ex)
                {
                    Debug.LogError("file exception! " + ex.Message);
                }
            }
            );

            // Write the rest of the payload as the contents of the file and
            // use the file name that was extracted as the file's name    
            //MainThreadManager.Run(() => File.WriteAllBytes(string.Format("{0}/{1}", Application.persistentDataPath, fileName), frame.StreamData.CompressBytes()));
            // set the active db name
            //MainThreadManager.Run(() => DataService.SetDbName(fileName));
            // load section selection
            //MainThreadManager.Run(MainNetwork.Instance.LoadSectionSelection);
            #endregion

            //MainThreadManager.Run(() =>
            //{
            //    Debug.Log("Sync enter");
            //    NetworkModel networkModel = NetworkModelToObject(frame.StreamData.CompressBytes());

            //    Debug.Log("Activity model, section count " + networkModel.lstActivityModel.Count);
            //    Debug.Log("Student model, student count " + networkModel.lstStudentModel.Count);
            //    Debug.Log("Sync exit");
            //});
        }
        else if(frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.CSV)
        {
            Debug.Log("CSV");
            MainThreadManager.Run(() =>
            {
                //System.Text.Encoding enc = System.Text.Encoding.Default;
                //importedData = enc.GetString(frame.StreamData.CompressBytes());   
                importedData = System.Text.Encoding.UTF8.GetString(frame.StreamData.CompressBytes());
                Debug.Log("data length " + frame.StreamData.CompressBytes().Length);
                importedData = importedData.Remove(importedData.Length - 8);
                Debug.Log("got data!\n" + importedData);
                txtData.text = importedData;
                btnSave.interactable = true;
            });
        }
        else
        {

            //if (frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateAutoReadCount &&
            //frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateReadCount &&
            //frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateReadToMeCount &&
            //frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Insert &&
            //frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Update)
            //return;        

            Debug.Log ("Reading file!");

            // kit
            Debug.Log (string.Format ("Insert group id {0}\nUpdate group id {1}",
                (int)MessageGroup.Insert,
                (int)MessageGroup.Update));
            Debug.Log (string.Format ("Message group {0}", frame.GroupId));

            NetworkData networkData = ConvertToObject (frame.StreamData.CompressBytes ());
            // add to queue for execution
            networkQueue.Enqueue (networkData);

            while (networkQueue.Count > 0)
            {
                // kit
                Debug.Log ("Queue count " + networkQueue.Count);

                if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Update ||
                   frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Insert)
                {
                    // activity model

                    string module = networkQueue.Peek ().activity_module;
                    string description = networkQueue.Peek ().activity_description;
                    int set = networkQueue.Peek ().activity_set;
                    string book_description = networkQueue.Peek ().book_description;

                    DataService.Open();
                    var activity = DataService._connection.Table<ActivityModel> ().Where (x => x.Module == module &&
                                                                                          x.Description == description &&
                                                                                          x.Set == set).FirstOrDefault ();

                    if (activity == null)
                    {
                        var _activity = new ActivityModel
                        {
                            BookId = DataService._connection.Table<BookModel> ().Where (x => x.Description == book_description).FirstOrDefault ().Id,
                            Description = networkQueue.Peek ().activity_description,
                            Module = networkQueue.Peek ().activity_module,
                            Set = networkQueue.Peek ().activity_set
                        };
                        DataService._connection.Insert (_activity);
                    }
                    DataService.Close();
                }

                // if message is insert
                if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Insert)
                {

                    // kit
                    Debug.Log ("Insert");
                    // handle insert here, check first item in queue
                    StudentActivityModel studentActivityModel = new StudentActivityModel
                    {
                        Id = networkQueue.Peek ().studentActivity_ID,
                        SectionId = networkQueue.Peek ().studentActivity_sectionId,
                        StudentId = networkQueue.Peek ().studentActivity_studentId,
                        BookId = networkQueue.Peek ().studentActivity_bookId,
                        ActivityId = networkQueue.Peek ().studentActivity_activityId,
                        Grade = networkQueue.Peek ().studentActivity_grade,
                        PlayCount = networkQueue.Peek ().studentActivity_playCount
                    };

                    // kit
                    Debug.Log (string.Format ("ID {0}\nSection ID {1}\nStudent ID {2}\nBook ID {3}\nActivity ID {4}\nGrade {5}\nPlay Count {6}",
                        studentActivityModel.Id,
                        studentActivityModel.SectionId,
                        studentActivityModel.StudentId,
                        studentActivityModel.BookId,
                        studentActivityModel.ActivityId,
                        studentActivityModel.Grade,
                        studentActivityModel.PlayCount));

                    DataService.Open();
                    DataService._connection.Insert (studentActivityModel);
                    DataService.Close();
                    networkQueue.Dequeue ();

                }
                else
                {
                    // handle update here
                    string command = "";
                    if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Update)
                    {
                        command = string.Format ("Update StudentActivityModel set Grade='{0}'," +
                        "PlayCount='{1}' where Id='{2}'", networkData.studentActivity_grade, networkData.studentActivity_playCount, networkData.studentActivity_ID);
                    }
                    else if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateReadCount)
                    {
                        Debug.Log ("Update read count");
                        if (CreateStudentBookModel (networkQueue.Peek ()) == false)
                        {
                            command = string.Format ("Update StudentBookModel set ReadCount='{0}' where id='{1}'",
                                networkData.studentBook_readCount,
                                networkData.studentBook_Id);

                            DataService.Open();
                            DataService._connection.Execute (command);
                            DataService.Close();
                        }
                    }
                    else if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateReadToMeCount)
                    {
                        Debug.Log ("Update read to me count");
                        if (CreateStudentBookModel (networkQueue.Peek ()) == false)
                        {
                            command = string.Format ("Update StudentBookModel set ReadToMeCount='{0}' where id='{1}'",
                            networkData.studentBook_readToMeCount,
                            networkData.studentBook_Id);

                            DataService.Open();
                            DataService._connection.Execute (command);
                            DataService.Close();
                        }
                    }
                    else if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateAutoReadCount)
                    {
                        Debug.Log ("Update auto read count");
                        if (CreateStudentBookModel (networkQueue.Peek ()) == false)
                        {
                            command = string.Format ("Update StudentBookModel set AutoReadCount='{0}' where id='{1}'",
                            networkData.studentBook_autoReadCount,
                            networkData.studentBook_Id);

                            DataService.Open();
                            DataService._connection.Execute (command);
                            DataService.Close();
                        }
                    }

                    // kit
                    Debug.Log ("Update");
                    Debug.Log (command);

                    //dataService._connection.Execute(command);
                    networkQueue.Dequeue ();
                }
            }
        }
        // kit
        Debug.Log("Queue empty");

		// kit, test data display text
		//MainThreadManager.Run( () => GameObject.FindGameObjectWithTag("data").GetComponent<UnityEngine.UI.Text>().text = string.Format("Name: {0}\nAge: {1}\nSection: {2}\n\n", networkData.name, networkData.age, networkData.section));

        // Write the rest of the payload as the contents of the file and
        // use the file name that was extracted as the file's name    

        //MainThreadManager.Run(() => File.WriteAllBytes(string.Format("{0}/{1}", Application.persistentDataPath, fileName), frame.StreamData.CompressBytes()));        
    }

    public void SendData(NetworkData pNetworkData, MessageGroup messageGroup)
    {       
        // Throw an error if this is not the server
        var networker = NetworkManager.Instance.Networker;

        // event when file is sent        

        if (networker.IsServer)
        {
            Debug.LogError("Only the client can send files in this example!");
            return;
        }      

		byte[] allData = { };


		// convert pData as byte[]
		BinaryFormatter binFormatter = new BinaryFormatter();
		MemoryStream memStream = new MemoryStream ();
		binFormatter.Serialize (memStream, pNetworkData);

		allData = memStream.ToArray ();

		Debug.Log ("allData " + allData.Length);		

//        // Prepare a byte array for sending
//        BMSByte allData = new BMSByte();        
//
//        // Add the file name to the start of the payload        
//        ObjectMapper.Instance.MapBytes(allData);        

        // Send the file to all connected clients
        Binary frame = new Binary(
            networker.Time.Timestep,                    // The current timestep for this frame
            false,                                      // We are server, no mask needed
            allData,                                    // The file that is being sent
            Receivers.Others,                           // Send to all clients
            MessageGroupIds.START_OF_GENERIC_IDS + (int)messageGroup,   // Some random fake number
            networker is TCPServer);

//        if (networker is UDPServer)
//            ((UDPServer)networker).Send(frame, true);
//        else
//            ((TCPServer)networker).SendAll(frame);

		if (networker is UDPClient)
			((UDPClient)networker).Send (frame, true);
		else
			((TCPClient)networker).Send (frame);
		
						
        //StringBuilder("sending file");
    }

    public void SendDatabase(string pFilePath)
    {
        // test
        //MessageBox.ins.ShowOk (string.Format ("File path is {0}", pFilePath), MessageBox.MsgIcon.msgInformation, null);
        //return;
        Debug.Log ("File Path " + pFilePath);

        // kit, temp
        //sentFile = true;

        // pass file path value to private variable
         string filePath = pFilePath;

        // Throw an error if this is not the server
        var networker = NetworkManager.Instance.Networker;

        // event when file is sent        

        if (!networker.IsServer)
        {
            Debug.LogError ("Only the server can send files in this example!");
            return;
        }

        // Throw an error if the file does not exist
        if (!File.Exists (filePath))
        {
            Debug.LogError ("The file " + filePath + " could not be found");
            return;
        }

        // Prepare a byte array for sending
        BMSByte allData = new BMSByte ();

        // Add the file name to the start of the payload
        ObjectMapper.Instance.MapBytes (allData, Path.GetFileName (filePath));

        // Add the data to the payload
        allData.Append (File.ReadAllBytes (filePath));

        // Send the file to all connected clients
        Binary frame = new Binary (
            networker.Time.Timestep,                    // The current timestep for this frame
            false,                                      // We are server, no mask needed
            allData,                                    // The file that is being sent
            Receivers.Others,                           // Send to all clients
            MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Sync,   // Some random fake number
            networker is TCPServer);

        if (networker is UDPServer)
            ((UDPServer)networker).Send (frame, true);
        else
            ((TCPServer)networker).SendAll (frame);        
    }
    NetworkModel networkModel;
    //public void SendDatabase2()
    //{
    //    //NetworkModel networkModel = new NetworkModel();

    //    DataService ds = new DataService();
    //    SQLiteCommand command = ds._connection.CreateCommand("select * from ActivityModel");

    //    networkModel.lstActivityModel = command.ExecuteQuery<ActivityModel>();

    //    command = ds._connection.CreateCommand("select * from StudentModel");
    //    networkModel.lstStudentModel = command.ExecuteQuery<StudentModel>();

    //    ds._connection.Close();

    //    // Throw an error if this is not the server
    //    var networker = NetworkManager.Instance.Networker;

    //    // event when file is sent        

    //    if (!networker.IsServer)
    //    {
    //        Debug.LogError("Only the client can send files in this example!");
    //        return;
    //    }

    //    byte[] allData = { };


    //    // convert pData as byte[]
    //    BinaryFormatter binFormatter = new BinaryFormatter();
    //    MemoryStream memStream = new MemoryStream();
    //    binFormatter.Serialize(memStream, networkModel);

    //    allData = memStream.ToArray();

    //    Debug.Log("allData " + allData.Length);

    //    //        // Prepare a byte array for sending
    //    //        BMSByte allData = new BMSByte();        
    //    //
    //    //        // Add the file name to the start of the payload        
    //    //        ObjectMapper.Instance.MapBytes(allData);        

    //    // Send the file to all connected clients
    //    Binary frame = new Binary(
    //        networker.Time.Timestep,                    // The current timestep for this frame
    //        false,                                      // We are server, no mask needed
    //        allData,                                    // The file that is being sent
    //        Receivers.Others,                           // Send to all clients
    //        MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Sync,   // Some random fake number
    //        networker is TCPServer);

    //    //        if (networker is UDPServer)
    //    //            ((UDPServer)networker).Send(frame, true);
    //    //        else
    //    //            ((TCPServer)networker).SendAll(frame);

    //    if (networker is UDPClient)
    //        ((UDPClient)networker).Send(frame, true);
    //    else
    //        ((TCPClient)networker).Send(frame);
    //}

    NetworkData ConvertToObject(byte[] byteData)
	{
		BinaryFormatter bin = new BinaryFormatter ();
		MemoryStream ms = new MemoryStream ();
		ms.Write (byteData, 0, byteData.Length);
		ms.Seek (0, SeekOrigin.Begin);

		return (NetworkData)bin.Deserialize (ms);
	}    

    //NetworkModel NetworkModelToObject(byte[] byteData)
    //{
    //    BinaryFormatter bin = new BinaryFormatter();
    //    MemoryStream ms = new MemoryStream();
    //    ms.Write(byteData, 0, byteData.Length);
    //    ms.Seek(0, SeekOrigin.Begin);

    //    return (NetworkModel)bin.Deserialize(ms);
    //}

    bool CreateStudentBookModel (NetworkData pNetworkData)
    {
        // check student book model
        DataService.Open();
        StudentBookModel studentModel = DataService._connection.Table<StudentBookModel> ().Where
        (
           x => x.SectionId == pNetworkData.studentBook_SectionId &&
           x.StudentId == pNetworkData.studentBook_StudentId &&
           x.BookId == pNetworkData.studentBook_bookId
        ).FirstOrDefault ();

        if (studentModel == null)
        {
            Debug.Log ("Create student book model");
            StudentBookModel studentBookModel = new StudentBookModel
            {
                SectionId = pNetworkData.studentBook_SectionId,
                StudentId = pNetworkData.studentBook_StudentId,
                BookId = pNetworkData.studentBook_bookId,
                ReadCount = pNetworkData.studentBook_readCount,
                ReadToMeCount = pNetworkData.studentBook_readToMeCount,
                AutoReadCount = pNetworkData.studentBook_autoReadCount
            };
            DataService._connection.Insert (studentBookModel);

            return true;
            DataService.Close();
        }
        else
        {
            Debug.Log ("Create student book model update");
            DataService.Close();
            return true;
        }
    }

    // kit, test
    void DebugText(string pText)
    {
        txtTest.text += pText;
    }
}
