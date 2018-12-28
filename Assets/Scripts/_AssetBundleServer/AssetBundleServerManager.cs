using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AssetBundleServerManager : MonoBehaviour {

    [SerializeField]
    AssetBundleServerNetwork serverNet;

    public InputField fieldURL;
    public InputField fieldVersion;
    [SerializeField]
    Text txtNumOfConnection;
    [SerializeField]
    Text txtConnectionInfo;

    [SerializeField]
    Button btnOK, btnStart;

    int numberOfConnectedClients;

    private void Start()
    {
        serverNet.OnClientAccepted += IncNumberConnectedClients;
        serverNet.OnClientDisconnected += DecNumberConnectedClients; //currently not working
        InvokeRepeating("NetworkState", 1f, 1f);
        btnStart.interactable = false;
        btnOK.onClick.AddListener (AssetBundleInfoReady);
    }

    private void AssetBundleInfoReady()
    {
        if("".Equals(fieldURL.text) || "".Equals (fieldVersion.text))
        {
            MessageBox.ins.ShowOk ("All fields are required.", MessageBox.MsgIcon.msgError, null);
        }
        else
        {
            MessageBox.ins.ShowQuestion ("Are you sure?\nURL " + fieldURL.text + "\nVersion " + fieldVersion.text, MessageBox.MsgIcon.msgInformation,
            new UnityAction (StartServerYes), new UnityAction (StartServerNo));
        }
    }


    void IncNumberConnectedClients()
    {
        numberOfConnectedClients++;
        txtNumOfConnection.text = "Number of connected clients: " + numberOfConnectedClients;
    }
    void DecNumberConnectedClients ()
    {
        numberOfConnectedClients--;
        txtNumOfConnection.text = "Number of connected clients: " + numberOfConnectedClients;
    }

    void StartServerYes ()
    {
        btnStart.interactable = true;
        fieldURL.interactable = false;
        fieldVersion.interactable = false;
        btnOK.interactable = false;
    }
    void StartServerNo()
    {

    }

    void NetworkState()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            txtConnectionInfo.text = "Not connected";
            txtConnectionInfo.color = Color.red;
        }
        else
        {
            txtConnectionInfo.text = "Connected";
            txtConnectionInfo.color = Color.green;
        }
    }
}
