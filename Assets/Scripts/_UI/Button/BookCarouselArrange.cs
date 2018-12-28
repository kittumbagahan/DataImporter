using UnityEngine;
using System.Collections;

public class BookCarouselArrange : MonoBehaviour {

//	[SerializeField]
//	float distance;

	[SerializeField]
	RectTransform afterMe, beforeMe; // button to follow in the carousel

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	public float Distance
//	{
//		get {return distance;}
//	}

	public RectTransform AfterMe
	{
		get {return afterMe;}
	}

	public RectTransform BeforeMe
	{
		get {return beforeMe;}
	}
}
