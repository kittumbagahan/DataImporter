using UnityEngine;
using System.Collections;

public class DeleteUser : MonoBehaviour {

     /*
      * 1. delete user save slot
      * 2. delete user activity
      * 3. deleter user activitis progress
      * 4. delete user book progress
      */

    public void Delete(int id, string user)
    {
        PlayerPrefs.DeleteKey(id.ToString());
    }
}
