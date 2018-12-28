using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TeacherDeviceController{

	
   public bool SetAsTeacherDevice()
   {
        //DataService ds = new DataService ();
        DataService.Open();
      TeacherDeviceModel model = DataService._connection.Table<TeacherDeviceModel> ().Where(x=>x.Id == 1).FirstOrDefault();
      if(model == null)
      {
         TeacherDeviceModel m = new TeacherDeviceModel {
            DeviceId = SystemInfo.deviceUniqueIdentifier
         };
         DataService._connection.Insert (m);
         return true;
      }
      Debug.Log (model.ToString());
        DataService.Close();
      return false;
   }

   public bool IsTeacherDevice()
   {
        //DataService ds = new DataService ();
        DataService.Open();
      var model = DataService._connection.Table<TeacherDeviceModel> ().Where (x => x.DeviceId == SystemInfo.deviceUniqueIdentifier).FirstOrDefault ();
        DataService.Close();
        if (model != null)
      {            
         return true;
      }        
        return false;
   }
}
