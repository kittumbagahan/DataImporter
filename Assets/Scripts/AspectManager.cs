using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AspectManager : MonoBehaviour {

	string aspect;

	// Use this for initialization
	void Start () {
		print(gameObject.name + "HELLLLO");
		Aspect();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void Aspect()
	{
		aspect = string.Format("{0:#.##}", Camera.main.aspect);
		//text.text = Screen.width + " " + Screen.height;	
		switch(aspect)
		{
		case "1.33":
			transform.GetChild(0).localScale = new Vector3(1.2f, 1.2f, 1f);
			break;
		case "1.5":
			transform.GetChild(0).localScale = new Vector3(1.06f, 1.06f, 1f);
			break;
		case "1.42":
			transform.GetChild(0).localScale = new Vector3(1.115f, 1.115f, 1f);
			break;
		}
	}
}
