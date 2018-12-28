using UnityEngine;
using System.Collections;

public class UpAndDownAnimation : MonoBehaviour {
   
    public float spd = 0.02f;
    public float timer = 0.5f;
    public bool up, down;

    float tempTime;
    [SerializeField]
    private RectTransform rectTrans;

    void Start()
    {
        tempTime = timer;
        rectTrans = GetComponent<RectTransform>();
       
        StartCoroutine(IEUpAndDown());
    }
    void Update()
    {
      
    }

    IEnumerator IEUpAndDown()
    {
        while (true)
        {
           // up = true;
            while (up)
            {
                yield return new WaitForSeconds(0.01f);
                Up();
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
           
            //down = false;
            while (down)
            {
                yield return new WaitForSeconds(0.01f);
                Down();
            }
        }
    }

    void Up()
    {
        if (timer > 0)
        {
            rectTrans.SetYPos(rectTrans.GetYPos() + Time.deltaTime * spd);
            timer -= Time.deltaTime * 0.1f;
        }
        else {
            up = false;
            timer = tempTime;
            down = true;
        }
        
    }

    void Down()
    {
        if (timer > 0)
        {
            rectTrans.SetYPos(rectTrans.GetYPos() - Time.deltaTime * spd);
            timer -= Time.deltaTime * 0.1f;
        }
        else
        {
            down = false;
            timer = tempTime;
            up = true;
        }
    }
   
    
}
