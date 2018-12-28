using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _Load : MonoBehaviour {

    public int sceneIndex;
    public string sceneName;
    public string btnText;
	void Start () {
      if(Application.loadedLevelName == "test")
      {
          transform.GetChild(0).GetComponent<Text>().text = btnText;
          transform.GetChild(0).GetComponent<Text>().color = Color.white;
      }
           
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Click()
    {
        //Item.ResetSubscriptions();
        if (sceneName == "")
        {
           // Application.LoadLevel(sceneIndex);
            Application.LoadLevel("test");
           
        }
        else {
            Application.LoadLevel(sceneName);
      
        }
    //    Application.LoadLevel(sceneIndex);
    }

}
