using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
interface IGrowClick{

    void Grow();
    void InitializeGrow();
    void BackToOriginalSize();
    
}

public class Item : MonoBehaviour, IGrowClick {

    public bool growOnClick = true;
    public bool backToOriginalSizeOnDrop = true;
    public EColor eColor = new EColor();
    public EAlphabet eAlphabet = new EAlphabet();

	//kit
	public string letterValue;

    [SerializeField]
    Transform startOrigin; //the origin on creation
    [SerializeField]
    Transform recentParent;
    //[SerializeField]
    //Vector3 originalRotation;
	bool dragging=false; //for local dragging
    [SerializeField]
    bool locked; //use along with WorldGameManager if item is under clue

    public delegate void ActionInsert(Transform parent, Transform dis);
    public delegate void ActionBeginDrag(GameObject obj);
	public delegate void ActionDrop();
    public delegate void ActionReturn(Transform t);
	public static event ActionDrop OnDrop;
    public static event ActionInsert OnInsert;
    public static event ActionBeginDrag OnBeginDrag;
    public static event ActionReturn OnReturn;

	//public event delDrop OnDrop;

	#region seterrs and getters
	public Transform RecentParent{
		set{recentParent = value;}
		get{return recentParent;}
	}
    public Transform StartOrigin {
        set { startOrigin = value; }
        get { return startOrigin; }
    }
    public bool Locked
    {
        set { locked = value; }
        get { return locked; }
    }

    
    #endregion


    void Awake()
    {
        //RemoveSubscribers();  
        //tempH = rect.sizeDelta.y;
        //tempW = rect.sizeDelta.x;
    }
    void Start () {

        rect = GetComponent<RectTransform>();
		recentParent = transform.parent;
        startOrigin = recentParent;
        //originalRotation = new Vector3(0,0,transform.rotation.z);
        //InventoryManager.ins.items.Add(this.gameObject);
        tempH = rect.sizeDelta.y;
        tempW = rect.sizeDelta.x;
        //To avoid conflict with Grow.cs onStart
        Invoke("InitializeGrow", 0.1f);
        if(OnBeginDrag == null)
        {
            print("begin drag is null");
        }
      
	}

    void OnDisable()
    {
        //DON'T DO THIS
       
        //RemoveSubscribers();
    }

    public static void RemoveSubscribers()
    {
        OnDrop = delegate { };
        OnInsert = delegate { };
        OnBeginDrag = delegate { };
        OnReturn = delegate { };
    }

    public void SetParentToParent()
    {
        recentParent = transform.parent;
    }

	
	public void Drag()
	{
		if(dragging){
            if (InventoryManager.ins.eDragDir == EDragDirection.all)
            {
                transform.position = Input.mousePosition;
            }
            else if (InventoryManager.ins.eDragDir == EDragDirection.x)
            {
                transform.SetXPos(Input.mousePosition.x);
            }
            else {
                transform.SetXPos(Input.mousePosition.y);
            }        
		}

	}

	public void Drop()
	{
       
        if (InventoryManager.ins.IsReparented(this.gameObject)) {
            //reparent happened
            print("reparented");
           
            recentParent = transform.parent;
            if (OnDrop != null) {
                print("OnDrop");
                OnDrop();
                transform.SetLocalZRot(transform.parent.GetLocalZRot());   
            }
            if (OnInsert != null)
            {
                print("On Insert");
                OnInsert(transform.parent, this.transform);
            }
            else {
                
                print("NO INSERT");
            }
        }
        else if (InventoryManager.ins.IsSwapped(this.gameObject))
        {
            if (OnDrop != null)
            {
                OnDrop();
            }
        }
        else {
            if (!Locked)
            {
                transform.SetParent(recentParent);
                transform.SetLocalXPos(0);
                transform.SetLocalYPos(0);
                //transform.rotation = Quaternion.Euler(originalRotation);
                //rect.SetWidth(tempW);
                //rect.SetHeight(tempH);
                print("returned");
                if (OnReturn != null)
                {
                    OnReturn(this.transform);
                }
            }
          
          
        }
     
        dragging  =false;
		InventoryManager.ins.Dragging = false;
        if (backToOriginalSizeOnDrop)
        {

            BackToOriginalSize();

        }
    }

	public void Begin()
	{
        InventoryManager.ins.TransItemRecentSlot = recentParent;
        if (!InventoryManager.ins.Dragging && !locked){
            //print("drag begin");
            if (OnBeginDrag != null)
            {
                OnBeginDrag(this.gameObject);
            }
			//remove object from its slot
			transform.SetParent(InventoryManager.ins.transform);
			InventoryManager.ins.Dragging = true;
			InventoryManager.ins.DraggedObject = this.gameObject;
			dragging = true;
            Grow();
           
		}
       
	}

    public void Return()
    {
        transform.SetParent(startOrigin);
        transform.SetLocalXPos(0);
        transform.SetLocalYPos(0);
        transform.SetLocalZRot(startOrigin.GetLocalZRot());

    }


    #region IGrowClick
    [SerializeField]
    private float finalWidth, finalHeight, tempH, tempW;
    private RectTransform rect;

    public float FinalWidth { get { return finalWidth; } }
    public float FinalHeight { get { return finalHeight; } }
    public float tempHeight { get { return tempH; } }
    public float tempWidth { get { return tempW; } }
    public void InitializeGrow() {
        if(growOnClick)
        {
            //rect = GetComponent<RectTransform>();
            //tempH = rect.sizeDelta.y;
            //tempW = rect.sizeDelta.x;
            
           if(finalHeight == 0)
           {
               finalWidth = tempW + (tempW * 0.50f);
               finalHeight = tempH + (tempH * 0.50f);
           }
        }
        
    }
    public void Grow() {
        //try
        //{
        //    Text txtDebug = GameObject.Find("txtDebug").GetComponent<Text>();
        //    if (growOnClick)
        //    {
        //        txtDebug.text += "\n" + "Item.cs " + "Grow()";
        //        txtDebug.text += "\n" + "Item.cs " + "finalWidth=" + finalWidth;
        //        txtDebug.text += "\n" + "Item.cs " + "finalHeight=" + finalHeight;
        //    }
          
        //}
        //catch (Exception ex) { print(ex); }

        if (growOnClick)
        {
            rect.SetWidth(finalWidth);
            rect.SetHeight(finalHeight);
           
        }
    }

  

    public void BackToOriginalSize() {
        if (growOnClick) {
            rect.SetWidth(tempW);
            rect.SetHeight(tempH);
        }
    }
    #endregion
}
