using UnityEngine;
using System.Collections;

//public enum ActivityNumber { ONE, TWO, THREE }
public enum Module { WORD, PUZZLE, OBSERVATION }

public class StoryBookSaveManager : MonoBehaviour {

	public static StoryBookSaveManager ins;
	
	//ActivityNumber activityNumber;
    public string activeUser;
    public int activeUser_id;
    public string activeSection;
    public int activeSection_id;

    public StoryBook selectedBook;
    public static int maxScore;
    
    public static int currentScore;
	static int activityDone = 0;

    private string scene;

    public string GetBookScene()
    {
        switch (selectedBook)
        {
            case StoryBook.NULL:
                return "";
            case StoryBook.FAVORITE_BOX:
                return "FavoriteBox";
            case StoryBook.AFTER_THE_RAIN:
                return "AfterTheRain";
            case StoryBook.CHAT_WITH_MY_CAT:
                return "ChatWithMyCat";
            case StoryBook.COLORS_ALL_MIXED_UP:
                return "Colors All Mixed Up";
            case StoryBook.WHAT_DID_YOU_SEE:
                return "WhatDidYouSee";
            case StoryBook.ABC_CIRCUS:
                return "ABC_Circus";
            case StoryBook.JOEY_GO_TO_SCHOOL:
                return "JoeyGoesToSchool";
            case StoryBook.SOUNDS_FANTASTIC:
                return "SoundsFantastic";
            case StoryBook.YUMMY_SHAPES:
                return "YummyShapes";
            case StoryBook.TINA_AND_JUN:
                return "TinaAndJun";
            default:
                return "";
        }
       
    }

    public string Username {
        get {
            if (activeUser != "")
            {
                return activeUser;
            }
            else {
                return "Guest";
            }
        }
        set {
            activeUser = value;
        }
    }

    void OnEnable()
    {
        if(ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ins = this;
        }
       
    }
    // Use this for initialization
    void Start () 
	{
      //  PlayerPrefs.DeleteAll();
		
	}

}
