using UnityEngine;
using System.Collections;

public class ActivitySelectionManager : MonoBehaviour {
    
    [SerializeField]
    GameObject canvas;



	void Start () {
        canvas = GameObject.Find("Canvas_UI_New");
        //use for going back to active storybook main page
        canvas.GetComponent<SceneLoader>().SceneToLoad = StoryBookSaveManager.ins.GetBookScene(); 
		BG_Music.ins.SetVolume(0.5f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
