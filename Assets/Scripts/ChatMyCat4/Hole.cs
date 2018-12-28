using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Hole : MonoBehaviour {

    public float h, w;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResizeMole()
    {
        for (int i = 0; i < transform.childCount; i++ )
        {

            transform.GetChild(i).GetComponent<RectTransform>().SetLocalWidth(w);
            transform.GetChild(i).GetComponent<RectTransform>().SetLocalHeight(h);
    
        }
    }
}
