using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class FlyIn : MonoBehaviour {

    public bool playOnStart = true;
    public Direction fromDirection;
    public Direction toDirection;

    public float startDelay, stayTime, spd;
    public bool setDestinationToCurrentPos = false;
    public float destinationX, destinationY;

    private bool reached;
    //RectTransform rect;
    Image img;
    RawImage rImg;
    void Awake()
    {
        img = GetComponent<Image>();
        rImg = GetComponent<RawImage>();
        if (img != null) img.enabled = false;
        if (rImg != null) rImg.enabled = false;
        if (setDestinationToCurrentPos)
        {
            destinationX = transform.GetLocalXPos();
            destinationY = transform.GetLocalYPos();
        }
    }
	void Start () {
      //  rect = GetComponent<RectTransform>();
       if(playOnStart)
       {
           Invoke("Fly", startDelay);
       }
       
    }

    public void Fly()
    {
      
        switch (fromDirection)
        {
            case Direction.up: transform.SetLocalYPos(800f * 2); break;
            case Direction.down: transform.SetLocalYPos(-800f * 2); break;
            case Direction.left: transform.SetLocalXPos(-600f * 2); break;
            case Direction.right: transform.SetLocalXPos(600 * 2); break;
            default: break;
        }
        if (img != null) img.enabled = true;
        if (rImg != null) rImg.enabled = true;
        reached =false;
        if (!playOnStart)
        {
            Invoke("Play", startDelay);
        }
        else {
            StartCoroutine(IEFlyTo(toDirection));
        }
    }

    void Play()
    {
        StartCoroutine(IEFlyTo(toDirection));
    }

    IEnumerator IEFlyTo(Direction dir)
    {
       
        while(!reached)
        {
          
            yield return new WaitForSeconds(0.001f);
            switch (dir)
            {
                case Direction.up:
                    if (transform.GetLocalYPos() <= destinationY)
                    {
                        transform.SetLocalYPos(Mathf.Lerp(transform.GetLocalYPos(), destinationY +1, spd));
                      //  print(reached);
                    }
                    else {
                        reached = true;
                    }
                    break;
                case Direction.down:
                    if (transform.GetLocalYPos() >= destinationY)
                    {
                        transform.SetLocalYPos(Mathf.Lerp(transform.GetLocalYPos(), destinationY -1, spd));
                      //  print(reached);
                    }
                    else
                    {
                        reached = true;
                    }
                    break;
                case Direction.left:
                    if (transform.GetLocalXPos() >= destinationX)
                    {
                        transform.SetLocalXPos(Mathf.Lerp(transform.GetLocalXPos(), destinationX - 1, spd));
                      //  print(reached);
                    }
                    else
                    {
                        reached = true;
                    }
                    break;
                case Direction.right:
                    if(transform.GetLocalXPos() < destinationX)
                    {
                        transform.SetLocalXPos(Mathf.Lerp(transform.GetLocalXPos(), destinationX+1, spd));
                        //print(reached + rect.GetXPos().ToString());
                       // print(transform.GetLocalXPos());
                    }
                    else
                    {
                        print("stop");
                        reached = true;
                    }
                    break;
                default:
                    break;
            }

           
        }
      
    }
}
