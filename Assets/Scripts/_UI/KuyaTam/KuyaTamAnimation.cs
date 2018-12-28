using UnityEngine;
using System.Collections;

public class KuyaTamAnimation : MonoBehaviour {

	public static KuyaTamAnimation instance;


	Animator animator;

	// Use this for initialization
	void Start () {
		instance = this;
		animator = GetComponent<Animator>();

	}
	

	public void OK()
	{
		animator.SetTrigger("OK");
	}
}
