using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class TimeUsageCounter : MonoBehaviour
{

   public static TimeUsageCounter ins;

   //public int timeInSecondsUsageGiven;
   [SerializeField]
   int timeInSecondsUsage;
  
   //DataService ds;
   SubscriptionTimeModel model = null;

   void Start()
   {
     
      if (ins != null)
      {
         Destroy (gameObject);
      }
      else
      {
         ins = this;
            // db
            DataService.Close();
            DataService.Open("system/subscription.db");
         //ds = new DataService ("system/subscription.db");
         model = DataService._connection.Table<SubscriptionTimeModel> ().Where (x => x.Id == 1).FirstOrDefault ();
            DataService.Close();
      }



      timeInSecondsUsage = model.Timer; //PlayerPrefs.GetInt ("TimeUsage");
      
      print ("Time usage left: " + (((double) timeInSecondsUsage / 60) / 60) + "hrs");
      StartCoroutine (IECountTimeUsage ());
   }


   public bool IsTimeOver()
   {
      if (timeInSecondsUsage <= 1) return true;
      else return false;
   }

   IEnumerator IECountTimeUsage()
   {
      while (timeInSecondsUsage > 1)
      {
         yield return new WaitForSeconds (1f);
         timeInSecondsUsage -= 1;
      }
      print ("Subscription has ended.");
      //MessageBox.ins.ShowOk("Subscription has ended.", MessageBox.MsgIcon.msgInformation, new UnityAction(CloseApp));
   }

   public int GetTime()
   {
      return timeInSecondsUsage;
   }

   public void SetTime(int newTime)
   {
      timeInSecondsUsage = newTime;
   }

   public void Save()
   {
      model.Timer = timeInSecondsUsage;
        DataService.Open("system/subscription.db");
      DataService._connection.Update (model);
        DataService.Close();
      
      //PlayerPrefs.SetInt ("TimeUsage", timeInSecondsUsage);
   }


   void CloseApp()
   {
      Application.Quit ();
   }

   void OnApplicationQuit()
   {
      Save ();
      print ("time saved");
   }
}
