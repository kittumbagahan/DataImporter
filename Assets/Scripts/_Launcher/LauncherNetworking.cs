using BeardedManStudios;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SQLite4Unity3d;
using System;

public class LauncherNetworking : MonoBehaviour
{

    public bool DontChangeSceneOnConnect = false;
    public string masterServerHost = string.Empty;
    public ushort masterServerPort = 13940;
    public string natServerHost = string.Empty;
    public ushort natServerPort = 13941;
    public bool connectUsingMatchmaking = false;
    public bool useElo = false;
    public int myElo = 0;
    public int eloRequired = 0;

    public GameObject networkManager = null;
    private NetworkManager mgr = null;
    private NetWorker server;
    public NetWorker client;

    //private List<Button> _uiButtons = new List<Button>();
    private bool _matchmaking = false;
    public bool useMainThreadManagerForRPCs = true;
    public bool useInlineChat = false;

    public bool getLocalNetworkConnections = false;

    public bool useTCP = false;

    // kit
    [SerializeField]
    private ushort mPort = 14937;
    private string mIpAddress = "127.0.0.1";
    ClientSendFile mClientSendFile;

    // kit
    public static MainNetwork Instance;
    NetworkingPlayer player;

    public delegate void FindingServer();
    public FindingServer OnFindingServer;
    public delegate void ConnectedToServer();
    public ConnectedToServer OnConnectedToServer;
    public delegate void AssetBundleDataReceived (AssetBundleData assetBundleData);
    public AssetBundleDataReceived OnAssetBundleDataReceived;


    public void Initialize()
    {
        if (!useTCP)
        {
            // Do any firewall opening requests on the operating system
            //NetWorker.PingForFirewall(ushort.Parse(portNumber.text));
            NetWorker.PingForFirewall(mPort);
        }

        if (useMainThreadManagerForRPCs)
            Rpc.MainThreadRunner = MainThreadManager.Instance;

        if (getLocalNetworkConnections)
        {
            NetWorker.localServerLocated += LocalServerLocated;
            //NetWorker.RefreshLocalUdpListings(ushort.Parse(portNumber.text));
            NetWorker.RefreshLocalUdpListings(mPort);
        }
    }
    public void FindServer()
    {
        StartCoroutine("_FindServer");
    }

    public void ClientConnect()
    {
        if (connectUsingMatchmaking)
        {
            ConnectToMatchmaking();
            return;
        }

        if (useTCP)
        {
            client = new TCPClient();
            ((TCPClient)client).Connect(mIpAddress, mPort);
            ((TCPClient)client).Connect(mIpAddress, mPort);
        }
        else
        {
            client = new UDPClient();

            if (natServerHost.Trim().Length == 0)
                ((UDPClient)client).Connect(mIpAddress, mPort);
            else
                ((UDPClient)client).Connect(mIpAddress, mPort, natServerHost, natServerPort);
        }

        // kit, add event                     
        client.serverAccepted += Client_serverAccepted;
        client.disconnected += Client_disconnected;

        Connected(client);
    }

    public void ConnectToMatchmaking()
    {
        if (_matchmaking)
            return;

        // kit
        //SetToggledButtons(false);
        _matchmaking = true;

        if (mgr == null && networkManager == null)
            throw new System.Exception("A network manager was not provided, this is required for the tons of fancy stuff");

        mgr = Instantiate(networkManager).GetComponent<NetworkManager>();

        mgr.MatchmakingServersFromMasterServer(masterServerHost, masterServerPort, myElo, (response) =>
        {
            _matchmaking = false;
            // kit
            Debug.LogFormat("Matching Server(s) count[{0}]", response.serverResponse.Count);

            //TODO: YOUR OWN MATCHMAKING EXTRA LOGIC HERE!
            // I just make it randomly pick a server... you can do whatever you please!
            if (response != null && response.serverResponse.Count > 0)
            {
                MasterServerResponse.Server server = response.serverResponse[UnityEngine.Random.Range(0, response.serverResponse.Count)];
                //TCPClient client = new TCPClient();
                UDPClient client = new UDPClient();
                client.Connect(server.Address, server.Port);
                Connected(client);
            }
        });
    }

    public void Connected(NetWorker networker)
    {
        if (!networker.IsBound)
        {
            Debug.LogError("NetWorker failed to bind");
            return;
        }

        if (mgr == null && networkManager == null)
        {
            Debug.LogWarning("A network manager was not provided, generating a new one instead");
            networkManager = new GameObject("Network Manager");
            mgr = networkManager.AddComponent<NetworkManager>();
        }
        else if (mgr == null)
            mgr = Instantiate(networkManager).GetComponent<NetworkManager>();

        // If we are using the master server we need to get the registration data
        JSONNode masterServerData = null;
        if (!string.IsNullOrEmpty(masterServerHost))
        {
            string serverId = "myGame";
            string serverName = "Forge Game";
            string type = "Deathmatch";
            string mode = "Teams";
            string comment = "Demo comment...";

            masterServerData = mgr.MasterServerRegisterData(networker, serverId, serverName, type, mode, comment, useElo, eloRequired);
        }

        mgr.Initialize(networker, masterServerHost, masterServerPort, masterServerData);

        if (useInlineChat && networker.IsServer)
            SceneManager.sceneLoaded += CreateInlineChat;

        if (networker is IServer)
        {
            if (!DontChangeSceneOnConnect)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                NetworkObject.Flush(networker); //Called because we are already in the correct scene!                

            // kit, is server

            Debug.Log("Connected as server");
        }
        else
        {
          
        }

        //mClientSendFile.enabled = true;
        NetworkManager.Instance.Networker.binaryMessageReceived += ReceivedFile;
    }

