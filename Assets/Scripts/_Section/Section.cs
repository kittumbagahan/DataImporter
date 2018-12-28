using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Section : MonoBehaviour {

    public int id;
    public string name;
    public string UID;

    public void Click()
    {
        if (SectionController.ins.editMode)
        {
            SectionController.ins.Edit(this);
        }
        else
        {
            MessageBox.ins.ShowQuestion("Load section " + name + "?", MessageBox.MsgIcon.msgInformation, new UnityAction(LoadYes), new UnityAction(LoadNo));
        }
     
    }

    void LoadYes()
    {
        StoryBookSaveManager.ins.activeSection = name;
        StoryBookSaveManager.ins.activeSection_id = id;
        SectionController.ins.Close();
        StudentController.ins.LoadStudentsSQL();
    }

    void LoadNo()
    {

    }
}
