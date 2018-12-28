using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintenanceWindows : MonoBehaviour {

	[SerializeField]
	GameObject devWin, adminWin;
	void Start () {
		if(MaintenanceManager.ins.loggedInPassword.Equals("tammytam"))
		{
			devWin.SetActive(true);
		}
		else
		{
			adminWin.SetActive(true);
		}
	}
	
	

	void OnEnable()
	{
		if(MaintenanceManager.ins.loggedInPassword.Equals("tammytam"))
		{
			devWin.SetActive(true);
		}
		else
		{
			adminWin.SetActive(true);
		}
	}

	public void Close()
	{
		devWin.SetActive(false); 
		adminWin.SetActive(false); 
		gameObject.SetActive(false);
	}

}
