using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleUpdateManager : MonoBehaviour {

	public static string pid_bundle_1 = "bundle_1";
	public static string pid_bundle_2 = "bundle_2";
	public static string pid_bundle_all = "bundle_all";

	/*
	if(the cached assetbundle is not equal to PatchManager.BundleURL(index))
		delete cache
		download new bundle
	else
		continue

	*/

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void CheckPurchases()
	{
		if(PlayerPrefs.GetInt("paid") == 1)
		{
			
		}
	}
}
