using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSection : MonoBehaviour {

    string selectedID;
    void Start () {
		
	}
	
	public void SelectId(string s)
    {
        selectedID = s;
    }
}
