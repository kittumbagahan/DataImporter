using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WhatDidYaSee_act5_Manager : MonoBehaviour {


    [SerializeField]
    AudioClip clipCorrect, clipWrong, clipDrag;
    public Transform Canvas;
    public Transform[] fakeSlot_A;
    public Transform[] fakeSlot_B;
    [SerializeField]
    int pts=0;
    AudioSource audSrc;
	void Start () {
        audSrc = GetComponent<AudioSource>();
        Item.OnInsert += Insert;
        Item.OnBeginDrag += Drag;
        Item.OnReturn += Return;

		ScoreManager.ins.AW();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void Insert(Transform parent, Transform dis)
    {
        Item _itm = dis.GetComponent<Item>();
        dis.GetComponent<Image>().raycastTarget = false;
        ReparentToFakeSlot(_itm);
        pts++;
        audSrc.PlayOneShot(clipCorrect);
        if(pts == 8)
        {
            Invoke("Done",1f);
        }
    }

    void Drag(GameObject obj) {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
    }

    void Return(Transform t)
    {
        audSrc.PlayOneShot(clipWrong);
    }
    void Done()
    {
        ActivityDone.instance.Done();
    }

    void ReparentToFakeSlot(Item itm)
    {
        switch (itm.eColor)
        {
            case EColor.red:
                for (int i = 0; i < fakeSlot_A.Length; i++ )
                {
                    if(fakeSlot_A[i].childCount == 0){
                        itm.transform.SetParent(fakeSlot_A[i]);
                        itm.transform.SetLocalXPos(0);
                        itm.transform.SetLocalYPos(0);
                    }
                }
                break;
            case EColor.green:
                for (int i = 0; i < fakeSlot_B.Length; i++)
                {
                    if (fakeSlot_B[i].childCount == 0)
                    {
                        itm.transform.SetParent(fakeSlot_B[i]);
                        itm.transform.SetLocalXPos(0);
                        itm.transform.SetLocalYPos(0);
                    }
                }
                break;
            default: break;
        }
    }
}
