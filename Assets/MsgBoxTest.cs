using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgBoxTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MessageBox.ins.ShowOk("hi!", MessageBox.MsgIcon.msgInformation, null);
	}
	
	
}
