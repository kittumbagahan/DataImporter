using UnityEngine;
using System.Collections;

public class UserRecent{

    public void SaveRecentUser(string currentUser) 
    {
        PlayerPrefs.SetString("recent_user", currentUser);
    }

    public void LoadRecentUser()
    {
        Singleton.userActive = true;
        string s = PlayerPrefs.GetString("recent_user");
        if(s != "")
        StoryBookSaveManager.ins.Username = s;
    }
}
