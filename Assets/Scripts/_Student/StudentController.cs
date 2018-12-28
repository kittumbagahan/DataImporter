using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class StudentController : MonoBehaviour
{
   public static StudentController ins;
   [SerializeField]
   GameObject panelCreateStudentInput;
   [SerializeField]
   GameObject panelEditStudentInput;
   [SerializeField]
   GameObject btnStudentContainer;
   [SerializeField]
   GameObject btnStudentPrefab;
   [SerializeField]
   Text txtGivenName;
   [SerializeField]
   Text txtSurname;
   [SerializeField]
   Text txtMiddleName;
   [SerializeField]
   Text txtNickName;
   [SerializeField]
   int currentMaxStudent;
   [SerializeField]
   int maxStudentAllowed;
   [SerializeField]
   GameObject btnEdit;
   public bool editMode = false;

   void Start()
   {
      if (ins != null)
      {
         Destroy (gameObject);
      }
      else
      {
         ins = this;
      }
      if (StoryBookSaveManager.ins.activeUser != "")
      {
         gameObject.SetActive (false);
      }
      //if (PlayerPrefs.GetInt("maxNumberOfStudentsAllowed") == 0)
      //{
      //    PlayerPrefs.SetInt("maxNumberOfStudentsAllowed", 10);
      //}
      //LoadStudents();
      // for (int i=0; i<10; i++)
      // {
      //     print(PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
      //     "student_id" + i));
      // }

   }
   public void ShowStudentsSQL(string letter)
   {
     
      currentMaxStudent = 0;
    
      DataService.Open ();
      maxStudentAllowed = DataService._connection.Table<NumberOfStudentsModel> ().Where (x => x.Id == 1).FirstOrDefault ().MaxStudent;
      string query = "select * from StudentModel where SectionId='" + StoryBookSaveManager.ins.activeSection_id  + "' and Givenname LIKE '" + letter +"%'";
      //var students = DataService._connection.Table<StudentModel> ().Where (x => x.SectionId == StoryBookSaveManager.ins.activeSection_id);
      var students = DataService._connection.Query<StudentModel>(query);
      for (int i = 0; i < btnStudentContainer.transform.childCount; i++)
      {
         Destroy (btnStudentContainer.transform.GetChild (i).gameObject);
      }

      foreach (var student in students)
      {
         GameObject _obj = Instantiate (btnStudentPrefab);
         Student _student = _obj.GetComponent<Student> ();
         _student.UID = student.DeviceId;
         _student.id = student.Id;
         _student.name = student.Givenname + " " + student.Middlename + " " + student.Lastname + " " + student.Nickname;
         _obj.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text = _student.name + _student.id;
         _obj.transform.SetParent (btnStudentContainer.transform);
         currentMaxStudent++;
      }

      DataService.Close ();
   }
   public void LoadStudentsSQL()
   {
      //maxStudentAllowed = PlayerPrefs.GetInt("maxNumberOfStudentsAllowed");
      currentMaxStudent = 0;
      //DataService ds = new DataService();
      DataService.Open ();
      maxStudentAllowed = DataService._connection.Table<NumberOfStudentsModel> ().Where (x => x.Id == 1).FirstOrDefault ().MaxStudent;
      //load all students from their section in all devices


      //var students = UserRestrictionController.ins.restriction == 0? 
      //    ds._connection.Table<StudentModel>().Where(x => x.SectionId == StoryBookSaveManager.ins.activeSection_id) : 
      //    ds._connection.Table<StudentModel>().Where(x => x.DeviceId == SystemInfo.deviceUniqueIdentifier && x.SectionId == StoryBookSaveManager.ins.activeSection_id);
      var students = DataService._connection.Table<StudentModel> ().Where (x => x.SectionId == StoryBookSaveManager.ins.activeSection_id);

      for (int i = 0; i < btnStudentContainer.transform.childCount; i++)
      {
         Destroy (btnStudentContainer.transform.GetChild (i).gameObject);
      }

      foreach (var student in students)
      {
         GameObject _obj = Instantiate (btnStudentPrefab);
         Student _student = _obj.GetComponent<Student> ();
         _student.UID = student.DeviceId;
         _student.id = student.Id;
         _student.name = student.Givenname + " " + student.Middlename + " " + student.Lastname + " " + student.Nickname;
         _obj.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text = _student.name + _student.id;
         _obj.transform.SetParent (btnStudentContainer.transform);
         currentMaxStudent++;
      }

      if (btnStudentContainer.transform.childCount == 0)
      {
         btnEdit.gameObject.SetActive (false);
      }
      else
      {
         if (UserRestrictionController.ins.restriction == 0)
         {
            btnEdit.gameObject.SetActive (true);
         }

      }
      DataService.Close ();
   }
   public void CreateNewStudent()
   {
      if ("".Equals (txtGivenName.text) || "".Equals (txtMiddleName.text) || "".Equals (txtSurname.text) || "".Equals (txtNickName.text))
      {
         MessageBox.ins.ShowOk ("All fields are required.", MessageBox.MsgIcon.msgError, null);
      }
      else
      {
         if (currentMaxStudent < maxStudentAllowed)
         {
            //DataService ds = new DataService();
            DataService.Open ();
            string studentName = txtGivenName.text + " " + txtMiddleName.text + " " + txtSurname.text + " " + txtNickName.text;

            //check duplicate entry in all devices and sections
            //assuming there can be no same names in a school
            StudentModel sm = DataService._connection.Table<StudentModel> ().Where (
               a => a.Lastname == txtSurname.text &&
               a.Middlename == txtMiddleName.text &&
               a.Givenname == txtGivenName.text &&
               a.Nickname == txtNickName.text
               ).FirstOrDefault ();
            if (sm == null)
            {
               StudentModel model = new StudentModel
               {
                  SectionId = StoryBookSaveManager.ins.activeSection_id,
                  Givenname = txtGivenName.text,
                  Middlename = txtMiddleName.text,
                  Lastname = txtSurname.text,
                  Nickname = txtNickName.text
               };
               DataService._connection.Insert (model);
               //ds._connection.Execute ("Insert into StudentModel(SectionId, Givenname, Middlename, Lastname, Nickname)" +
               //   "Values('" + StoryBookSaveManager.ins.activeSection_id + "','" + model.Givenname + "','" + model.Middlename + "','" + model.Lastname + "','" + model.Nickname + "')");


               GameObject _obj = Instantiate (btnStudentPrefab);
               Student _student = _obj.GetComponent<Student> ();
               StudentModel s = DataService._connection.Table<StudentModel> ().Where (
                a => a.Lastname == txtSurname.text &&
                a.Middlename == txtMiddleName.text &&
                a.Givenname == txtGivenName.text &&
                a.Nickname == txtNickName.text
                ).FirstOrDefault ();


               _student.id = s.Id;
               _student.name = studentName;
               _obj.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text = _student.name + s.Id;
               _obj.transform.SetParent (btnStudentContainer.transform);

               panelCreateStudentInput.gameObject.SetActive (false);
               currentMaxStudent++;
               btnEdit.gameObject.SetActive (true);

            }
            else
            {
               MessageBox.ins.ShowOk ("Name already exist.", MessageBox.MsgIcon.msgError, null);
            }

            DataService.Close ();
         }
         else
         {
            MessageBox.ins.ShowOk ("Max number of students allowed already reached.", MessageBox.MsgIcon.msgError, null);
         }
      }

      //PrintSections();
   }

   public void Show()
   {
      gameObject.SetActive (true);

   }

   public void Close()
   {
      gameObject.SetActive (false);
   }

   //EDIT ------------------------------------
   public void EditStudent()
   {
      if (btnStudentContainer.transform.childCount == 0)
      {
         MessageBox.ins.ShowOk ("No student to edit.", MessageBox.MsgIcon.msgInformation,
          null);
      }
      else
      {
         editMode = true;
         MessageBox.ins.ShowOkCancel ("Select student to edit. Click cancel to return.", MessageBox.MsgIcon.msgInformation,
             EditYes, EditCancel);
      }

   }

   void EditYes()
   {
      editMode = true;
   }
   void EditCancel()
   {
      editMode = false;
      MessageBox.ins.ShowOk ("Edit student cancelled.", MessageBox.MsgIcon.msgInformation, null);
   }

   void EditClose()
   {
      editMode = false;
      EditStudentView view = panelEditStudentInput.GetComponent<EditStudentView> ();
      view.btnOK.onClick.RemoveAllListeners ();
   }

   public void Edit(Student student)
   {
      EditStudentView view = panelEditStudentInput.GetComponent<EditStudentView> ();
      view.gameObject.SetActive (true);
      view.txtGivenName.text = student.name.Split (' ')[0];
      view.txtMiddleName.text = student.name.Split (' ')[1];
      view.txtSurname.text = student.name.Split (' ')[2];
      view.txtNickname.text = student.name.Split (' ')[3];

      UpdateStudent updateStudent = new UpdateStudent (view, student);
      print ("checking update " + student.name);
      view.btnOK.onClick.AddListener (updateStudent.UpdateStudentName);
      view.btnClose.onClick.AddListener (EditClose);
   }


}

