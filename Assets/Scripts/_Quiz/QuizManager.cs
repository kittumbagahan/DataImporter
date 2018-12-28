using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;

public class QuizManager : MonoBehaviour {

    [SerializeField]
    GameObject m_PrefabQuiz, m_PrefabQuizParent, m_QuizMaintenancePanel;

    public QuizAdmin quizAdmin;

    public enum MaintenanceState
    {
        Add, Edit
    }

    // Use this for initialization
    void Start () {

        quizAdmin = GetComponent<QuizAdmin> ();
        quizAdmin.Init ();

        PopulateQuizList ();

    }

    #region METHODS

    void PopulateQuizList()
    {
        // kit
        //Test ();

        // Table Quiz surely exist
        //DataService ds = new DataService ();
        DataService.Open();

        string strCommand = "select * from QuizModel";

        SQLiteCommand command = DataService._connection.CreateCommand (strCommand);
        List<QuizModel> m_lstQuizModel = command.ExecuteQuery<QuizModel> ();
        Debug.Log ("Count " + m_lstQuizModel.Count);

        // existing quiz
        for (int index = 0; index < m_lstQuizModel.Count; index++)
        {
            Debug.Log (string.Format ("ID: {0}, Title: {1}", m_lstQuizModel[index].ID, m_lstQuizModel[index].Title));
            CreateQuizObject (m_lstQuizModel[index]);
        }

        // create for Add Quiz
        GameObject quizObject = Instantiate (m_PrefabQuiz);
        quizObject.transform.parent = m_PrefabQuizParent.transform;

        DataService.Close();
    }


    void CreateQuizObject(QuizModel pQuizModel)
    {
        GameObject quizObject = Instantiate (m_PrefabQuiz);
        quizObject.transform.parent = m_PrefabQuizParent.transform;
        // set the model
        quizObject.GetComponent<QuizUI> ().QuizModel (pQuizModel);
    }

    void Test()
    {
        //DataService ds = new DataService ();
        //ds._connection.DropTable<QuizModel> ();

        //DataService ds = new DataService ();
        DataService.Open();
        // create object
        QuizModel quizModel = new QuizModel
        {
            ID = DateTime.Now.ToString ("YYYY") + (DataService._connection.Table<QuizModel> ().Count () + 1).ToString (),
            Title = "First Quiz",
            Subject = "English"
        };
        // insert object
        DataService._connection.Insert (quizModel);
        DataService.Close();
    }

    #endregion

}
