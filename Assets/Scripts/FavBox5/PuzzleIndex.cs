using UnityEngine;
using System.Collections;

public class PuzzleIndex : MonoBehaviour {

	[SerializeField]
	int index;

	// Use this for initialization
	void Start () {
	
	}

	public int Index
	{
		get {return index; }
	}
}
