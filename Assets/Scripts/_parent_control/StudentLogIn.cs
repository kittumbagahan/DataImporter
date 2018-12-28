using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentLogIn : MonoBehaviour {

    [SerializeField]
    GameObject authentication, sectionSelection;

	public void LogIn()
    {
        UserRestrictionController.ins.Restrict(1);

        // kit, gameobjects
        sectionSelection.gameObject.SetActive(true);
        authentication.gameObject.SetActive(false);
    }
}
