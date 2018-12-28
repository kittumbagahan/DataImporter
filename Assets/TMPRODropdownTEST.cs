using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPRODropdownTEST : MonoBehaviour {

	[SerializeField]
	TMP_Dropdown dropdown;
	[SerializeField]
	List<string> lstopts;
	// Use this for initialization
	void Start () {
		dropdown.AddOptions(lstopts);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Selected()
	{
		print(lstopts[dropdown.value]);
	}

	public void Clear()
	{
		dropdown.ClearOptions();
	}
}
