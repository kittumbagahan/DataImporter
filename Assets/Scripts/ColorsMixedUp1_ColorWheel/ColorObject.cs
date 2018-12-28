using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorObject : MonoBehaviour {

    public delegate void ActionInsert(Transform parent, Transform item);
    //public ActionInsert OnInserted;
    public static event ActionInsert OnInsert;
 
    void Start () {
        //container = transform.parent;
       
     
       // Item.OnDrop += Drop;
       
	}

    //void Drop()
    //{
    //    Slot s = null;
    //    Image img = null;
    //    img = transform.parent.parent.GetComponent<Image>();
    //    s = GetComponentInParent<Slot>();
    //    if (itm.eColor == s.eColor)
    //    {
    //        print("ColorObject scripts");
    //        gameObject.SetActive(false);
    //        img.enabled = false;

    //        if (OnInsert != null)
    //        {
    //            OnInsert(null , null);
    //            print("yes");
    //        }
    //        else {
    //            print("no");
    //        }
           
    //    }
    //}
  
}
