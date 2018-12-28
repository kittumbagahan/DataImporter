using UnityEngine;
using System.Collections;

public class _LoadTestButton : MonoBehaviour {
    //trash
    public int setIndex;
    public StoryBook book;
    public Module type;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Click()
    {
        //_SaveTest.Save(book, type, setIndex);
        LoadScene();
    }

    void LoadScene() {
        switch (book) { 
            case StoryBook.FAVORITE_BOX:
                switch (type){
                    case Module.WORD:
						SaveTest.Set = setIndex;
                        Application.LoadLevel("favBox_Act1_word");
                        break;
                    case Module.PUZZLE:
                        Application.LoadLevel("favBox_Act2_colorning");
                        break;
                }
                break;
            
            default: break;
        }
    }
}
