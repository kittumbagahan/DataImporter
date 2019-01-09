using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Lobby;
using BeardedManStudios.SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

using System.Net.Sockets;

public class DataImportNetwork : MonoBehaviour
{

    //public InputField ipAddress = null;
    //public InputField portNumber = null;
    public bool DontChangeSceneOnConnect = false;
    public string masterServerHost = string.Empty;
    public ushort masterServerPort = 12940;
    public string natServerHost = string.Empty;
    public ushort natServerPort = 12941;
    public bool connectUsingMatchmaking = false;
    public bool useElo = false;
    public int myElo = 0;
    public int eloRequired = 0;

    public GameObject networkManager = null;
    private NetworkManager mgr = null;
    private NetWorker server, client;

    //private List<Button> _uiButtons = new List<Button>();
    private bool _matchmaking = false;
    public bool useMainThreadManagerForRPCs = true;
    public bool useInlineChat = false;

    public bool getLocalNetworkConnections = false;

    public bool useTCP = false;

    // kit
    [SerializeField]
    private ushort mPort = 12937;
    private string mIpAddress = "127.0.0.1";
    [SerializeField] Button btnReceiver, btnSender;
    ClientSendFile mClientSendFile;

    // kit
    public static DataImportNetwork Instance;
    NetworkingPlayer player;

    private void Start ()
    {
        //ipAddress.text = "127.0.0.1";
        //portNumber.text = "15937";          

        //for (int i = 0; i < ToggledButtons.Length; ++i)
        //{
        //  Button btn = ToggledButtons[i].GetComponent<Button>();
        //  if (btn != null)
        //      _uiButtons.Add(btn);
        //}                                                    

        mClientSendFile = GetComponent<ClientSendFile> ();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        }
        else
        {
            Destroy (gameObject);
        }

        if (!useTCP)
        {
            // Do any firewall opening requests on the operating system
            //NetWorker.PingForFirewall(ushort.Parse(portNumber.text));
            NetWorker.PingForFirewall (mPort);
        }

        if (useMainThreadManagerForRPCs)
            Rpc.MainThreadRunner = MainThreadManager.Instance;

