using UnityEngine;
using System.Collections;

public class SubtitleManager : MonoBehaviour {

    public StoryBookPlayer[] subtitle;
    [SerializeField]
    int recentSubs = -1;


    public void ShowSubs(int index)
    {
     
        HideRecent();
        recentSubs = index;
        //StoryBookPlayer player = null;
        subtitle[index].gameObject.SetActive(true);
        //player = subtitle[index].GetComponent<StoryBookPlayer>();
        //player.PlayTextAnimation();
        subtitle[index].PlayTextAnimation();
    }

    void HideRecent()
    {
        if (recentSubs != -1)
        {
            subtitle[recentSubs].gameObject.SetActive(false);
        }
        
    }
}
