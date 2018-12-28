using UnityEngine;
using System.Collections;

public class FavBox_Act3_Manager : MonoBehaviour {
    
    public GameObject container;
    public SpotDiffManager spotDiffManager;
    //StoryBookSaveManager saveManager = new StoryBookSaveManager();
    FlyIn flyIn;
   // Grow grow;
	void Start () {
        flyIn = container.GetComponent<FlyIn>();

       
        ObjectToSpot.OnCorrect += Fly;
        ObjectToSpot.OnCorrect += Grow;
        ObjectToSpot.OnCorrect += IncPts;
        SpotDiffManager.OnGenerate += ResetObjectsSize;
        SpotDiffManager.OnEnd += Save;
        spotDiffManager.SetIndex = SaveTest.Set;

		ScoreManager.ins.maxMove = 3;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Save()
    {
     //   StoryBookSaveManager.Save(StoryBook.FAVORITE_BOX, Module.OBSERVATION);
    }
    void ResetObjectsSize()
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            //RectTransform rect = container.transform.GetChild(i).GetComponent<RectTransform>();
            Transform t = container.transform.GetChild(i);
            if (t.GetComponent<ObjectToSpot>() != null)
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
        if(SpotDiffManager.ins.Pts < 3)
        {
            flyIn.Fly();
        }
    }

    void Fly(GameObject obj)
    {
        Invoke("InvokeFly",1f);
    }

    void Grow(GameObject obj)
    {
        Grow g = obj.GetComponent<Grow>();
        g.Play();
    }

    void IncPts(GameObject obj)
    {
        spotDiffManager.Pts++;
    }
}
