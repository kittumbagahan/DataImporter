using UnityEngine;
using Holoville.HOTween;

public class Tween : MonoBehaviour {

	// Use this for initialization
	void Start () {
        HOTween.Init();
        //HOTween.To(gameObject.transform, 1, "rotation", new Vector3(0, 0, ))
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
