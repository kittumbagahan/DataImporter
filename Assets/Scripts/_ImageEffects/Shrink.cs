using UnityEngine;
using System.Collections;

public class Shrink : MonoBehaviour {

    public bool playOnStart = true;
    public float spd;
    public float startDelay;
    public float initialHeight = 0, initialWidth = 0;
    public float finalHeight=0, finalWidth= 0;
    public bool start = true;
    [SerializeField]
    float h, w;

    public delegate void DelegateShrinkEnd(GameObject obj);
    public static event DelegateShrinkEnd OnShrinkEnd;
    RectTransform rect;
    
    void Awake()
    {
        OnShrinkEnd = delegate { };
    }
    void Start()
    {

        rect = GetComponent<RectTransform>();
        if(finalHeight == 0 && initialHeight == 0)
        {
            initialHeight = rect.sizeDelta.y;
            initialWidth = rect.sizeDelta.x;
          
        }
        h = initialHeight;
        w = initialWidth;
        if(playOnStart)
        {
            Invoke("SetStart", startDelay);
        }
      
    }


    void SetStart()
    {
        start = true;
    }
   public void Play()
    {
        if (finalHeight == 0 && initialHeight == 0)
        {
            initialHeight = rect.sizeDelta.y;
            initialWidth = rect.sizeDelta.x;
            h = initialHeight;
            w = initialWidth;
        }
        else
        {
           // initialHeight = rect.sizeDelta.y;
           // initialWidth = rect.sizeDelta.x;
            h = initialHeight;
            w = initialWidth;
        }
        
        Invoke("SetStart", startDelay);
        //start = true;
    }

   public void Stop()
   {
       start = false;
        
   }

    void Update()
    {
        if (start)
        {
            h = Mathf.Lerp(h, finalHeight, spd);
            w = Mathf.Lerp(w, finalWidth, spd);
            rect.SetWidth(w);
            rect.SetHeight(h);
            if (h < finalHeight + 1f)
            {
                if(OnShrinkEnd != null)
                {
                    OnShrinkEnd(this.gameObject);
                }
                start = false;
                h = initialHeight;
                w = initialWidth;
            }
           
        }
    }
}
