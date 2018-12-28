using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class ExitApp : MonoBehaviour {

    UserRecent recent;

    void CloseMe()
    {
      //  recent.SaveRecentUser(StoryBookSaveManager.instance.oldUsername);
        Application.Quit();
    }
    void CancelClose()
    {
        print("Quit cancelled!");
    }
    public void Close()
    {
        MessageBox.ins.ShowQuestion("Quit game?", MessageBox.MsgIcon.msgInformation, new UnityAction(CloseMe), new UnityAction(CancelClose));
    }

    


   
}
