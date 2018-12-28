using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ReadType {ReadItMySelf, ReadItToMe, AutoRead}

public class ReadDialog : MonoBehaviour {

	Animator animator;

	[SerializeField]
	RectTransform _readDialog;

	// Use this for initialization
	void Start () {
		animator = _readDialog.GetComponent<Animator>();
        //Close();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Close()
	{
		//animator.SetTrigger("Close");
	}

	public void Open()
	{
		//animator.SetTrigger("Open");
	}
}
