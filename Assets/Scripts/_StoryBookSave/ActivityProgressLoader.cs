using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ActivityProgressLoader : MonoBehaviour {

	[SerializeField]
	RectTransform[] star;

	[SerializeField]
	Texture starDone, starNotDone;

	[SerializeField]
	int totalActivitiesPerModule, totalActivityDone;

	public StoryBook storyBook;
	public Module module;
	//public ActivityNumber actNumber;

	// Use this for initialization
	void Start () {
		//StoryBookSaveManager.instance.Load()
		//PlayerPrefs.DeleteAll();
		LoadStar();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoadStar()
	{
        //for (int i = 0; i < StoryBookSaveManager.Load(storyBook, module); i++) 
        //{
        ////	star[i].GetComponent<RawImage>().texture = starDone;
        //}
	}
}
