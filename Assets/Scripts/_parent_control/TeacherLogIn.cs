using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeacherLogIn : MonoBehaviour {

	[SerializeField]
	TMP_InputField inputPWD;

	void Start () {
        if(StoryBookSaveManager.ins.selectedBook != StoryBook.NULL)
        {
            gameObject.SetActive(false);
        }
		
	}

	public void LogIn()
	{
        ResetPasswordModel m = null;
        //DataService ds = new DataService();
        DataService.Open();
        AdminPasswordModel admin = DataService._connection.Table<AdminPasswordModel>().Where(x => x.Id == 1).FirstOrDefault();        

        if (inputPWD.text.Equals(admin.Password)){
            UserRestrictionController.ins.Restrict(0);
            StoryBookSaveManager.ins.activeUser = "teacher";
            StoryBookSaveManager.ins.activeUser_id = -1;
            Tammytam.ins.Say("Hi teacher!");
            gameObject.SetActive(false);
            //show section selection
		}
        else if (m != SystemPassword(inputPWD.text))
        {
            UserRestrictionController.ins.Restrict(0);
            StoryBookSaveManager.ins.activeUser = "teacher";
            StoryBookSaveManager.ins.activeUser_id = -1;
            MessageBox.ins.ShowOk("Admin password has been reset to 1234", MessageBox.MsgIcon.msgInformation, null);
            gameObject.SetActive(false);
        }
		else
		{
			MessageBox.ins.ShowOk("Access denied!", MessageBox.MsgIcon.msgError, null);
		}

        DataService.Close();
    }

    private ResetPasswordModel SystemPassword(string input)
    {
        //DataService ds = new DataService();
        DataService.Open();
        ResetPasswordModel model = DataService._connection.Table<ResetPasswordModel>().Where(x => x.Used == false && x.SystemPasscode == input).FirstOrDefault();
        if (model != null)
            model.Used = true;
        else
        return null;

        AdminPasswordModel admin = DataService._connection.Table<AdminPasswordModel>().Where(x => x.Id == 1).FirstOrDefault();
        admin.Password = "1234";
        DataService._connection.Update(model);
        DataService._connection.Update(admin);

        DataService.Close();

        return model;
    }
}
