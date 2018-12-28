using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class NetworkTest : MonoBehaviour {

	[SerializeField]
	InputField inputName, inputAge, inputSection;

	public void PassData()
	{
		//NetworkTestObject mObject = new NetworkTestObject ();
		//mObject.name = inputName.text;
		//mObject.age = inputAge.text;
		//mObject.section = inputSection.text;

		//MainNetwork.Instance.clientSendFile.SendData (mObject);

		Debug.Log ("Data sent.");
	}
}

[System.Serializable]
public class NetworkTestObject
{
	public string name { get; set; }
	public string age { get; set; }
	public string section { get; set; }
}