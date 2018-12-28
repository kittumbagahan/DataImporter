using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class AfterTheRain_act2_Manager : MonoBehaviour {
    public static AfterTheRain_act2_Manager ins;
  
    //public List<GameObject> lstColorItem;
    //public List<GameObject> lstColorSlot;
    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag;
    //public AudioClip[] audClip;

    [SerializeField]
    int point = 0, maxPts;
    AudioSource audSrc;
    public int Point
    {
        set { point = value; }
        get { return point; }
    }

    public void IncPoints()
    {
        point++;
    }

    public void IncPoints(Transform parent, Transform item)
    {
        Slot slot = parent.GetComponent<Slot>();
        Item itm = item.GetComponent<Item>();

        if (itm.eColor == slot.eColor)
        {
            point++;
            //parent.parent.GetComponent<Image>().enabled = false;
            parent.GetComponent<Image>().enabled = false;
            Destroy(item.gameObject);
            audSrc.PlayOneShot(clipFit);
           
        }
        
        GameOver();
    }

    void WrongInsert(Transform t)
    {
        audSrc.PlayOneShot(clipWrong);
    }

    void BeginDrag(GameObject obj) {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
    }

   

    void Start()
    {
        ins = this;
        audSrc = GetComponent<AudioSource>();
        Item.OnDrop += GameOver;
        Item.OnInsert += IncPoints;
        Item.OnReturn += WrongInsert;
        Item.OnBeginDrag += BeginDrag;
        // StartCoroutine(IELate());
       // Invoke("Late",0.5f);
		ScoreManager.ins.AW();

    }
    void Late()
    {

        // ColorObject.OnInsert += IncPoints;
        Item.OnDrop += GameOver;
        Item.OnInsert += IncPoints;
        Item.OnReturn += WrongInsert;
        Item.OnBeginDrag += BeginDrag;
    }

    void GameOver()
    {
        if (point == maxPts)
        {
            Invoke("Done",1f);
        }
    }

    void Done()
    {
        ActivityDone.instance.Done();
        print("done");
    }
}
