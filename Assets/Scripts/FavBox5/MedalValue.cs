using UnityEngine;
using System.Collections;

public class MedalValue : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


	bool isDone = false;
	public bool IsDone
	{
		get{ return isDone; }
		set{ isDone = value; }
	}
}
