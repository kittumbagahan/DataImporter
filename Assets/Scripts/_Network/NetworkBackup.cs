using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkBackup : NetworkDiscovery {

    Text txtTestData;	

	#region UI

	// start as server to send data
	public void Sender(string data)
	{
        Initialize();
        StartAsServer();

        // set the data to be sent
        broadcastData = data;

        StopBroadcast();

        StartCoroutine(fak());

        Debug.Log("Started as Server");
	}

	public void Receiver(Text _txtTestData)
	{
        Initialize();
        StartAsClient();

        txtTestData = _txtTestData;

        Debug.Log("Started as Listener");
	}

	#endregion

	#region NETWORK

	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
        Debug.Log("OnReceivedBroadcast");

		fromAddress = fromAddress.Replace ("::ffff:", "");

        // show prompt to write the receieved data

        txtTestData.text = data;
	}

    IEnumerator fak()
    {
        yield return new WaitForSeconds(1);

        Initialize();
        StartAsServer();        
    }   

	#endregion
}
