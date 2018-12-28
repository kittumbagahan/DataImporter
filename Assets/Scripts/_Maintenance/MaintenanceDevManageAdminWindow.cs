using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class MaintenanceDevManageAdminWindow : MonoBehaviour {

	[SerializeField] InputField inputText;
	[SerializeField] Text txtAdminPwd;

	void OnEnable()
	{
            //DataService ds = new DataService();
        DataService.Open();
        AdminPasswordModel model = DataService._connection.Table<AdminPasswordModel>().Where(x => x.Id == 1).FirstOrDefault();
        txtAdminPwd.text = "Admin password: " + model.Password;
        DataService.Close();
    }

	public void ChangeAdminPWD()
	{
        if(inputText.text.Length < 4)
        {
            MessageBox.ins.ShowOk("Password must atleast have 4 characters", MessageBox.MsgIcon.msgError, null);
        }
        else
        {
            if (!"".Equals(inputText.text))
            {
                MessageBox.ins.ShowQuestion("Change admin password?", MessageBox.MsgIcon.msgWarning, new UnityAction(Save), null);
            }
            else
            {
                MessageBox.ins.ShowOk("Enter new password!", MessageBox.MsgIcon.msgError, null);
            }
        }
	


	}

	void Save()
	{
            //DataService ds = new DataService();
        DataService.Open();
        AdminPasswordModel model = DataService._connection.Table<AdminPasswordModel>().Where(x => x.Id == 1).FirstOrDefault();
        if (inputText.text.Equals(model.Password))
        {
            MessageBox.ins.ShowOk("Old and new password must not be the same.", MessageBox.MsgIcon.msgError, null);
        }
        else
        {
            model.Password = inputText.text;
            txtAdminPwd.text = "Admin password: " + model.Password;
            DataService._connection.Update(model);
            //PlayerPrefs.SetString("admin", inputText.text);
            MessageBox.ins.ShowOk("Admin password changed!", MessageBox.MsgIcon.msgInformation, null);
        }
        DataService.Close();
	}
}
