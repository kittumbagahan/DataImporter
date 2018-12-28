using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SoundsFantastic_Act6_Manager : MonoBehaviour {

    public static SoundsFantastic_Act6_Manager ins;

    [SerializeField]
    AudioClip clipCarMoving, clipCarCrash, clipTryAgain;
    public Transform movable;
    public Transform bg;
    public Transform wheel;
    public Transform leftRd, rightRd;
    public GameObject tree;
    public GameObject rock;
    [SerializeField]
    Transform startL, startR;
    [SerializeField]
    Transform destinationL, destinationR;
    [SerializeField]
    Transform destination_rock_L, destination_rock_R;
    public List<GameObject> treeList = new List<GameObject>();
    public List<GameObject> rockList = new List<GameObject>();
	[SerializeField]
	GameObject tryAgain;
    public bool L, R;
    //[SerializeField]
    private bool tempL, tempR;
    AudioSource audSrc;

    public float wheelAngleL, wheelAngleR;
    [SerializeField]
    int pts = 0;
    bool stop;
    
	public GameObject ObjTryAgain(){return tryAgain;}
	public bool Left() { return tempL; }
    public bool Right() { return tempR; }
    public int Pts {
        set { pts = value; }
        get { return pts; }
    }
    void Start () {
        audSrc = GetComponent<AudioSource>();
        audSrc.clip = clipCarMoving;
        audSrc.Play();
        InvokeRepeating("SpawnTree",0,0.6f);
        InvokeRepeating("SpawnRock",1f,3f);
       // SpawnRock();
        ins = this;
		ScoreManager.ins.AW();
	}
	
	// Update is called once per frame
	void Update () {
      //  L = Input.GetKey(KeyCode.LeftArrow);
       // R = Input.GetKey(KeyCode.RightArrow);
        if (L)
        {
            R = false;
            StartCoroutine(IETurnRight());
            tempL = true;
            tempR = false;
        }

        if (R)
        {
            L = false;
            StartCoroutine(IETurnLeft());
            tempR = true;
            tempL = false;
        }
        if(pts == 6)
        {
            ActivityDone.instance.Done();
            audSrc.Stop();
            pts--;
            Stop();
        }
	}

	public void TryAgain()
	{
		

		EmptySceneLoader.ins.sceneToLoad = "SoundsFantastic_Act6";
		SceneLoader.instance.SceneToLoad = "empty";
		SceneLoader.instance.AsyncLoadStr("empty");
	}

    public void TurnLeft(){
		ScoreManager.ins.IncNumOfMoves();
        if(!stop && !tempL)
        L = true;
     
    }

    public void TurnRight(){
		ScoreManager.ins.IncNumOfMoves();
        if(!stop && !tempR)
        R = true;
       
    }
    IEnumerator IETurnLeft()
    { 
        while(R)
        {
            movable.position = Vector2.Lerp(movable.position, leftRd.position, 0.5f * Time.fixedDeltaTime);
            //wheel.SetLocalZRot(wheelAngleR));
            wheel.SetLocalZRot(wheelAngleR);
            if(Vector2.Distance(movable.position, leftRd.position) <= 1)
            {
                R = false;
            }
            yield return new WaitForSeconds(0.00001f);
        }
        wheel.SetLocalZRot(0);
    }
    IEnumerator IETurnRight()
    {
        while (L)
        {
            movable.position = Vector2.Lerp(movable.position, rightRd.position, 0.5f * Time.fixedDeltaTime);
            wheel.SetLocalZRot(wheelAngleL);
            if (Vector2.Distance(movable.position, rightRd.position) <= 1)
            {
                L = false;
            }
            yield return new WaitForSeconds(0.00001f);
        }
        wheel.SetLocalZRot(0);
    }
    public void Shake()
    {
        audSrc.clip = null;
        audSrc.PlayOneShot(clipCarCrash);
        Stop();
        StartCoroutine(IEShake());
    }
    public IEnumerator IEShake()
    {
        for (int i = 0; i < 10; i++)
        {
            movable.SetXPos(Random.Range(movable.GetXPos() - 10f, movable.GetXPos() + 10f));
            //print(i);
            yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
        }
        if(tempR)
        {
            movable.SetXPos(leftRd.GetXPos());
        }
        if (tempL)
        {
            movable.SetXPos(rightRd.GetXPos());
        }
		audSrc.PlayOneShot(clipTryAgain);
		tryAgain.SetActive(true);
		//yield return new WaitForSeconds(1f);
		//ActivityDone.instance.Done();
    }

    public void Stop()
    {
        stop = true;
        for (int i = 0; i < treeList.Count; i++ )
        {
            treeList[i].GetComponent<Tree>().Stop = true;
        }
        for (int i = 0; i < rockList.Count; i++ )
        {
            rockList[i].GetComponent<Rock>().Stop = true;
        }
    }

    void SpawnRock()
    { 
        GameObject r = null;
        bool f=false;
        if(!stop)
        {
            int n = Random.Range(0, 100);
            for (int i = 0; i < rockList.Count; i++)
            {
                if (rockList[i].activeInHierarchy == false)
                {

                    rockList[i].SetActive(true);
                    rockList[i].GetComponent<Rock>().wheel = wheel;
                    if (n < 50)
                    {
                        rockList[i].GetComponent<Rock>().SetDestination(destination_rock_L);
                        rockList[i].transform.SetAsFirstSibling();
                        rockList[i].transform.SetLocalXPos(-94f);
                        rockList[i].transform.SetLocalYPos(102f);

                    }
                    else
                    {

                        rockList[i].GetComponent<Rock>().SetDestination(destination_rock_R);
                        rockList[i].transform.SetAsFirstSibling();
                        rockList[i].transform.SetLocalXPos(89f);
                        rockList[i].transform.SetLocalYPos(102f);
                    }

                    f = true;
                }
            }
            if (!f)
            {
                r = (GameObject)Instantiate(rock, transform.position, Quaternion.identity);
                rockList.Add(r);
                r.transform.SetParent(bg);
                r.transform.SetAsFirstSibling();
                r.GetComponent<Rock>().wheel = wheel;
                if (n < 50)
                {
                    r.GetComponent<Rock>().SetDestination(destination_rock_L);
                    //r.transform.position = new Vector2(-94f, 102f);
                    r.transform.SetLocalXPos(-94f);
                    r.transform.SetLocalYPos(102f);

                }
                else
                {
                    r.GetComponent<Rock>().SetDestination(destination_rock_R);
                    //r.transform.position = new Vector2(89f, 102f);
                    r.transform.SetLocalXPos(89f);
                    r.transform.SetLocalYPos(102f);
                }
            }
        }
       
    }

    void SpawnTree()
    {
        GameObject left = null;//(GameObject)Instantiate(tree, startL.position, Quaternion.identity);
        GameObject right = null;//(GameObject)Instantiate(tree, startR.position, Quaternion.identity);
        bool f =false;
        if(!stop)
        {
            for (int i = 0; i < treeList.Count && !f; i++)
            {
                if (treeList[i].activeInHierarchy == false)
                {
                    treeList[i].SetActive(true);
                    treeList[i].transform.position = startL.position;
                    treeList[i].transform.SetAsFirstSibling();
                    treeList[i].GetComponent<Tree>().SetDestination(destinationL);
                    f = true;
                }
            }
            f = false;
            for (int i = 0; i < treeList.Count && !f; i++)
            {
                if (treeList[i].activeInHierarchy == false)
                {
                    treeList[i].SetActive(true);
                    treeList[i].transform.position = startR.position;
                    treeList[i].transform.SetAsFirstSibling();
                    treeList[i].GetComponent<Tree>().SetDestination(destinationR);
                    f = true;
                }
            }
            if (!f)
            {
                left = (GameObject)Instantiate(tree, startL.position, Quaternion.identity);
                right = (GameObject)Instantiate(tree, startR.position, Quaternion.identity);
                treeList.Add(left);
                treeList.Add(right);
                right.transform.SetParent(bg);
                left.transform.SetParent(bg);
                left.transform.SetAsFirstSibling();
                right.transform.SetAsFirstSibling();
                left.GetComponent<Tree>().SetDestination(destinationL);

                right.GetComponent<Tree>().SetDestination(destinationR);

            }
       
    
        }
    }
}