    private void Server_disconnected(NetWorker sender)
    {
        Debug.Log("Server disconnected");
        ResetNetwork();
        Reset();
    }

    private void Server_playerConnected(NetworkingPlayer player, NetWorker sender)
    {
        Debug.Log("Player connected");
    }

    private void TestLocalServerFind(NetWorker.BroadcastEndpoints endpoint, NetWorker sender)
    {
        Debug.Log("Address: " + endpoint.Address + ", Port: " + endpoint.Port + ", Server? " + endpoint.IsServer);

        // a server is found
        mPort = endpoint.Port;
        mIpAddress = endpoint.Address;

        isServerFound = true;
        // stop coroutine for finding a server
        //StopCoroutine(_FindServer());
    }

    private void CreateInlineChat(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= CreateInlineChat;
        var chat = NetworkManager.Instance.InstantiateChatManager();
        DontDestroyOnLoad(chat.gameObject);
    }

    private void OnApplicationQuit()
    {
        Quit();
    }

    void Quit()
    {
        Debug.Log("Quit");

        if (getLocalNetworkConnections)
            NetWorker.EndSession();

        //if (server != null) server.Disconnect(true);
        if (server != null) server.Disconnect(false);
    }

    private void Client_disconnected(NetWorker sender)
    {
        Debug.Log(string.Format("{0} is disconnected from server", "Huehue"));
        MainThreadManager.Run(ResetNetwork);
    }

    private void Client_serverAccepted(NetWorker sender)
    {
        Debug.Log(string.Format("{0} is connected to server", "Huehue"));
        Debug.Log("Connected as client");
        if (OnConnectedToServer != null)
        {
            OnConnectedToServer();
        }
        //MainThreadManager.Run(() => SceneManager.LoadScene("Test"));

    }

    private void LocalServerLocated(NetWorker.BroadcastEndpoints endpoint, NetWorker sender)
    {
        Debug.Log("Found endpoint: " + endpoint.Address + ":" + endpoint.Port);
    }

    #region ADDITIONAL LOGIC

    bool isServerFound = false;
    private void Reset()
    {
        isServerFound = false;
        mIpAddress = "";
    }

    private void ResetNetwork()
    {

    }

    private void OnEnable()
    {
        NetWorker.localServerLocated += TestLocalServerFind;

    }

    private void OnDisable()
    {
        // reset
        Reset();
        NetWorker.localServerLocated -= TestLocalServerFind;
    }

    public void AsTeacher()
    {
        // the one who will send the data        

    }

    public void AsStudent()
    {

        StartCoroutine("_FindServer");
    }

    WaitForSeconds wfs = new WaitForSeconds(1.5f);

    IEnumerator _FindServer()
    {
        while (isServerFound == false)
        {
            NetWorker.RefreshLocalUdpListings(mPort);
            Debug.Log("Finding Server " + mPort);
            //callback
            if(OnFindingServer != null)
            {
                OnFindingServer();
            }
            yield return wfs;
        }

        Debug.Log("Server found.");
        ClientConnect();

    }



    #endregion

    void ReceivedFile (NetworkingPlayer player, Binary frame, NetWorker sender)
    {
        Debug.Log ("frame group id:" + frame.GroupId);
        Debug.Log ("Message group id of sync, " + MessageGroupIds.START_OF_GENERIC_IDS + 8);

        if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + 8)
        {
            Debug.Log("Asset bundle");
            AssetBundleData assetBundleData = ConvertToObject(frame.StreamData.CompressBytes());
            Debug.Log("version" + assetBundleData.version);
            Debug.Log("url " + assetBundleData.url);

            MainThreadManager.Run(() =>
            {
                MessageBox.ins.ShowOk("version " + assetBundleData.version + ", url " + assetBundleData.url, MessageBox.MsgIcon.msgInformation, null);
                if(OnAssetBundleDataReceived != null)
                {
                    OnAssetBundleDataReceived (assetBundleData);
                }
            });            
        }
        else
        {
            Debug.Log ("asset bundle return");
            return;
        }
    }

    AssetBundleData ConvertToObject (byte[] byteData)
    {
        BinaryFormatter bin = new BinaryFormatter ();
        MemoryStream ms = new MemoryStream ();
        ms.Write (byteData, 0, byteData.Length);
        ms.Seek (0, SeekOrigin.Begin);

        return (AssetBundleData)bin.Deserialize (ms);
    }
}
