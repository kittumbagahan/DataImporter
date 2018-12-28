using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FavoriteBox_Act4_Manager : MonoBehaviour {

	[SerializeField]
    int score = 0, maxScore;
    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag;
    AudioSource audSrc;

  
	void Start () {
        Item.OnInsert += Insert;
        Item.OnBeginDrag += BeginDrag;
        Item.OnReturn += Return;
        //Shrink.OnShrinkEnd += EnableItems;
        //for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        //{
        //    Item itm = InventoryManager.ins.items[i].GetComponentInChildren<Item>();
        //    itm.Locked = true;
        //}
        audSrc = GetComponent<AudioSource>();
		ScoreManager.ins.maxMove = 6;
		ScoreManager.ins.AW();
       // audSrc.clip = insertClip;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Insert(Transform parent, Transform itm)
    {
        Item _itm = itm.GetComponent<Item>();
        _itm.Locked = true;

        audSrc.PlayOneShot(clipFit);
        itm.GetComponent<RectTransform>().SetWidth(_itm.FinalWidth);
        itm.GetComponent<RectTransform>().SetHeight(_itm.FinalHeight);
        score++;
        if (score == maxScore)
        {
            
            Invoke("Done", 0.5f);
            print("aw");
        }
        
    }

    void BeginDrag(GameObject obj)
    {
        Item _itm = obj.GetComponent<Item>();
        obj.transform.GetComponent<RectTransform>().SetWidth(_itm.FinalWidth);
        obj.transform.GetComponent<RectTransform>().SetHeight(_itm.FinalHeight);
        print("wow");
        audSrc.PlayOneShot(clipDrag);

		ScoreManager.ins.IncNumOfMoves();
    }

    void Return(Transform itm)
    {
        Item _itm = itm.GetComponent<Item>();
        audSrc.PlayOneShot(clipWrong);
        if(_itm.Locked == false)
        {
            
            itm.GetComponent<RectTransform>().SetWidth(_itm.tempWidth);
            itm.GetComponent<RectTransform>().SetHeight(_itm.tempHeight);
    
        }
    }


    void EnableItems(GameObject obj)
    {
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            InventoryManager.ins.items[i].GetComponentInChildren<Item>().Locked = false;
            InventoryManager.ins.items[i].GetComponentInChildren<Image>().color = Color.white;

        }
        Shrink.OnShrinkEnd -= EnableItems;
    }
    void Done()
    { 
       
            ActivityDone.instance.Done();
        
    }
}
