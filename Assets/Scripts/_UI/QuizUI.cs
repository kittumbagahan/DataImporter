using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUI: MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI txtTitle, txtSubject;

    [SerializeField]
    Button btnQuiz;

    public QuizManager.MaintenanceState state;

    public void Start ()
    {
        btnQuiz.onClick.AddListener (Click);
    }

    #region METHODS

    public void QuizModel(QuizModel pQuizModel)
    {
        txtTitle.text = pQuizModel.Title;
        txtSubject.text = pQuizModel.Subject;
        txtTitle.color = txtSubject.color = Color.white;

        btnQuiz.GetComponentInChildren<Text> ().text = "Edit";
        state = QuizManager.MaintenanceState.Edit;
    }

    void Click()
    {
        if(state == QuizManager.MaintenanceState.Add)
        {
            Debug.Log ("Clicked ADD!");
        }
        else
        {
            Debug.Log ("Clicked EDIT!");
        }
    }

    #endregion
}
