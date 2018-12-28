using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyTinaAndJun : BookAccuracy
{

 
    void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
      
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act1", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 9));


        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), -1));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 7));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 11));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 15));

      
        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyWord(int id)
    {

        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act1", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 9));


        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyObservation(int id)
    {

        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), -1));
        lstGrade.Add(GetGrade(id, StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 7));
        lstGrade.Add(GetGrade(id, StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 11));
        lstGrade.Add(GetGrade(id, StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 15));


        SetList(lstGrade);
        return base.GetAccuracy();
    }

}
