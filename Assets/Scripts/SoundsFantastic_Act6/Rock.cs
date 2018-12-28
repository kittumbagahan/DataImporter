using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    public Transform wheel;
    [SerializeField]
    Transform destination;
    float movSpeed = 30f;
    float growSpeed = 0.3f;
    float origSpd;
    [SerializeField]
    Vector2 orignalSize;
    float scale = 0;

    bool stop;
    void Awake()
    {
        orignalSize = transform.localScale;
        scale = orignalSize.x;
        origSpd = movSpeed;
    }
    void Start()
    {

    }
    public bool Stop {
        set { stop = value; }
    }
    public void SetDestination(Transform pointB)
    {
        destination = pointB;
    }
    public float MoveSpd
    {
        set { movSpeed = value; }
    }
    public float GrowSpd
    {
        set { growSpeed = value; }
    }
    // Update is called once per frame
    void Update()
    {
        if (destination != null)
        {



        }
        if (wheel != null && !stop) { 
            //if(Vector2.Distance(transform.position, wheel.position) <= 125f)
            //{
            //    print("BOOOM!");
            //}
            //print(Vector2.Distance(transform.position, wheel.position));
            if(transform.position.y < wheel.position.y)
            {
               
                if(SoundsFantastic_Act6_Manager.ins.Left() && transform.localPosition.x < -1)
                {
                    print("crash on the left");
                   // StartCoroutine(SoundsFantastic_Act6_Manager.ins.IEShake());
                    SoundsFantastic_Act6_Manager.ins.Shake();
                }
                else if (SoundsFantastic_Act6_Manager.ins.Right() && transform.localPosition.x > 1)
                {
                    print("crash on the right");
                   // StartCoroutine(SoundsFantastic_Act6_Manager.ins.IEShake());
                    SoundsFantastic_Act6_Manager.ins.Shake();
                }
                else
                {
                    print("Nice!");
                    SoundsFantastic_Act6_Manager.ins.Pts++;
                }
                BecameInvisible();
                //gameObject.SetActive(false);
            }
        }
        
        transform.position = Vector2.MoveTowards(transform.position, destination.position, movSpeed * Time.deltaTime);
        movSpeed += (movSpeed * Time.deltaTime)/2;
        scale += Time.deltaTime * growSpeed;
        transform.SetLocalWidth(scale);
        transform.SetLocalHeight(scale);

        if (Vector2.Distance(transform.position, destination.position) < 100)
        {
            BecameInvisible();
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
