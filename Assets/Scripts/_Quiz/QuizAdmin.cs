using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SQLite4Unity3d;

public class QuizAdmin : MonoBehaviour {

    //DataService dataService;

    QuizModel quizModel;
    QuizItemModel quizItemModel;
    QuizItemChoicesModel quizItemChoicesModel;

    [SerializeField]
    GameObject quizListContent;


    // privates
    private QuizManager m_quizManager;

    // Use this for initialization
    void Start () {

    }

    #region DB

    public void Init()
    {
        //dataService = new DataService ();

        // check models
        CheckQuizModel ();
        CheckQuizItemModel ();
        CheckQuizItemChoicesModel ();
        CheckSubjectModel ();
    }

    void CheckQuizModel()
    {
        try
        {
            DataService.Open();
            DataService._connection.Table<QuizModel> ().Count();
            DataService.Close();
            Debug.Log ("Quiz Model Exist");
        }
        catch(SQLiteException ex)
        {
            DataService.Open();
            DataService._connection.CreateTable<QuizModel> ();
            DataService.Close();
        }
    }

    void CheckQuizItemModel()
    {
        try
        {
            DataService.Open();
            DataService._connection.Table<QuizItemModel> ().Count ();
            DataService.Close();
            Debug.Log ("Quiz Item Model Exist");
        }
        catch (SQLiteException ex)
        {
            DataService.Open();
            DataService._connection.CreateTable<QuizItemModel> ();
            DataService.Close();
        }
    }

    void CheckQuizItemChoicesModel ()
    {
        try
        {
            DataService.Open();
            DataService._connection.Table<QuizItemChoicesModel> ().Count ();
            DataService.Close();
            Debug.Log ("Quiz Item Choices Model Exist");
        }
        catch (SQLiteException ex)
        {
            DataService.Open();
            DataService._connection.CreateTable<QuizItemChoicesModel> ();
            DataService.Close();
        }
    }

    void CheckSubjectModel()
    {
        try
        {
            DataService.Open();
            DataService._connection.Table<SubjectModel> ().Count ();
            DataService.Close();
        }
        catch(SQLiteException ex)
        {
            DataService.Open();
            DataService._connection.CreateTable<SubjectModel> ();
            SubjectModel subjectModel1 = new SubjectModel
            {
                Name = "MATH"
            };

            SubjectModel subjectModel2 = new SubjectModel
            {
                Name = "ENGLISH"
            };

            DataService._connection.Insert (subjectModel1);
            DataService._connection.Insert (subjectModel2);

            DataService.Close();
        }
    }
    #endregion
}
