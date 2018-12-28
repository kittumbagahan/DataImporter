using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Student : MonoBehaviour {

    public int id;
    public string name;
    public string UID;

    public void Click()
    {
        if (StudentController.ins.editMode)
        {
            StudentController.ins.Edit(this);
        }
        else
        {
            //MessageBox.ins.ShowQuestion("Load student " + name + "?", MessageBox.MsgIcon.msgInformation,
            //    new UnityAction(LoadYes), new UnityAction(LoadNo));
            if (0 != UserRestrictionController.ins.restriction)
            {
                MessageBox.ins.ShowQuestion("Load student " + name + "?", MessageBox.MsgIcon.msgInformation, 
                    new UnityAction(LoadYes), new UnityAction(LoadNo));
            }
            else
            {
                MessageBox.ins.ShowOk("Teachers can't play with student account.", MessageBox.MsgIcon.msgInformation, null);
            }
           
        }
       
    }

    void LoadYes()
    {
        StoryBookSaveManager.ins.activeUser = name;
        StoryBookSaveManager.ins.activeUser_id = id;
        StudentController.ins.Close();
        print("Hey " + name.Split(' ')[3]);
        if (UnityEngine.Random.Range(0, 5) > 2)
        {
            Tammytam.ins.Say("Hello, \n" + name.Split(' ')[3] + "!");
        }
        else
        {
            Tammytam.ins.Say("Let's read \n" + name.Split(' ')[3] + "!");
        }
    }

    void LoadNo()
    {

    }
}
