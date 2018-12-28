using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



public class ColorWheelManager : MonoBehaviour {
    [SerializeField]
    AudioClip clipInsert, clipWrong, clipDrag;
    [SerializeField]
    RectTransform ColorWheel;

    //Animator animator;

    public static ColorWheelManager ins;
    public List<GameObject> lstColorItem;
    public List<GameObject> lstColorSlot;
    [SerializeField]
    int point, maxPts;

    public float spinSpd = 0.2f;
 
    AudioSource audSrc;
    [SerializeField]
    int index;

    public int Point {
        set { point = value; }
        get { return point; }
    }

    public void IncPoints()
    {
        point++;
    }
    void Start()
    {
        index = SaveTest.Set;
        //animator = ColorWheel.GetComponent<Animator>();
        audSrc = GetComponent<AudioSource>();
        ins = this;
        Item.OnInsert += IncPoints;
        // Item.OnInsert += FadeOutSlot;
        Item.OnDrop += Spin;
        Item.OnReturn += WrongInsert;
        Item.OnBeginDrag += BeginDrag;
        
        //  Invoke("Late",0.5f);
        switch (index)
        {
            case 0:
                maxPts = 3;

                lstColorSlot[0].SetActive(true);
                lstColorSlot[1].SetActive(true);
                lstColorSlot[2].SetActive(true);
                //dont destroy the slots just change their color value
                DisableSlots(lstColorSlot[3].transform);
                DisableSlots(lstColorSlot[4].transform);
                DisableSlots(lstColorSlot[5].transform);
                DisableSlots(lstColorSlot[6].transform);
                DisableSlots(lstColorSlot[7].transform);
                //lstColorSlot[3].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[4].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[5].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[6].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[7].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                break;
            case 1:
                maxPts = 3;
                lstColorSlot[7].SetActive(true);
                lstColorSlot[3].SetActive(true);
                lstColorSlot[4].SetActive(true);
                //dont destroy the slots just change their color value
                DisableSlots(lstColorSlot[0].transform);
                DisableSlots(lstColorSlot[1].transform);
                DisableSlots(lstColorSlot[5].transform);
                DisableSlots(lstColorSlot[6].transform);
                DisableSlots(lstColorSlot[2].transform);
                //lstColorSlot[0].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[1].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[5].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[6].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[2].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                break;
            case 2:
                maxPts = 3;
                lstColorSlot[5].SetActive(true);
                lstColorSlot[6].SetActive(true);
                lstColorSlot[0].SetActive(true);
                //dont destroy the slots just change their color value
                DisableSlots(lstColorSlot[4].transform);
                DisableSlots(lstColorSlot[1].transform);
                DisableSlots(lstColorSlot[7].transform);
                DisableSlots(lstColorSlot[3].transform);
                DisableSlots(lstColorSlot[2].transform);
                //lstColorSlot[4].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[1].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[7].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[3].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                //lstColorSlot[2].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                break;
            case 3:
                maxPts = 4;
                lstColorSlot[0].SetActive(true);
                lstColorSlot[2].SetActive(true);
                lstColorSlot[1].SetActive(true);
                lstColorSlot[4].SetActive(true);
       
                lstColorSlot[6].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                lstColorSlot[7].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                lstColorSlot[3].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                lstColorSlot[5].transform.GetChild(0).GetComponent<Slot>().eColor = EColor.non;
                break;
            case 4:
                maxPts = 8;
                lstColorSlot[0].SetActive(true);
                lstColorSlot[1].SetActive(true);
                lstColorSlot[2].SetActive(true);
                lstColorSlot[3].SetActive(true);
                lstColorSlot[4].SetActive(true);
                lstColorSlot[5].SetActive(true);
                lstColorSlot[6].SetActive(true);
                lstColorSlot[7].SetActive(true);
                
                break;
            default: break;
        }
		ScoreManager.ins.maxMove = maxPts;
		ScoreManager.ins.AW();
    }

    void DisableSlots(Transform t)
    {
        for (int i = 0; i < t.childCount; i++ )
        {
            t.GetChild(i).GetComponent<Slot>().eColor = EColor.non;
        }
    }
    public void IncPoints(Transform parent, Transform item)
    {
        Slot slot = parent.GetComponent<Slot>();
        Item itm = item.GetComponent<Item>();
        point++;
        //item.GetComponent<Image>().enabled = false;
        item.gameObject.SetActive(false);
       
        audSrc.PlayOneShot(clipInsert);
        FadeOutSlot(parent);
        if (itm.eColor == slot.eColor)
        {
            //point++;
            //item.GetComponent<Image>().enabled = false;
            ////parent.parent.GetComponent<Image>().enabled = false;
            ////Destroy(item.gameObject);
            //audSrc.clip = audClip[0];
            //audSrc.Play();
            //FadeOutSlot(parent);
        }
        Spin();
    }

    void WrongInsert(Transform t)
    {
       
        audSrc.PlayOneShot(clipWrong);
    }

    void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
    }

    void Late() {
        Item.OnInsert += IncPoints;
        // Item.OnInsert += FadeOutSlot;
        Item.OnDrop += Spin;
        Item.OnReturn += WrongInsert;
    }

    void Spin()
    {
        if (point >= maxPts)
        {
           // animator.SetTrigger("play");
            print("done");
            Invoke("Done", 1.5f);
        }
       
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }

    void FadeOutSlot(Transform parent)
    {
        FadeOut fadeOut = parent.parent.GetComponent<FadeOut>();
        fadeOut.Play();
        print("oo");
    }

   
}
