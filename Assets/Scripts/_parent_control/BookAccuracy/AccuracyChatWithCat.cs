using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyChatWithCat : BookAccuracy {

  
    void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_2", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_2", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_2", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_2", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_2", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_1", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_1", Module.OBSERVATION.ToString(), 1));
        lstGrade.Add(GetGrade(StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_1", Module.OBSERVATION.ToString(), 2));
        lstGrade.Add(GetGrade(StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_1", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_1", Module.OBSERVATION.ToString(), 4));

      
       
        return base.GetAccuracy();
    }

    public double GetAccuracyWord(int id)
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_2", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_2", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_2", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_2", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_2", Module.WORD.ToString(), 12));

        return base.GetAccuracy();
    }

    public double GetAccuracyObservation(int id)
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_1", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_1", Module.OBSERVATION.ToString(), 1));
        lstGrade.Add(GetGrade(id, StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_1", Module.OBSERVATION.ToString(), 2));
        lstGrade.Add(GetGrade(id, StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_1", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.CHAT_WITH_MY_CAT.ToString(), "chatWithCat_Act_1", Module.OBSERVATION.ToString(), 4));

        return base.GetAccuracy();
    }
}
