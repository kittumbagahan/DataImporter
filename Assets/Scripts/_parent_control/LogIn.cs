using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;
public class LogIn : MonoBehaviour {

    [SerializeField]
    GameObject panel_input;
    [SerializeField]
	Button btnInput_OK;// btnSave1, btnSave2, btnSave3;

	[SerializeField]
	Button[] btnSave;

    int selectedID;

    void Start()
    {
        try {
            if (StoryBookSaveManager.ins.activeUser != "")
            {
                gameObject.SetActive(false);
            }
        }catch(Exception ex)
        {
            gameObject.SetActive(true);
        }

        
        Init();
    }

    void Init()
    {
		for(int i=0; i<btnSave.Length; i++)
		{
			TextMeshProUGUI txt = btnSave[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            //txt.text = PlayerPrefs.GetString(i.ToString()); //8/29/2018
            txt.text = PlayerPrefs.GetString("student_id" + i.ToString());
            if (txt.text.Equals("")|| txt.text.Equals("0"))
				txt.text = "Username";
		}
    }

    public void ClickSlot(TextMeshProUGUI txt)
    {
        if (txt.text == "Username"){
            panel_input.SetActive(true);
        }
        else {
            Load(txt.text);
        }
    }

    public void SelectID(int s) {
        selectedID = s;
        StoryBookSaveManager.ins.activeUser_id = selectedID;
        
    }

    void Load(string oldUsername)
    {
        gameObject.SetActive(false);
        StoryBookSaveManager.ins.activeUser = oldUsername;
       
        if (UnityEngine.Random.Range(0, 5) > 2)
        {
            Tammytam.ins.Say("Hello, \n" + oldUsername.Split(' ')[3] + "!");
        }
        else
        {
            Tammytam.ins.Say("Let's read \n" + oldUsername.Split(' ')[3] + "!");
        }

        
    }

    public void SaveNewUser(Text newUser)
    {
        
        //PlayerPrefs.SetString(selectedID, newUser.text); //8/29/2018
        PlayerPrefs.SetString("section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + selectedID, newUser.text);
        panel_input.SetActive(false);
        Load(newUser.text);
    }
}
