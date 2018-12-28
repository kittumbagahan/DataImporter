using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

using TMPro;

public class NetworkUI : MonoBehaviour{    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    #region METHODS

    public static void Sender(Button senderButton, Button receiverButton)
    {
        senderButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Send File";
        receiverButton.gameObject.GetComponentInChildren<TextMeshProUGUI> ().text = "Stop";
    }

    public static void Receiver(Button senderButton, Button receiverButton)
    {        
        Button[] buttons = { senderButton };
        DisableButton(buttons);
        receiverButton.gameObject.GetComponentInChildren<TextMeshProUGUI> ().text = "Stop";
    }

    public static void DisableButton(params Button[] buttons)
    {        
        for (int index = 0; index < buttons.Length; index++)
        {
            buttons[index].interactable = false;
        }
    }    

    public static void EnableButtons(params Button[] buttons)
    {
        for (int index = 0; index < buttons.Length; index++)
        {
            buttons[index].interactable = true;
        }
    }

    #endregion
}
