using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ColorsMixedUpMatchingManager : MonoBehaviour {

    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag;
    int point = 0;
   // public AudioClip[] audClip;
    AudioSource audSrc;

	void Start () {
        //Item itm = null;
        audSrc = GetComponent<AudioSource>();
        for(int i=0; i<InventoryManager.ins.items.Count; i++)
        {
           // itm = InventoryManager.ins.items[i].GetComponent<Item>();
            //itm.OnDrop += IncPoints;
        }
        Item.OnInsert += Insert;
        Item.OnDrop += IncPoints;
        Item.OnReturn += WrongInsert;
        Item.OnBeginDrag += BeginDrag;
		ScoreManager.ins.AW();
	}

    void IncPoints() {
        point++;
        audSrc.PlayOneShot(clipFit);
        //print(point);
        if (point == 4)
        {
           
            Invoke("Done",0.5f);   
        }
    }

    void WrongInsert(Transform t)
    {
        audSrc.PlayOneShot(clipWrong);
    }

    void Insert(Transform parent, Transform dis)
    {
        //parent.GetComponent<Image>().enabled = false;
    }

    void BeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
    }

    void Done() {
        ActivityDone.instance.Done();
        print("Game over");
    }
}
