using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MaintenanceDevResetPWDWindow.cs
//MaintenanceLogIn.cs
public class MaintenanceForgotPassword : MonoBehaviour {

	public string[] pwdResetCode;
	//public string forgotPWDcode;

	void Start()
	{
		for(int i=0; i<10; i++)
		{
			if(!PlayerPrefs.GetString("PWD_CODE_CHG" + i.ToString()).Equals(""))
			{
				pwdResetCode[i] = PlayerPrefs.GetString("PWD_CODE_CHG" + i.ToString());
			}
		}
	}


}
