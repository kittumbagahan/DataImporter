using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyFavoriteBox : BookAccuracy {
 
	void OnEnable()
    {
        total = GetAccuracy();
    }

    public override double GetAccuracy()
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;

        lstGrade = new List<string>();
        lstGrade.Add(GetGrade(StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 12));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public override double GetAccuracy(int id)
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;

        lstGrade = new List<string>();
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 12));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

     public double GetAccuracyWord(int id)
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;

        lstGrade = new List<string>();
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act1_word", Module.WORD.ToString(), 12));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyObservation(int id)
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;

        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.FAVORITE_BOX.ToString(), "favBox_Act3_spotDiff", Module.OBSERVATION.ToString(), 12));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

}
