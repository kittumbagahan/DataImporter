using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyAfterTheRain : BookAccuracy {

     void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        
        lstGrade = new List<string>();
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 4));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 10));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 18));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 28));

        return base.GetAccuracy();
    }

    public double GetAccuracyWord(int id)
    {

        lstGrade = new List<string>();
        lstGrade.Add(GetGrade(id, StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 12));

        return base.GetAccuracy();
    }

    public double GetAccuracyObservation(int id)
    {

        lstGrade = new List<string>();
       
        lstGrade.Add(GetGrade(id, StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 4));
        lstGrade.Add(GetGrade(id, StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 10));
        lstGrade.Add(GetGrade(id, StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 18));
        lstGrade.Add(GetGrade(id, StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 28));

        return base.GetAccuracy();
    }
}
