using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Grow : MonoBehaviour {

    public bool playOnStart = true;
    public float spd;
    public float startDelay;
    public delegate void ActionEnd(GameObject obj);
    public static ActionEnd OnEnd;
    [SerializeField]
    float h = 0, w = 0, tempH, tempW;

    public float finalHeight, finalWidth;

    public bool start = true;

    RectTransform rect;
    Image img;
    RawImage rImg;
    public float GetOriginalHeight() {
        return tempH;
    }

    public float GetOriginalWidth() {
        return tempW;
    }

    void Awake()
    {
        img = GetComponent<Image>();
        rImg = GetComponent<RawImage>();
        if (img != null && finalHeight == 0) {
            img.enabled = false;
        }
        if (rImg != null && finalHeight == 0)
        {
            rImg.enabled = false;
        }
    }

    void Start()
    {
       
        rect = GetComponent<RectTransform>();
        if (finalHeight == 0)
        {
            //print("wewe");
            finalHeight = rect.sizeDelta.y;
            finalWidth = rect.sizeDelta.x;
          
            if (rect != null)
            {
                rect.SetWidth(0);
                rect.SetHeight(0);
            }
        //    print("q");
        }
        else {
            tempH = rect.sizeDelta.y;
            tempW = rect.sizeDelta.x;
        }

       
        
        if(playOnStart)
        {
            Invoke("SetStart", startDelay);
        }
       

    }

    public void SetStart()
    {
        if (img != null)
        {
            img.enabled = true;
        }
        if (rImg != null)
        {
            rImg.enabled = true;
        }
        h = tempH;
        w = tempW;
        start = true;  
    }

    public void Play()
    {
        print("Grow");
        if (finalHeight == 0)
        {
            rect.SetWidth(0);
            rect.SetHeight(0);
            h = 0; w = 0;
            if (img != null)
            {
                img.enabled = false;
            }
        }
        else {
            rect.SetWidth(tempW);
            rect.SetHeight(tempH);
            h = tempH; w = tempW;
        }
        
        Invoke("SetStart", startDelay);
    }

    public void Stop()
    {
        start = false;
    }
    void Update()
    { 
        if(start)
        {
            h = Mathf.Lerp(h, finalHeight, spd);
            w = Mathf.Lerp(w, finalWidth, spd);
            rect.SetWidth(w);
            rect.SetHeight(h);
            if(h >= finalHeight - 1){
                start = false;
                if(OnEnd != null)
                {
                    OnEnd(this.gameObject);
                }
            }
        }
    }
}
