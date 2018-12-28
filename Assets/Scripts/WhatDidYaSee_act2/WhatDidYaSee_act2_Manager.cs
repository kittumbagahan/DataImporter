using UnityEngine;
using System.Collections;

public class WhatDidYaSee_act2_Manager : MonoBehaviour {

    public GameObject container;
    [SerializeField]
    SpotDiffManager spotDiffMan;
    FlyIn flyIn;
    // Grow grow;
    void Awake()
    {
        spotDiffMan.SetIndex = SaveTest.Set;
    }
    void Start()
    {
        flyIn = container.GetComponent<FlyIn>();

        ObjectToSpot.OnCorrect += Fly;
        ObjectToSpot.OnCorrect += Grow;
        ObjectToSpot.OnCorrect += IncPts;
        SpotDiffManager.OnGenerate += ResetObjectsSize;
    }

    void IncPts(GameObject obj)
    {
        spotDiffMan.Pts++;
    }
    void Update()
    {

    }


    void ResetObjectsSize()
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            //RectTransform rect = container.transform.GetChild(i).GetComponent<RectTransform>();
            Transform t = container.transform.GetChild(i);
            if(t.GetComponent<ObjectToSpot>() != null)
            {
                RectTransform rect = t.GetComponent<RectTransform>();
                Grow g = t.GetComponent<Grow>();
                rect.SetHeight(g.GetOriginalHeight());
                rect.SetWidth(g.GetOriginalWidth());
            }
            
        }
    }

    void InvokeFly()
    {
        flyIn.Fly();
    }

    void Fly(GameObject obj)
    {
        if (spotDiffMan.Pts < 2)
        {
            Invoke("InvokeFly", 1f);
        }
        
    }

    void Grow(GameObject obj)
    {
        Grow g = obj.GetComponent<Grow>();
        g.Play();
    }
}
