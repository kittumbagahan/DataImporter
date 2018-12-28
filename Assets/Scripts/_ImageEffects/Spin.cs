using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{
    public bool playOnStart = true;
    RectTransform rect;
   // Vector3 rot;
    [SerializeField]
    float z = 0;
    public float spd, finalZ, startDelay;
    float originalZ;
    [SerializeField]
    bool start;

   // public float FinalZ { set { finalZ = value; } }

    void Start()
    {
        rect = GetComponent<RectTransform>();
        originalZ = rect.transform.GetLocalZRot();
        //finalZ = rect.transform.GetZPos();
        if(playOnStart)
        {
            Invoke("SpinIt", startDelay);
        }
      
       
    }

    public void Stop()
    {
        start = false;
    }

    public void SpinIt()
    {
        start = true;
        StartCoroutine(IEStart());
     
    }
    public void SpinToFinalZ()
    {
        start = true;
        StartCoroutine(IEPlayToFinalZ());

    }
    //this is made for favorite box 6
    IEnumerator IEPlayToFinalZ()
    {
        while (z < finalZ && start)
        {
            z += spd;
            yield return new WaitForSeconds(0.01f);
            rect.transform.rotation = Quaternion.Euler(0, 0, z);
        }
        z = 0;
        while (finalZ >= 180)
        {
            finalZ -= 360;
        }
        if (finalZ < 0)
        {
            finalZ = 180;
        }
        else
        {
            finalZ = 360;
        }
        rect.transform.rotation = Quaternion.Euler(0, 0, finalZ);
        start = false;
        StopCoroutine(IEPlayToFinalZ());
    }

    IEnumerator IEStart()
    {
        //
        while(z < finalZ && start)
        {
            z += spd;
            yield return new WaitForSeconds(0.01f);
            rect.transform.rotation = Quaternion.Euler(0, 0, z);
        }
        z = 0;
       
        rect.transform.rotation = Quaternion.Euler(0, 0, originalZ);
    }
}
