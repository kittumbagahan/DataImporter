using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserRestrictionController : MonoBehaviour {
    public static UserRestrictionController ins;

    [SerializeField]
    GameObject[] objects;

    public int restriction = -1;

    private void Start()
    {
        ins = this;
    }

    public void Restrict(int restriction)
    {
        this.restriction = restriction;
        if(restriction == 0)
        {
            for(int i=0; i<objects.Length; i++)
            {
                objects[i].SetActive(true);
            }
        }
        else if(restriction == 1)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(false);
            }
        }
    }
}
