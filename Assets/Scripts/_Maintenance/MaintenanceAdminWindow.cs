using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaintenanceAdminWindow : MonoBehaviour {

	[SerializeField] Text txtTime;
	[SerializeField] Text txtbackUpDate;

	void Start () {
		txtTime.text = "Time until subscription expires: " + (TimeUsageCounter.ins.GetTime()/60)/60 + "hrs";
	}

	void OnEnable()
	{
		txtTime.text = "Time until subscription expires: " + (TimeUsageCounter.ins.GetTime()/60)/60 + "hrs";
	}
	// Update is called once per frame

}
