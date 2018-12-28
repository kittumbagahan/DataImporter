using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ChatWithCat_Act5_Manager : MonoBehaviour {

    public Text txtDebug;
	[SerializeField]
    AudioClip clipFit, clipWrong, clipDrag;
   
    [SerializeField]
    int pts = 0;
    AudioSource audSrc;
	void Start () {
        Item.OnInsert += Insert;
        Item.OnBeginDrag += BeginDrag;
        Item.OnReturn += Return;
        
        audSrc = GetComponent<AudioSource>();
		ScoreManager.ins.AW();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Insert(Transform parent, Transform dis)
    {
        dis.GetComponent<Item>().Locked = true;
        pts++;
        audSrc.PlayOneShot(clipFit);
        if(pts == 6)
        {
            print("DONE");
            Invoke("Done",1f);
        }
    }

    void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
        obj.transform.GetChild(0).GetComponent<RectTransform>().SetWidth(obj.GetComponent<Item>().FinalWidth);
        obj.transform.GetChild(0).GetComponent<RectTransform>().SetHeight(obj.GetComponent<Item>().FinalHeight);
        try
        {

            txtDebug.text += "\n" + "Manager.cs get Item.FinalHeight" + obj.GetComponent<Item>().FinalHeight;
            txtDebug.text += "\n" + "Manager.cs get Item.FinalWidht" + obj.GetComponent<Item>().FinalWidth;
        }
        catch (Exception ex)
        {
            print(ex);
        }
        //print(parent.GetComponent<Item>().FinalWidth);
        //dis.GetChild(0).GetComponent<RectTransform>().SetWidth(parent.GetComponent<Item>().FinalWidth);
        //dis.GetChild(0).GetComponent<RectTransform>().SetHeight(parent.GetComponent<Item>().FinalHeight);
    }

    void Return(Transform t)
    {
        audSrc.PlayOneShot(clipWrong);
        t.GetChild(0).GetComponent<RectTransform>().SetWidth(t.GetComponent<Item>().tempWidth);
        t.GetChild(0).GetComponent<RectTransform>().SetHeight(t.GetComponent<Item>().tempHeight);
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