class UpdateStudent
{

   EditStudentView view;
   Student s;
   public UpdateStudent(EditStudentView view, Student s)
   {
      this.view = view;
      this.s = s;
   }
   public void UpdateStudentName()
   {
      if ("".Equals (view.txtGivenName.text) || "".Equals (view.txtMiddleName.text) || "".Equals (view.txtSurname.text) || "".Equals (view.txtNickname.text))
      {
         MessageBox.ins.ShowOk ("All fields are required.", MessageBox.MsgIcon.msgError, null);
      }

      else if (view.txtGivenName.text.Equals (s.name.Split (' ')[0]) && view.txtMiddleName.text.Equals (s.name.Split (' ')[1]) && view.txtSurname.text.Equals (s.name.Split (' ')[2])
          && view.txtNickname.text.Equals (s.name.Split (' ')[3]))
      {
         //nothing to update just say updated!
         MessageBox.ins.ShowOk ("Student name updated!", MessageBox.MsgIcon.msgInformation, null);
         StudentController.ins.editMode = false;
         view.btnOK.onClick.RemoveAllListeners ();
      }
      else
      {
         //DataService ds = new DataService();
         DataService.Open ();
         StudentModel model = new StudentModel
         {
            Id = s.id,
            SectionId = StoryBookSaveManager.ins.activeSection_id,
            Givenname = view.txtGivenName.text,
            Middlename = view.txtMiddleName.text,
            Lastname = view.txtSurname.text,
            Nickname = view.txtNickname.text
         };

         DataService._connection.Execute ("Update StudentModel set Givenname='" + model.Givenname + "', Middlename='" + model.Middlename + "', " +
            "Lastname='" + model.Lastname + "', Nickname='" + model.Nickname + "' where Id='" + model.Id + "' and SectionId='" + model.SectionId + "'");

         StudentController.ins.LoadStudentsSQL ();
         MessageBox.ins.ShowOk ("Student name updated!", MessageBox.MsgIcon.msgInformation, null);
         StudentController.ins.editMode = false;
         view.btnOK.onClick.RemoveAllListeners ();

         DataService.Close ();
      }
   }
}
