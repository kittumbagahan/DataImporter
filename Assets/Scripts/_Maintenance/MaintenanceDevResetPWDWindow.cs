using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MaintenanceDevResetPWDWindow : MonoBehaviour {

	[SerializeField] Text txtCodeList;
	[SerializeField]
	List<string> lstCode = new List<string>();
	[SerializeField] Button btnAdd, btnSave;
	// void Start () {
	// 	txtCodeList.text = "";
	// 	for(int i=0; i<10; i++)
	// 	{
	// 		txtCodeList.text += i + " " + PlayerPrefs.GetString("PWD_CODE_CHG" + i.ToString()) + "=" + PlayerPrefs.GetInt(PlayerPrefs.GetString("PWD_CODE_CHG" + i.ToString())) + "\n";
	// 	}
	// }
	
	void OnEnable()
	{
		txtCodeList.text = "";
		for(int i=0; i<10; i++)
		{
			txtCodeList.text += i + " " + PlayerPrefs.GetString("PWD_CODE_CHG" + i.ToString()) + "=" + PlayerPrefs.GetInt(PlayerPrefs.GetString("PWD_CODE_CHG" + i.ToString())) + "\n";
		}
	}

	public void AddCode(Text txt)
	{
		if(lstCode.Count < 10)
		{
			if(txt.text.Length < 7 || txt.text.Length > 7)
			{
				MessageBox.ins.ShowOk("Enter a 7 characters code!", MessageBox.MsgIcon.msgError, null);
			}
			else
			{
				
				if(HasDuplicate(txt.text) == false)
				{
					lstCode.Add(txt.text);
					txtCodeList.text = "";
					for(int i=0; i<lstCode.Count; i++)
					{
						txtCodeList.text += i + " " + lstCode[i] +"\n";
					}
				}
				else{
					MessageBox.ins.ShowOk("Code already exist!",MessageBox.MsgIcon.msgError, null);
				}	
			}
		}
		else
		{
			btnAdd.interactable = false;
			MessageBox.ins.ShowOk("Max of 10 codes only!", MessageBox.MsgIcon.msgInformation, null);
		}
		if(lstCode.Count == 10)
		{
			btnSave.interactable = true;
		}else
		{
			btnSave.interactable = false;
		}
	}

	bool HasDuplicate(string s)
	{
		for(int i=0; i<lstCode.Count; i++)
		{
			if(s == lstCode[i]) return true;
		}

		return false;
	}

	public void SaveCodes()
	{
		if(lstCode.Count == 10)
		{
			for(int i=0; i<lstCode.Count; i++)
			{
				PlayerPrefs.SetString("PWD_CODE_CHG"+i.ToString(), lstCode[i]);
			}
			MessageBox.ins.ShowOk("Codes saved!", MessageBox.MsgIcon.msgInformation, null);
		}
		else
		{
			MessageBox.ins.ShowOk("10 codes are needed to save.", MessageBox.MsgIcon.msgError, null);
		}

	}
}
