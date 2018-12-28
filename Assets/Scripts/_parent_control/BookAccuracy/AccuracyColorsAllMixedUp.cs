using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyColorsAllMixedUp : BookAccuracy {

   
     void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 1));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 2));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 4));

      
        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyWord(int id)
    {
      
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 12));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyObservation(int id)
    {
     
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 1));
        lstGrade.Add(GetGrade(id, StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 2));
        lstGrade.Add(GetGrade(id, StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 4));


        SetList(lstGrade);
        return base.GetAccuracy();
    }
}
