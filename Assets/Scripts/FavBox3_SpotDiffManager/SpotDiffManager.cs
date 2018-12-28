using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class SpotDiffManager : AspectManager {
    public static SpotDiffManager ins;
    [SerializeField]
    float[] imgScale;
    public List<Sprite> lstClone;
    public List<Sprite> lstDiff;
    public List<Transform> lstObjectToSpot;
    public delegate void ActionGenerate();
    public delegate void ActionEnd();
    public static ActionGenerate OnGenerate;
    public static ActionEnd OnEnd;
   
    [SerializeField]
    int setIndex = -1, pts=0;
    //float w, h;

    public int SetIndex {
        set { setIndex = value; }
    }
    public int Pts {
        set { pts = value; }
        get { return pts; }
    }
    void Awake()
    {
        ins = this;
    }
	void Start () {
        if (imgScale.Length == 0)
        {
            imgScale = new float[lstClone.Count];
        }
        GenerateSpot();
		Aspect();
        //w = lstObjectToSpot[0].GetComponent<RectTransform>().GetWidth();
        //h = lstObjectToSpot[0].GetComponent<RectTransform>().GetHeight();
	}
	
   
	// Update is called once per frame
	void Update () {
	
	}

   public void GenerateSpot()
    {
       
        StartCoroutine(IEGenerateSpot());
    }

    void Reset()
    {
        ObjectToSpot ots = null;
        for (int i=0; i<lstObjectToSpot.Count; i++)
        {
            ots = lstObjectToSpot[i].GetComponent<ObjectToSpot>();
            ots.different = false;
        }
    }

    IEnumerator IEGenerateSpot()
    {
        yield return new WaitForSeconds(1f);

        if (setIndex < lstClone.Count - 1 && pts < 3)
        {
            setIndex++; //starting from -1
            Reset();
            ObjectToSpot ots = null;
            lstObjectToSpot.Shuffle();
            //clones
            lstObjectToSpot[0].GetComponent<Image>().sprite = lstClone[setIndex];
            lstObjectToSpot[1].GetComponent<Image>().sprite = lstClone[setIndex];
            lstObjectToSpot[2].GetComponent<Image>().sprite = lstClone[setIndex];
            //diff
            lstObjectToSpot[3].GetComponent<Image>().sprite = lstDiff[setIndex];
            ots = lstObjectToSpot[3].GetComponent<ObjectToSpot>();
            ots.different = true;
            for (int i = 0; i < lstObjectToSpot.Count; i++)
            {

                if (imgScale[setIndex] == 0)
                {
                    imgScale[setIndex] = 1f;
                }
                lstObjectToSpot[i].transform.localScale = new Vector3(imgScale[setIndex], imgScale[setIndex]);
            }
            if (OnGenerate != null)
            {
                OnGenerate();
            }
        }
        else {
            
            if(OnEnd != null)
            {
                OnEnd();
            }
            print("GameOver");
            Invoke("Done",0.5f);
        }
        ObjectToSpot.ready = true;
    }

    void OnDestroy()
    {
        OnGenerate = delegate { };
        OnEnd = delegate{};
    }

    void Done()
    {
        ActivityDone.instance.Done();
    }
}
