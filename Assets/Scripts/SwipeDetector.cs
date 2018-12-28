using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour
{

    public float minSwipeDistY;

    public float minSwipeDistX;

    private Vector2 startPos;

    void Update()
    {
     #if UNITY_ANDROID
             if (Input.touchCount > 0)        
             {
                 
                 Touch touch = Input.touches[0];
                 
                 switch (touch.phase)  
                 {
                     
                 case TouchPhase.Began:
     
                     startPos = touch.position;
                     print(startPos);
                     break;
                 case TouchPhase.Ended:
     
                  
                         float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
                         print(swipeDistHorizontal);
                         if (swipeDistHorizontal > minSwipeDistX) 
                             
                         {
                             
                             float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                             if (swipeValue > 0)//right swipe
                                 SceneSpawner.ins.Prev();
                             //MoveRight ();

                             else if (swipeValue < 0)//left swipe
                                 SceneSpawner.ins.Next();
                                 //MoveLeft ();
                             
                         }
                     break;
                 }
             }
            #endif
         }
}

