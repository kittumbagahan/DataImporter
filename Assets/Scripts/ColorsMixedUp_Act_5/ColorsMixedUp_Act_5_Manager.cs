using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ColorsMixedUp_Act_5_Manager : MonoBehaviour {

    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag, clipSolved;
    //public AudioClip[] audClip;
    //[SerializeField]
    AudioSource audSrc;
    Vector3 recentRotation;
    [SerializeField]
    int pts;
	void Start () {
        Item.OnInsert += OnInsert;
        Item.OnBeginDrag += OnBeginDrag;
        Item.OnReturn += OnReturn;
        Shrink.OnShrinkEnd += EnableItems;
        for (int i = 0; i < InventoryManager.ins.items.Count; i++ )
        {
            Item itm = InventoryManager.ins.items[i].GetComponentInChildren<Item>();
            itm.Locked = true;
        }
        audSrc = GetComponent<AudioSource>();
		ScoreManager.ins.AW();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Done()
    {
        ActivityDone.instance.Done();
    }
    void OnInsert(Transform parent, Transform dis)
    {
        dis.rotation = parent.rotation;
        dis.GetComponent<RectTransform>().SetWidth(parent.GetComponent<RectTransform>().GetWidth());
        dis.GetComponent<RectTransform>().SetHeight(parent.GetComponent<RectTransform>().GetHeight());
        dis.GetComponent<Item>().Locked = true;
        dis.GetChild(0).GetComponent<Image>().raycastTarget = false;
        pts++;
        if (pts == 9)
        {
            Invoke("Done",1f);
            print("done");
            audSrc.PlayOneShot(clipSolved);
        }
        else {
            //audSrc.clip = audClip[0];
            audSrc.PlayOneShot(clipFit);
        }
    }

    void OnBeginDrag(GameObject obj)
    {
		ScoreManager.ins.IncNumOfMoves();
         obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }

    void OnReturn(Transform dis)
    {
        dis.GetComponent<RectTransform>().SetWidth(120f);
        dis.GetComponent<RectTransform>().SetHeight(120f);
        dis.rotation = dis.parent.rotation;//Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        audSrc.PlayOneShot(clipWrong);
    }

    void BeginDrag(GameObject obj)
    {
		//ScoreManager.ins.IncNumOfMoves();
        audSrc.PlayOneShot(clipDrag);
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

}
