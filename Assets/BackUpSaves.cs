using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

using System.Text;

public class BackUpSaves : MonoBehaviour {

	[SerializeField]
	string path = "/";
	[SerializeField]
	List<string> lstData = new List<string>();
	string _userId;
	[SerializeField]
	int n=0; //read cntr

	[SerializeField]
	float loadSpd = 0.01f;

	[SerializeField]
	bool backUP;

	[SerializeField]
	TextMeshProUGUI tmLocation;
	[SerializeField]
	Text txtTime;
	[SerializeField]
	ProgressBar pb;

	IEnumerator coProcess;

	DirectoryInfo d;
	[SerializeField]
	FileInfo[] files;
	[SerializeField]
	List<string> fileNames;
	[SerializeField]
	TMP_Dropdown dropDownFiles;

    DatabaseController dc;
    #region MONO

    private void Awake()
    {
        dc = new DatabaseController();
    }
    

    void OnEnable()
    {
        dropDownFiles.ClearOptions();
        dropDownFiles.AddOptions(dc.GetFileNames());

        GetTime();
    }

    #endregion

    void GetTime()
	{
		txtTime.text = "Time until subscription expires " + ((TimeUsageCounter.ins.GetTime()/60)/60).ToString() + "hrs";
	}
	public void BackUpData()
	{
        dc.MakeBackUp();
      
	}

	void ClosePB()
	{
		pb.gameObject.SetActive(false);
	}

	IEnumerator IEProcess()
	{
		float progress=0, n;
		n = (float)1/(float)2130;
		pb.gameObject.SetActive(true);
		for(int i=0; i<2130; i++) //n is the number of lines
		{
			progress += n;
			//print(progress);
			pb.SetProgress(progress);
			yield return new WaitForSeconds(loadSpd);
		}
	}

	public void Read()
	{
        if (!dropDownFiles.captionText.ToString().Equals(""))
        {
            dc.SetDatabase(dropDownFiles.captionText.text.ToString());
            MessageBox.ins.ShowOk(dropDownFiles.captionText.text.ToString() + "Loaded!", MessageBox.MsgIcon.msgInformation, null);
        }
	
		else
		{
			MessageBox.ins.ShowOk("Select an option.", MessageBox.MsgIcon.msgError, null);
		}
	}

	
	
}
