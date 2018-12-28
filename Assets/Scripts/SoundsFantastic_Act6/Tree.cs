using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Tree : MonoBehaviour {


    [SerializeField]
    Transform destination;
     float movSpeed = 30f;
    float growSpeed = 0.5f;
    float origSpd;
    [SerializeField]
    Vector2 orignalSize;
    float scale=0;
    bool stop;

    void Awake()
    {
        orignalSize = transform.localScale;
        scale = orignalSize.x;
        origSpd = movSpeed;
    }
	void Start () {
       
    }
    public bool Stop {
        set { stop = value; }
    }
    public void SetDestination(Transform pointB)
    {
        destination = pointB;
    }
    public float MoveSpd {
        set { movSpeed = value; }
    }
    public float GrowSpd {
        set { growSpeed = value; }
    }
	// Update is called once per frame
	void Update () {
        if(destination != null)
        {

           
	
        }
        if(!stop)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.position, movSpeed * Time.deltaTime);
            scale += Time.deltaTime * growSpeed;
            movSpeed += (movSpeed * Time.deltaTime) / 2;
            transform.SetLocalWidth(scale);
            transform.SetLocalHeight(scale);

            if (Vector2.Distance(transform.position, destination.position) < 100)
            {
                BecameInvisible();
            }
        }
     
    }

    void BecameInvisible()
    {
        scale = orignalSize.x;
        transform.SetLocalWidth(orignalSize.x);
        transform.SetLocalHeight(orignalSize.y);
        gameObject.SetActive(false);

        movSpeed = origSpd;
    }
}
