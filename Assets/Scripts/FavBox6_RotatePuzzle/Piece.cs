using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
//using Holoville.HOTween;
using UnityEngine.UI;


public class Piece : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    bool isCorrect = false;
    [SerializeField]
    bool isPressable;

    //[SerializeField]
    //AudioClip audioClip;

    public delegate void CheckPiece();
    public static event CheckPiece OnCheckPiece;

	public delegate void PlayAudio();
	public static event PlayAudio onPuzzlePieceClick;

    public int value = 0;
    public int cnt = 0;
    public bool play, stop;
    RectTransform rect;
    //Quaternion quatStart, quatEnd;
    [SerializeField]
    float time = 1f, spd = 0.2f;
    float tempTime;
    float rotationDeg = 180;
    float tempRotationDeg;
    [SerializeField]
    FadeIn fadeIn;
    //Spin spinner;

    public bool Play {
        get { return play; }
        set { play = value;
       
        }
    }
    void Awake()
    {
        //quatStart = new Quaternion(0, 0, 0, 1);
        //quatEnd = new Quaternion(0, 0, 0, 1);
        //quatEnd.eulerAngles = new Vector3(0, 0, rotationDeg);
        //quatStart.eulerAngles = new Vector3(0, 0, 0);
        //spinner = GetComponent<Spin>();
        rect = GetComponent<RectTransform>();
        fadeIn = GetComponent<FadeIn>();
    }
    void Start () {                
        print("piece");
        tempTime = time;
        tempRotationDeg = rotationDeg;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        print("click");
        if (isPressable)
        {
            value++;
            
            //fadeIn.Play();
            if (value % 2 == 0)
            {
                //isCorrect = false;
                //transform.SetLocalZRot(180);
                transform.rotation = Quaternion.Euler(0,0,180f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                //transform.SetLocalZRot(0);
               // isCorrect = true;
            }
            //spinner.finalZ += 180f;
            Rotation();
            if (onPuzzlePieceClick != null)
            {
                onPuzzlePieceClick();
            }
            //spinner.SpinToFinalZ();
            //Check();
            //Invoke("Check", 2f);
            //StartCoroutine("IERotate");
        }
    }

    //void Spin()
    //{
    //    if (play)
    //    {
    //        transform.rotation = Quaternion.Slerp(quatStart, quatEnd, time);
    //        //quatStart.eulerAngles = new Vector3(0,0,quatStart.eulerAngles.z + time);
    //        time += (Time.fixedDeltaTime *spd);
    //       // print(transform.eulerAngles.z + "----------" + quatEnd.eulerAngles.z);
    //        //print("spinning ..." + time);
            
    //        if (transform.eulerAngles.z == quatEnd.eulerAngles.z)
    //        {
    //            //print("NOT spinning ..." + time);
    //            cnt++;
    //            time = tempTime;
    //            quatStart = quatEnd;
    //            rotationDeg += tempRotationDeg;
    //            if (rotationDeg >= 360)
    //            {
    //                rotationDeg = 0;
    //            }
    //            quatEnd.eulerAngles = new Vector3(0, 0, rotationDeg);
    //            play = false;
               
    //        }
    //    }
    //}

    

    void Update()
    {
       // Spin();
        
    }

    void Check()
    {
        if (OnCheckPiece != null)
        {
            OnCheckPiece();
        }
    }

    #region kit
    //public void Rotate()
    //{
        
    //    StartCoroutine("IERotate");
    //}

    //IEnumerator IERotate()
    //{
    //    //iTween.RotateAdd(gameObject, new Vector3(0, 0, 180), 0.2f);

    //    print(gameObject.GetComponent<RectTransform>().rotation.z);
    //    isPressable = false;
    //    HOTween.To(gameObject.transform, 0.2f, "rotation", new Vector3(0, 0, -180), true);

    //    yield return new WaitForSeconds(0.5f);
    //    //print(Mathf.Ceil(transform.GetComponent<RectTransform>().localRotation.z));
    //    isPressable = true;

    //    //if(Mathf.Ceil(transform.GetComponent<RectTransform>().localRotation.z) == 0 || Mathf.Ceil(transform.GetComponent<RectTransform>().localRotation.z) % 180 == 0)
    //    //{
    //    //    isCorrect = true;
    //    //}
    //    //else
    //    //{
    //    //    isCorrect = false;
    //    //}
    //    Rotation();
    //    print(isCorrect);        

    //    if (OnCheckPiece != null)
    //    {
    //        OnCheckPiece();
    //    }
    //}

    public void Rotation()
    {
        //if (Mathf.Ceil(transform.GetComponent<RectTransform>().localRotation.z) == 0 || Mathf.Ceil(transform.GetComponent<RectTransform>().localRotation.z) % 180 == 0)
        if(value % 2 == 0)
        //if(transform.eulerAngles.z == 0)
        {
            isCorrect = false;
        }
        else
        {
            isCorrect = true;
        }
    }

    #endregion kit

    public bool IsCorrect
    {
        get { return isCorrect; }
        set { isCorrect = value; }
    }

    public bool IsPressable
    {
        get { return isPressable; }
        set { isPressable = value;
           
        }
    }
}
