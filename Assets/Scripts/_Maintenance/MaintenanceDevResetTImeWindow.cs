using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
public class MaintenanceDevResetTImeWindow : MonoBehaviour {

	[SerializeField] InputField inputTime;
	[SerializeField] Text timeLeft;
	int timeInput;

	// void Start () {
	// 	timeLeft.text = "Time usage left: " + ((TimeUsageCounter.ins.GetTime()/60)/60).ToString() + "hrs";
	// }

	void OnEnable()
	{
		timeLeft.text = "Time usage left: " + ((TimeUsageCounter.ins.GetTime()/60)/60).ToString() + "hrs";
	}
	public void ResetTime()
	{
		if(int.TryParse(inputTime.text, out timeInput))
		{
			//timeInput = int.Parse(inputTime.text);	
			MessageBox.ins.ShowQuestion("Reset time to " + timeInput + "hrs ?",MessageBox.MsgIcon.msgInformation, new UnityAction(Reset), null);
		}	
		else
		{
			MessageBox.ins.ShowOk("Invalid input!", MessageBox.MsgIcon.msgError, null);
		}
		//PlayerPrefs.SetInt("TimeUsage",);
	}

	void Reset()
	{
        //DataService ds = new DataService ("system/subscription.db");
        DataService.Open("system/subscription.db");
      SubscriptionTimeModel model = new SubscriptionTimeModel
      {
         SettedTime = (timeInput * 60) * 60,
         Timer = (timeInput * 60) * 60
      };
		//print(timeInput);
		timeInput = (timeInput*60)*60;
		//PlayerPrefs.SetInt("TimeUsage", timeInput);

      DataService._connection.Update (model);
		TimeUsageCounter.ins.SetTime(timeInput);
		timeLeft.text = "Time usage left: " + timeInput + "s";
		MessageBox.ins.ShowOk("Time usage updated!", MessageBox.MsgIcon.msgInformation, null);

        DataService.Close();
	}
}
