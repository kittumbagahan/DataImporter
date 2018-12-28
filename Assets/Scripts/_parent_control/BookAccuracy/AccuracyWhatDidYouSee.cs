using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyWhatDidYouSee : BookAccuracy
{

 
     void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct7", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct7", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct7", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct7", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct7", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.WHAT_DID_YOU_SEE.ToString(), "whatDidYaSee_act2", Module.OBSERVATION.ToString(), -1));
        lstGrade.Add(GetGrade(StoryBook.WHAT_DID_YOU_SEE.ToString(), "whatDidYaSee_act2", Module.OBSERVATION.ToString(), 2));
        lstGrade.Add(GetGrade(StoryBook.WHAT_DID_YOU_SEE.ToString(), "whatDidYaSee_act2", Module.OBSERVATION.ToString(), 5));
        lstGrade.Add(GetGrade(StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct5", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct8", Module.OBSERVATION.ToString(), 0));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyWord(int id)
    {
      
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct7", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct7", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct7", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct7", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct7", Module.WORD.ToString(), 12));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyObservation(int id)
    {

        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.WHAT_DID_YOU_SEE.ToString(), "whatDidYaSee_act2", Module.OBSERVATION.ToString(), -1));
        lstGrade.Add(GetGrade(id, StoryBook.WHAT_DID_YOU_SEE.ToString(), "whatDidYaSee_act2", Module.OBSERVATION.ToString(), 2));
        lstGrade.Add(GetGrade(id, StoryBook.WHAT_DID_YOU_SEE.ToString(), "whatDidYaSee_act2", Module.OBSERVATION.ToString(), 5));
        lstGrade.Add(GetGrade(id, StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct5", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.WHAT_DID_YOU_SEE.ToString(), "WhatDidYouSeeAct8", Module.OBSERVATION.ToString(), 0));

        SetList(lstGrade);
        return base.GetAccuracy();
    }
}
