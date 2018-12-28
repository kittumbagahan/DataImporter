using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeacherSetButton : MonoBehaviour {

   TeacherDeviceController controller;
   [SerializeField]
   GameObject btnServer;
	// Use this for initialization
	void Start () {
      controller = new TeacherDeviceController ();
      
      if (controller.IsTeacherDevice ())
      {
         btnServer.gameObject.SetActive (true);
         gameObject.SetActive (false);
      }
      else
      {
         btnServer.gameObject.SetActive (false);
      }
   }

   public void Set()
   {
      if (controller.IsTeacherDevice ())
      {
         MessageBox.ins.ShowOk ("Device is set for teacher.", MessageBox.MsgIcon.msgInformation, null);
         btnServer.gameObject.SetActive (true);
         gameObject.SetActive (false);
      }
      else
      {
         if (controller.SetAsTeacherDevice())
         {
            MessageBox.ins.ShowOk ("Device is set for teacher. Success!", MessageBox.MsgIcon.msgInformation, null);
            btnServer.gameObject.SetActive (true);
            gameObject.SetActive (false);
         }
         else
         {
            MessageBox.ins.ShowOk ("Not a teacher device", MessageBox.MsgIcon.msgError, null);
         }
      }

   }
	
	
}
