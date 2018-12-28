using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectActivity : MonoBehaviour {

    public Button []btnActivity;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Click()
    {
        int cnt = 0;
        for (int i = 0; i < btnActivity.Length; i++ )
        {
            //if (btnActivity[i].GetComponent<Image>().sprite.name == "star_g")
            if(btnActivity[i].GetComponent<Image>().material.name == "Grayscale")
            {
                print("load it " + i);
                //btnActivity[i].GetComponent<ButtonActivity>().Click();
                btnActivity[i].GetComponent<Button>().onClick.Invoke();
                break;
            }
            cnt++;
        }

        if(cnt >= btnActivity.Length)
        {
            print("random");
           //btnActivity[Random.Range(0,btnActivity.Length)].GetComponent<ButtonActivity>().Click();
            btnActivity[Random.Range(0, btnActivity.Length)].GetComponent<Button>().onClick.Invoke();
        }

        print(cnt);
    }
}
