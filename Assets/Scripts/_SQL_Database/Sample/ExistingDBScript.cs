using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExistingDBScript : MonoBehaviour {

	public Text DebugText;

	// Use this for initialization
	void Start () {
        //var ds = new DataService ("existing.db");
        DataService.Open("existing.db");
		//ds.CreateDB ();
		var people = DataService.GetBooks ();
		//ToConsole (people);

		//people = ds.GetPersonsNamedRoberto ();
		//ToConsole("Searching for Roberto ...");
		//ToConsole (people);

		DataService.CreatePerson ();
		ToConsole("New person has been created");
		var p = DataService.GetJohnny ();
		ToConsole(p.ToString());
        DataService.Close();
	}
	
	private void ToConsole(IEnumerable<Person> people){
		foreach (var person in people) {
			ToConsole(person.ToString());
		}
	}

	private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}

}