        if (getLocalNetworkConnections)
        {
            NetWorker.localServerLocated += LocalServerLocated;
            //NetWorker.RefreshLocalUdpListings(ushort.Parse(portNumber.text));
            NetWorker.RefreshLocalUdpListings (mPort);
        }
    }

    private void LocalServerLocated (NetWorker.BroadcastEndpoints endpoint, NetWorker sender)
    {
        Debug.Log ("Found endpoint: " + endpoint.Address + ":" + endpoint.Port);
    }

    public void Connect ()
    {
        //ushort port;
        //if(!ushort.TryParse(portNumber.text, out port))
        //{
        //  Debug.LogError("The supplied port number is not within the allowed range 0-" + ushort.MaxValue);
        //      return;
        //}

        //NetWorker client;

        if (useTCP)
        {
            client = new TCPClient ();

            //((TCPClient)client).Connect(ipAddress.text, (ushort)port);
            ((TCPClient)client).Connect (mIpAddress, mPort);
            ((TCPClient)client).Connect (mIpAddress, mPort);
        }
        else
        {
            client = new UDPClient ();


            if (natServerHost.Trim ().Length == 0)
                //((UDPClient)client).Connect(ipAddress.text, (ushort)port);
                ((UDPClient)client).Connect (mIpAddress, mPort);
            else
                //((UDPClient)client).Connect(ipAddress.text, (ushort)port, natServerHost, natServerPort);
                ((UDPClient)client).Connect (mIpAddress, mPort, natServerHost, natServerPort);
        }

        // kit, add event                     
        client.serverAccepted += Client_serverAccepted;
        client.disconnected += Client_disconnected;

        Connected (client);
    }

    private void Client_disconnected (NetWorker sender)
    {
        Debug.Log (string.Format ("{0} is disconnected from server", "Huehue"));
        MainThreadManager.Run (ResetNetwork);
    }

    private void Client_serverAccepted (NetWorker sender)
    {
        Debug.Log (string.Format ("{0} is connected to server", "Huehue"));
        //MainThreadManager.Run(() => SceneManager.LoadScene("Test"));
        //MainThreadManager.Run(() => btnStudent.GetComponent<StudentLogIn>().LogIn());
    }

    public void Host ()
    {
        if (useTCP)
        {
            server = new TCPServer (100);

            ((TCPServer)server).Connect ();
        }
        else
        {
            server = new UDPServer (100);

            if (natServerHost.Trim ().Length == 0)
            //((UDPServer)server).Connect(ipAddress.text, ushort.Parse(portNumber.text));
            {
                try
                {
                    ((UDPServer)server).Connect (mIpAddress, mPort);
                }
                catch (BaseNetworkException ex)
                {
                    MessageBox.ins.ShowOk ("A server already exist. Please check your network connection.", MessageBox.MsgIcon.msgInformation, null);
                }
            }
            else
            //((UDPServer)server).Connect(port: ushort.Parse(portNumber.text), natHost: natServerHost, natPort: natServerPort);
            {
                try
                {
                    ((UDPServer)server).Connect (port: mPort, natHost: natServerHost, natPort: natServerPort);
                }
                catch (BaseNetworkException ex)
                {
                    MessageBox.ins.ShowOk ("A server already exist. Please check your network connection.", MessageBox.MsgIcon.msgInformation, null);
                }
            }
        }

        server.playerTimeout += (player, sender) =>
        {
            Debug.Log ("Player " + player.NetworkId + " timed out");
        };
        //LobbyService.Instance.Initialize(server);
        server.playerConnected += Server_playerConnected;
        server.disconnected += Server_disconnected;
        server.playerAccepted += Server_playerAccepted;
        // kit, event                      
        Connected (server);
    }

    private void Server_playerAccepted (NetworkingPlayer player, NetWorker sender)
    {
        Debug.Log ("player is accepted");        
    }

    private void Server_disconnected (NetWorker sender)
    {
        Debug.Log ("Server disconnected");
        ResetNetwork ();
        Reset ();
    }

    private void Server_playerConnected (NetworkingPlayer player, NetWorker sender)
    {
        Debug.Log ("Player connected");
        // send latest db to client
        //clientSendFile.SendDatabase(Application.persistentDataPath + "/" + DataService.DbName());
    }

    private void TestLocalServerFind (NetWorker.BroadcastEndpoints endpoint, NetWorker sender)
    {
        Debug.Log ("Address: " + endpoint.Address + ", Port: " + endpoint.Port + ", Server? " + endpoint.IsServer);

        // a server is found
        mPort = endpoint.Port;
        mIpAddress = endpoint.Address;

        isServerFound = true;
        // stop coroutine for finding a server
        //StopCoroutine(_FindServer());
    }

    public void Connected (NetWorker networker)
    {
        if (!networker.IsBound)
        {
            Debug.LogError ("NetWorker failed to bind");
            return;
        }

        if (mgr == null && networkManager == null)
        {
            Debug.LogWarning ("A network manager was not provided, generating a new one instead");
            networkManager = new GameObject ("Network Manager");
            mgr = networkManager.AddComponent<NetworkManager> ();
        }
        else if (mgr == null)
            mgr = Instantiate (networkManager).GetComponent<NetworkManager> ();

        // If we are using the master server we need to get the registration data
        JSONNode masterServerData = null;
        if (!string.IsNullOrEmpty (masterServerHost))
        {
            string serverId = "myGame";
            string serverName = "Forge Game";
            string type = "Deathmatch";
            string mode = "Teams";
            string comment = "Demo comment...";

            masterServerData = mgr.MasterServerRegisterData (networker, serverId, serverName, type, mode, comment, useElo, eloRequired);
        }

        mgr.Initialize (networker, masterServerHost, masterServerPort, masterServerData);

        if (useInlineChat && networker.IsServer)
            SceneManager.sceneLoaded += CreateInlineChat;

        if (networker is IServer)
        {                     
            // kit, is server
            btnReceiver.GetComponentInChildren<TextMeshProUGUI> ().text = "Disconnect";
            btnReceiver.onClick.RemoveAllListeners ();
            btnReceiver.onClick.AddListener (Quit);
            Debug.Log ("Connected as server");
        }

        mClientSendFile.enabled = true;
    }

    private void CreateInlineChat (Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= CreateInlineChat;
        var chat = NetworkManager.Instance.InstantiateChatManager ();
        DontDestroyOnLoad (chat.gameObject);
    }

    private void OnApplicationQuit ()
    {
        Quit ();
    }

    void Quit ()
    {
        Debug.Log ("Quit");

        if (getLocalNetworkConnections)
            NetWorker.EndSession ();

        //if (server != null) server.Disconnect(true);
        if (server != null) server.Disconnect (false);
    }

    public ClientSendFile clientSendFile
    {
        get { return mClientSendFile; }
    }

    #region ADDITIONAL LOGIC
    bool isServerFound = false;
    private void Reset ()
    {
        isServerFound = false;
        mIpAddress = "";
    }

    private void ResetNetwork ()
    {
        if (btnReceiver != null)
        {
            btnReceiver.interactable = true;
            btnReceiver.onClick.RemoveAllListeners ();
            btnReceiver.onClick.AddListener (AsReceiver);
        }

        if (btnSender != null)
        {
            btnSender.onClick.RemoveAllListeners ();
            btnSender.onClick.AddListener (AsSender);
        }

        if (btnSender != null)
            btnSender.GetComponentInChildren<TextMeshProUGUI> ().text = "Connect";
        if (btnReceiver != null)
            btnReceiver.GetComponentInChildren<TextMeshProUGUI> ().text = "Connect";
    }

    private void OnEnable ()
    {
        // find teacher and student button

        if (btnReceiver != null)
        {
            btnReceiver.onClick.RemoveAllListeners ();
            btnReceiver.onClick.AddListener (AsReceiver);
        }
        if (btnSender != null)
        {
            btnSender.onClick.RemoveAllListeners ();
            btnSender.onClick.AddListener (AsSender);
        }

        NetWorker.localServerLocated += TestLocalServerFind;

        Debug.Log (btnReceiver);
        Debug.Log (btnSender);
    }

    private void OnDisable ()
    {
        // reset
        Reset ();
        NetWorker.localServerLocated -= TestLocalServerFind;
    }

    public void AsReceiver ()
    {
        // the one who will send the data        
        Host ();
    }

    public void AsSender ()
    {
        if (btnReceiver != null)
            btnReceiver.GetComponent<Button>().interactable = false;
        if (btnSender != null)
            btnSender.GetComponentInChildren<Text>().text = "Stop";

        btnSender.onClick.RemoveAllListeners();
        btnSender.onClick.AddListener(() =>
        {
            StopCoroutine("_FindServer");
            ResetNetwork();
            //            StopCoroutine("_FindServerLoading");            
        });

        StartCoroutine("_FindServer");
    }

    WaitForSeconds wfs = new WaitForSeconds (1.5f);
    IEnumerator _FindServer ()
    {
        while (isServerFound == false)
        {
            NetWorker.RefreshLocalUdpListings (mPort);
            Debug.Log ("Finding Server");
            yield return wfs;
        }

        Debug.Log ("Coroutine exited.");
        Connect ();

    }

    IEnumerator _FindServerLoading ()
    {
        int counter = 0;
        string status = "";
        while (isServerFound == false)
        {
            switch (counter)
            {
                case 0:
                status = "Finding Server.";
                break;
                case 1:
                status = "Finding Server..";
                break;
                case 2:
                status = "Finding Server...";
                break;
            }

            if (counter == 2)
                counter = 0;
            else
                counter++;

            yield return new WaitForSeconds (1);
        }
    }

    #endregion
}
