using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyYummyShapes : BookAccuracy
{

    void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_1", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_1", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_1", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_1", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_1", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_2", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_2", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_3", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_3", Module.OBSERVATION.ToString(), 4));
        lstGrade.Add(GetGrade(StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_3", Module.OBSERVATION.ToString(), 8));

        
        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyWord(int id)
    {
      
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_1", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_1", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_1", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(id, StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_1", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(id, StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_1", Module.WORD.ToString(), 12));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

    public double GetAccuracyObservation(int id)
    {
      
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(id, StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_2", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_2", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(id, StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_3", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(id, StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_3", Module.OBSERVATION.ToString(), 4));
        lstGrade.Add(GetGrade(id, StoryBook.YUMMY_SHAPES.ToString(), "yummyShapes_Act_3", Module.OBSERVATION.ToString(), 8));


        SetList(lstGrade);
        return base.GetAccuracy();
    }
}
