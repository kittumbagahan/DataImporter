using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class MaintenanceChangePWD : MonoBehaviour {

	[SerializeField]
	InputField inputOld, inputNew;



	public void ChangePassword()
	{
		if(inputOld.text.Equals("") || inputNew.text.Equals(""))
		{
			MessageBox.ins.ShowOk("All fields are required.", MessageBox.MsgIcon.msgError, null);
		}
        else if (inputNew.text.Length < 4)
        {
            MessageBox.ins.ShowOk("Password must atleast have 4 characters", MessageBox.MsgIcon.msgError, null);
        }
		else
		{
            //DataService ds = new DataService();
            DataService.Open();
            AdminPasswordModel model = DataService._connection.Table<AdminPasswordModel>().Where(x=>x.Id==1).FirstOrDefault();
			if(inputOld.text.Equals(model.Password) && !inputNew.text.Equals("tammytam"))
			{
				if(!inputOld.text.Equals(inputNew.text))
				{
                    model.Password = inputNew.text;
                    DataService._connection.Update(model);
					MessageBox.ins.ShowOk("Change password success!", MessageBox.MsgIcon.msgInformation, new UnityAction(CloseWindow));
					inputNew.text = "";
					inputOld.text = "";

				}
				else
				{
					MessageBox.ins.ShowOk("Old and new password must not be the same.", MessageBox.MsgIcon.msgError, null);
				}
			}
			else
			{
				MessageBox.ins.ShowOk("Wrong old password.", MessageBox.MsgIcon.msgError, null);
			}
            DataService.Close();
		}

	}


	void CloseWindow()
	{
		gameObject.SetActive(false);
	}
}
