using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour {

	static Singleton instance;
    public static bool mute = false;
	static StoryBook selectedStorybook;
    public static bool userActive;
  
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
            DontDestroyOnLoad(gameObject);
        }
		else
		{
			Destroy(gameObject);
         
        }
	}

	public static StoryBook SelectedBook
	{
		get{ return selectedStorybook; }
		set{ selectedStorybook = value; }
	}
}
