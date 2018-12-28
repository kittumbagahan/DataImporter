using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WhatDidYouSee_Act8_Manager : MonoBehaviour {
    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag;
    //public AudioClip[] audClip;
    AudioSource audSrc;
    [SerializeField]
    int pts = 0, maxPts;

    void Start()
    {
        maxPts = InventoryManager.ins.slots.Count;
        InventoryManager.ins.slots.Shuffle();
        audSrc = GetComponent<AudioSource>();
        Item.OnInsert += Insert;
        Item.OnReturn += WrongInsert;
        Item.OnBeginDrag += BeginDrag;

        InventoryManager.ins.slots[0].gameObject.SetActive(true);
		ScoreManager.ins.AW();
    }

    void Insert(Transform parent, Transform item)
    {
        
      
        audSrc.PlayOneShot(clipFit);
        item.GetComponent<Item>().Locked = true;
        
        Destroy(item.gameObject, 1f);
        //Destroy(parent.gameObject, 1F);
        Invoke("Disable",1f);
       
        Invoke("Next",1f);
       
        print("test");
    }

    void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
    }
    void WrongInsert(Transform t)
    {
        audSrc.PlayOneShot(clipWrong);
    }
    void Disable()
    {
        InventoryManager.ins.slots[pts].gameObject.SetActive(false);
    }
    void Next()
    {
        pts++;
        if (pts >= maxPts)
        {
            print("WIN!");
            Invoke("Done", 1f);

        }
        if (pts < maxPts)
        InventoryManager.ins.slots[pts].gameObject.SetActive(true);
       // g = InventoryManager.ins.slots[pts].GetComponent<Grow>();
       // g.Play();
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
