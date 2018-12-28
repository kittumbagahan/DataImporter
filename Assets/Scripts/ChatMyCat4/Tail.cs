using UnityEngine;
using System.Collections;

public class Tail : MonoBehaviour {

    //float t;
    public bool up;
	void Start () {
    
       // t = transform.GetLocalYPos() + 4f;
	}
	
	// Update is called once per frame
	void Update () {
        if (up)
        {
            transform.SetLocalYPos(transform.GetLocalYPos() + Mathf.Lerp(0,4f,0.1f));

        }
        else {

            transform.SetLocalYPos(transform.GetLocalYPos() - Mathf.Lerp(0, 5f, 0.2f));
	
        }
    }

}
