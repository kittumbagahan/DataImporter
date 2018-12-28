using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyJoeyGoesToSchool : BookAccuracy
{

  
     void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act1", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act1", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act1", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act1", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act1", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act3", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act3", Module.OBSERVATION.ToString(), 4));
        lstGrade.Add(GetGrade(StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act3", Module.OBSERVATION.ToString(), 10));
        lstGrade.Add(GetGrade(StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act3", Module.OBSERVATION.ToString(), 18));
        lstGrade.Add(GetGrade(StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act3", Module.OBSERVATION.ToString(), 28));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyWord(int id)
    {
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act1", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act1", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act1", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act1", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act1", Module.WORD.ToString(), 12));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyObservation(int id)
    {
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act3", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act3", Module.OBSERVATION.ToString(), 4));
        lstGrade.Add(GetGrade(id, StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act3", Module.OBSERVATION.ToString(), 10));
        lstGrade.Add(GetGrade(id, StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act3", Module.OBSERVATION.ToString(), 18));
        lstGrade.Add(GetGrade(id, StoryBook.JOEY_GO_TO_SCHOOL.ToString(), "JoeyGoesToSchool_Act3", Module.OBSERVATION.ToString(), 28));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

}
