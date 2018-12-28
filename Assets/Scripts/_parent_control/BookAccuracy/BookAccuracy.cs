using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAccuracy : MonoBehaviour
{

    public List<string> lstGrade;

    public double total;
    public int max;

    public virtual double GetAccuracy()
    {
        double totalScore = 0;
        for (int i = 0; i < lstGrade.Count; i++)
        {
            if (lstGrade[i].Equals("A++"))
            {
                totalScore += 100;
            }
            else if (lstGrade[i].Equals("A"))
            {
                totalScore += 95;
            }
            else if (lstGrade[i].Equals("B+"))
            {
                totalScore += 90;
            }
            else if (lstGrade[i].Equals("B"))
            {
                totalScore += 85;
            }
            else if (lstGrade[i].Equals("C+"))
            {
                totalScore += 80;
            }
            else if (lstGrade[i].Equals("C"))
            {
                totalScore += 75;
            }
            else if (lstGrade[i].Equals("D+"))
            {
                totalScore += 70;
            }
            else if (lstGrade[i].Equals("D"))
            {
                totalScore += 65;
            }
            else if (lstGrade[i].Equals("E+"))
            {
                totalScore += 60;
            }
            else if (lstGrade[i].Equals("E"))
            {
                totalScore += 55;
            }
            else if (lstGrade[i].Equals("F"))
            {
                totalScore += 50;
            }
            else
            {
                totalScore += 100;
            }
        }
        max = lstGrade.Count * 100;
        return totalScore;
    }
    public virtual double GetAccuracy(int id)
    {
       
        return 1;
    }
    public virtual double GetAccuracy(int id, string module)
    {

        return 1;
    }
    public void SetList(List<string> lst)
    {
        lstGrade = lst;
    }
    public string Get(string s)
    {
        if (s.Equals(""))
            return "0";
        return s;
    }
    public string GetGrade(string bookDesc, string activityDesc, string module, int set)
    {
        //DataService ds = new DataService();
        DataService.Open();
        BookModel bm = DataService._connection.Table<BookModel>().Where(x => x.Description == bookDesc).FirstOrDefault();

        ActivityModel am = DataService._connection.Table<ActivityModel>().Where(x => x.BookId == bm.Id &&
        x.Description == activityDesc && x.Module == module && x.Set == set).FirstOrDefault();
        //In this case this activity is have not yet taken by the user. ActivityModel table is created when user played the activity for the first time.
        if (am != null)
        {
            StudentActivityModel sam = DataService._connection.Table<StudentActivityModel>().Where(x => x.SectionId == StoryBookSaveManager.ins.activeSection_id &&
             x.StudentId == UserAccountManager.ins.SelectedSlot.UserId && x.ActivityId == am.Id).FirstOrDefault();

            DataService.Close();
            return sam == null ? "" : sam.Grade;
        }
        else
        {
            DataService.Close();
            return "";
        }


        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "0")));        
    }
    public string GetGrade(int studentId, string bookDesc, string activityDesc, string module, int set)
    {
        //DataService ds = new DataService();
        DataService.Open();
        BookModel bm = DataService._connection.Table<BookModel>().Where(x => x.Description == bookDesc).FirstOrDefault();

        ActivityModel am = DataService._connection.Table<ActivityModel>().Where(x => x.BookId == bm.Id &&
        x.Description == activityDesc && x.Module == module && x.Set == set).FirstOrDefault();
        //In this case this activity is have not yet taken by the user. ActivityModel table is created when user played the activity for the first time.
        if (am != null)
        {
            StudentActivityModel sam = DataService._connection.Table<StudentActivityModel>().Where(x => x.SectionId == StoryBookSaveManager.ins.activeSection_id &&
             x.StudentId == studentId && x.ActivityId == am.Id).FirstOrDefault();

            DataService.Close();
            return sam == null ? "" : sam.Grade;
        }
        else
        {
            DataService.Close();
            return "";
        }


        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "0")));        
    }
}
