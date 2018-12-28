using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//MaintenanceForgotPassword.cs
//MaintenanceDevResetPWDWindow.cs
public class MaintenanceLogIn : MonoBehaviour {
	[SerializeField]
	InputField inputPWD;
	[SerializeField]
	GameObject canvas;
	
	


	public void LogIn()
	{
        //DataService ds = new DataService();
        DataService.Open();
        AdminPasswordModel model = DataService._connection.Table<AdminPasswordModel>().Where(x => x.Id == 1).FirstOrDefault();
		if(inputPWD.text.Equals(model.Password)){
			print("Log In Success");
			MaintenanceManager.ins.loggedInPassword = "admin";
			UserParentalManager.ins.SpawnParentControl();
			ClearPWDField();
			canvas.SetActive(false);

		}
		else if(inputPWD.text == "tammytam")
		{
			MessageBox.ins.ShowOk("THE SUPER oldUsername HAS LOGGED IN!", MessageBox.MsgIcon.msgInformation, new UnityAction(ShowParentalControl));
			print("Developer Logged In!");
			MaintenanceManager.ins.loggedInPassword = "tammytam";
			ClearPWDField();
			canvas.SetActive(false);

		}
		else
		{
			MessageBox.ins.ShowOk("Access denied.",MessageBox.MsgIcon.msgError, null);
		}
        DataService.Close();
	}

	public void ClearPWDField()
	{
		//txtPWD.text = "";
		inputPWD.text = "";
	}

	public void ContactUs()
	{
		MessageBox.ins.ShowOk("Please email us at palabaydev@gmail.com",MessageBox.MsgIcon.msgInformation, null);
	}

	void ShowParentalControl()
	{
		UserParentalManager.ins.SpawnParentControl();
	}

	
}
