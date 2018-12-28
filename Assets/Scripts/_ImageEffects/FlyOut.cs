using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlyOut : MonoBehaviour
{

    public bool playOnStart = true;
    // public Direction fromDirection;
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
    void Start()
    {
        //  rect = GetComponent<RectTransform>();
        if (playOnStart)
        {
            Invoke("Fly", startDelay);
        }

    }

    public void Fly()
    {

        //switch (fromDirection)
        //{
        //    case Direction.up: transform.SetLocalYPos(800f * 2); break;
        //    case Direction.down: transform.SetLocalYPos(-800f * 2); break;
        //    case Direction.left: transform.SetLocalXPos(-600f * 2); break;
        //    case Direction.right: transform.SetLocalXPos(600 * 2); break;
        //    default: break;
        //}
        if (img != null) img.enabled = true;
        if (rImg != null) rImg.enabled = true;
        reached = false;
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
        
        while (!reached)
        {
           // print("sd");
            yield return new WaitForSeconds(0.001f);
            switch (dir)
            {
                case Direction.up:
                    if (transform.GetLocalYPos() <= destinationY)
                    {
                        transform.SetLocalYPos(Mathf.Lerp(transform.GetLocalYPos(), destinationY + 1, spd));
                        //  print(reached);
                    }
                    else
                    {
                        reached = true;
                    }
                    break;
                case Direction.down:
                    if (transform.GetLocalYPos() >= destinationY)
                    {
                        transform.SetLocalYPos(Mathf.Lerp(transform.GetLocalYPos(), destinationY - 1, spd));
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
                    if (transform.GetLocalXPos() < destinationX)
                    {
                        transform.SetLocalXPos(Mathf.Lerp(transform.GetLocalXPos(), destinationX + 1, spd));
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


        ////public Direction fromDirection;
        //public Direction toDirection;

        //public float startDelay, stayTime, spd;

        //public float destinationX, destinationY;
        //bool start;
        //RectTransform rect;
        //void Start()
        //{
        //    rect = GetComponent<RectTransform>();


        //}

        //void FlyTo()
        //{
        //    //in case of wrong input
        //    switch (toDirection)
        //    {
        //        case Direction.up:
        //            break;
        //        case Direction.down:
        //            if (destinationY > -1) destinationY *= -1;
        //            break;
        //        case Direction.left:
        //            if (destinationX > -1) destinationX *= -1;
        //            break;
        //        case Direction.right:
        //            break;
        //        default:
        //            break;
        //    }
        //    StartCoroutine(IEFlyTo(toDirection));
        //}

        //public void Play()
        //{
        //    start = true;
        //    Invoke("FlyTo", startDelay);
        //}

        //IEnumerator IEFlyTo(Direction dir)
        //{
        //    while (start)
        //    {

        //        yield return new WaitForSeconds(0.001f);
        //        switch (dir)
        //        {
        //            case Direction.up:
        //                rect.SetLocalYPos(Mathf.Lerp(rect.GetLocalYPos(), destinationY, spd));
        //                break;
        //            case Direction.down:
        //                rect.SetLocalYPos(Mathf.Lerp(rect.GetLocalYPos(), destinationY, spd));
        //                break;
        //            case Direction.left:
        //                rect.SetLocalXPos(Mathf.Lerp(rect.GetLocalXPos(), destinationX, spd));
        //                break;
        //            case Direction.right:
        //                rect.SetLocalXPos(Mathf.Lerp(rect.GetLocalXPos(), destinationX, spd));
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //}
   
}