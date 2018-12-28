using BeardedManStudios;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System.IO;
using UnityEngine;

using UnityEngine.UI;

public class SendThatFile : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text txtLog;

    public string filePath;
    private bool sentFile;

    private void Start()
    {
        NetworkManager.Instance.Networker.binaryMessageReceived += ReceiveFile;        
    }

    private void OnEnable()
    {
        NetworkManager.Instance.Networker.binaryMessageReceived += ReceiveFile;
    }

    private void ReceiveFile(NetworkingPlayer player, Binary frame, NetWorker sender)
    {
        // We are looking to read a very specific message
        if (frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + 1)
            return;

        Debug.Log("Reading file!");

        StringBuilder("Reading file!");

        // Read the string from the beginning of the payload
        string fileName = frame.StreamData.GetBasicType<string>();

        Debug.Log("File name is " + fileName);
        StringBuilder("File name is " + fileName + ", path: " + Application.persistentDataPath);

        if (File.Exists(fileName))
        {
            Debug.LogError("The file " + fileName + " already exists!");
            StringBuilder("The file " + fileName + " already exists!");
            return;
        }

        // Write the rest of the payload as the contents of the file and
        // use the file name that was extracted as the file's name
#if UNITY_ANDROID
        File.WriteAllBytes(string.Format("{0}/{1}", Application.persistentDataPath, fileName), frame.StreamData.CompressBytes());
#else
        File.WriteAllBytes(fileName, frame.StreamData.CompressBytes());        
#endif        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))//  && !sentFile
        {
            // kit, temp
            //sentFile = true;

            // Throw an error if this is not the server
            var networker = NetworkManager.Instance.Networker;
            if (!networker.IsServer)
            {
                Debug.LogError("Only the server can send files in this example!");
                StringBuilder("Only the server can send files in this example!");
                return;
            }

            // Throw an error if the file does not exist
            if (!File.Exists(filePath))
            {
                Debug.LogError("The file " + filePath + " could not be found");
                StringBuilder("The file " + filePath + " could not be found");
                return;
            }

            // Prepare a byte array for sending
            BMSByte allData = new BMSByte();

            // Add the file name to the start of the payload
            ObjectMapper.Instance.MapBytes(allData, Path.GetFileName(filePath));

            // Add the data to the payload
            allData.Append(File.ReadAllBytes(filePath));

            // Send the file to all connected clients
            Binary frame = new Binary(
                networker.Time.Timestep,                    // The current timestep for this frame
                false,                                      // We are server, no mask needed
                allData,                                    // The file that is being sent
                Receivers.Others,                           // Send to all clients
                MessageGroupIds.START_OF_GENERIC_IDS + 1,   // Some random fake number
                networker is TCPServer);

            if (networker is UDPServer)
                ((UDPServer)networker).Send(frame, true);
            else
                ((TCPServer)networker).SendAll(frame);

            StringBuilder("sending file");
        }
    }

    void StringBuilder(string pText)
    {
        txtLog.text += pText + "\n";
    }
}
