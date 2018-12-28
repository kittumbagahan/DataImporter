using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracySoundsFantastic : BookAccuracy
{

    void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {

        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act4", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act4", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act4", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act4", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act4", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act3", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act6", Module.OBSERVATION.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act7", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act8", Module.OBSERVATION.ToString(), -1));
        lstGrade.Add(GetGrade(StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act8", Module.OBSERVATION.ToString(), 2));


        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyWord(int id)
    {

        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act4", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act4", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act4", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act4", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act4", Module.WORD.ToString(), 12));


        SetList(lstGrade);
        return base.GetAccuracy();
    }
    public double GetAccuracyObservation(int id)
    {

        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act3", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act6", Module.OBSERVATION.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act7", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act8", Module.OBSERVATION.ToString(), -1));
        lstGrade.Add(GetGrade(id, StoryBook.SOUNDS_FANTASTIC.ToString(), "SoundsFantastic_Act8", Module.OBSERVATION.ToString(), 2));


        SetList(lstGrade);
        return base.GetAccuracy();
    }

}
