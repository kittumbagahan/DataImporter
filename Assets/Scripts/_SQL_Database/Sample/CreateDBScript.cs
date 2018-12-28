using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class CreateDBScript : MonoBehaviour
{

   public Text DebugText;

   // Use this for initialization
   void Start()
   {

      StartSync ();


   }

   private void StartSync()
   {
        //set the active database for the app for the firstime
        //if ("".Equals(PlayerPrefs.GetString("activeDatabase")))
        //{
        //    PlayerPrefs.SetString("activeDatabase", "tempDatabase.db");
        //}

        //var ds = new DataService ();
        DataService.Open();
      DataService.CreateDB ();
      var books = DataService.GetBooks ();
      var acts = DataService.GetActivities ();
      var sections = DataService.GetSections ();
      var st = DataService.GetStudents ();
      var studentActivities = DataService.GetStudentActivities ();
      var studentBooks = DataService.GetStudentBooks ();

        DataService.Close();

      DatabaseController dc = new DatabaseController ();
      dc.CreateDB ("subscription.db");
      //var ds2 = new DataService ("system/subscription.db");

        DataService.Open("system/subscription.db");

        DataService.CreateSubscriptionDB ();
      var subs = DataService.GetSubscription ();
      ToConsole (subs);

        DataService.Close();
   }

   private void ToConsole(IEnumerable<SubscriptionTimeModel> model)
   {
      foreach (var person in model)
      {

         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }

   private void ToConsole(IEnumerable<BookModel> model)
   {
      foreach (var person in model)
      {

         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }
   private void ToConsole(IEnumerable<ActivityModel> model)
   {
      foreach (var person in model)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }
   private void ToConsole(IEnumerable<SectionModel> model)
   {
      foreach (var person in model)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }

   private void ToConsole(IEnumerable<StudentModel> model)
   {
      foreach (var person in model)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }

   private void ToConsole(IEnumerable<StudentActivityModel> model)
   {
      foreach (var person in model)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }
   private void ToConsole(IEnumerable<StudentBookModel> model)
   {
      foreach (var person in model)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }

   private void ToConsole(string msg)
   {
      DebugText.text += System.Environment.NewLine + msg;
      Debug.Log (msg);
   }
}
