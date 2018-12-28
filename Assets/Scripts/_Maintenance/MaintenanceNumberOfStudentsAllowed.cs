using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class MaintenanceNumberOfStudentsAllowed : MonoBehaviour
{

   [SerializeField]
   InputField txtNumOfStudentsAllowed;
   int maxStudentAllowed;

   NumberOfStudentsModel model = null;
   void OnEnable()
   {
        //DataService ds = new DataService ();
        DataService.Open();
        model = DataService._connection.Table<NumberOfStudentsModel> ().Where (x => x.Id == 1).FirstOrDefault ();
        maxStudentAllowed = model.MaxStudent;
        txtNumOfStudentsAllowed.text = maxStudentAllowed.ToString ();
        DataService.Close();
   }

   public void UpdateNumberOfAllowedStudents()
   {
      try
      {
         int input = int.Parse (txtNumOfStudentsAllowed.text);
         if (input == maxStudentAllowed)
         {
            MessageBox.ins.ShowOk ("Enter new number!", MessageBox.MsgIcon.msgError, null);
         }
         else if (input < maxStudentAllowed)
         {
            MessageBox.ins.ShowOk ("Minimum number of student is 10.", MessageBox.MsgIcon.msgError, null);
         }
         else
         {
            MessageBox.ins.ShowQuestion ("Are you sure you want to make changes?", MessageBox.MsgIcon.msgWarning, new UnityAction (Yes), null);
         }
      }
      catch (Exception ex)
      {
         MessageBox.ins.ShowOk (ex.ToString (), MessageBox.MsgIcon.msgError, null);
      }
   }

   void Yes()
   {
        //DataService ds = new DataService ();
        DataService.Open();
      maxStudentAllowed = int.Parse (txtNumOfStudentsAllowed.text);
      model.MaxStudent = maxStudentAllowed;
      DataService._connection.Update (model);
      //PlayerPrefs.SetInt ("maxNumberOfStudentsAllowed", maxStudentAllowed);
      MessageBox.ins.ShowOk ("Number of students per section updated!", MessageBox.MsgIcon.msgInformation, null);
        DataService.Close();
   }
}
