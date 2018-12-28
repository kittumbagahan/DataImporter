using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
/*
 key, value
 * slotID, username
 */

public class UserAccountManager : MonoBehaviour
{
    public static UserAccountManager ins;
    [SerializeField]
    UserBooksManager userBooksManager;
    [SerializeField]
    private GameObject panelStart;
    [SerializeField]
    private GameObject panelSaves;
    [SerializeField]
    private GameObject panelInput;
    [SerializeField]
    private GameObject objSaveOptions;
    public Text txtUsernameInput;
    [SerializeField]
    UserAccount selectedSlot;
    [SerializeField]
    List<UserAccount> lstAccount;
    public enum ECommand { newgame, continue_ };
    [SerializeField]
    ECommand cmd = new ECommand();
    [SerializeField]
    bool overwriteASaveSlot;
    [SerializeField]
    Button loadButton;
    [SerializeField]
    Transform saves_group;


    [SerializeField]
    GameObject btnUserSavePrefab;
    [SerializeField]
    GameObject btnStudentContainer;
    int currentMaxStudent;
    int maxStudentAllowed;

    string prevUsername;

    public string PrevUsername { set { prevUsername = value; } get { return prevUsername; } }
    public ECommand Command { get { return cmd; } set { cmd = value; } }
    public GameObject PanelSaves { get { return panelSaves; } }
    public GameObject slotHighlight, pnlSaveSlots, pnlStartOptions;
    public GameObject PanelInput { get { return panelInput; } }
    public Button LoadButton { set { loadButton = value; } get { return loadButton; } }
    public UserAccount SelectedSlot { get { return selectedSlot; } set { selectedSlot = value; } }
    public List<UserAccount> UserAccounts {
        get { return lstAccount; }
    }
    public GameObject SaveOptions { get { return objSaveOptions; } }
    public GameObject PanelStart { get { return panelStart; } }
    public UserBooksManager UserBooksManager{ get{ return userBooksManager; }}
    void Start()
    {
        ins = this;
        if (Singleton.userActive)
        {
            gameObject.SetActive(false);
        }
        panelSaves.SetActive(true);
        LoadStudents();
    }


    public void LoadStudents()
    {
        int n = 0;
        maxStudentAllowed = PlayerPrefs.GetInt("maxNumberOfStudentsAllowed");
        //DataService ds = new DataService();
        DataService.Open();
        var students = DataService._connection.Table<StudentModel>().Where(x => x.SectionId == StoryBookSaveManager.ins.activeSection_id);
      

        foreach(StudentModel student in students)
        {
            GameObject _obj = Instantiate(btnUserSavePrefab);
            UserAccount _student = _obj.GetComponent<UserAccount>();
            _student.UserId = student.Id;
            _student.Username = student.Givenname + " " + student.Middlename + " " + student.Lastname + " " + student.Nickname; 
            _obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _student.Username;
            _obj.transform.SetParent(btnStudentContainer.transform);
            n++;
        }

        currentMaxStudent = n;
        DataService.Close();
    }

    #region old

    public void Overwrite(bool val)
    {
        UserParentalManager.ins.PlayClickClip();
        overwriteASaveSlot = val;
    }
    public void SelectSlot(GameObject slot)
    {
        //  selectedSlot = slot;
    }
    public void Delete()
    {
        UserParentalManager.ins.PlayClickClip();
        MessageBox.ins.ShowQuestion("Delete user?", MessageBox.MsgIcon.msgInformation, new UnityAction(DeleteSaveYes), new UnityAction(DeleteSaveNo));
    }

    void DeleteSaveNo()
    {
        UserParentalManager.ins.PlayClickClip();
    }
    void DeleteSaveYes()
    {
        string user = selectedSlot.Username;
        int saveId = selectedSlot.UserId;
      
        PlayerPrefs.DeleteKey("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + selectedSlot.UserId);
        print("DElete ?" + PlayerPrefs.GetString("student_id" + saveId.ToString()));

        UserParentalManager.ins.PlayClickClip();

        DeleteActivity("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + selectedSlot.UserId);
        DeleteBooksUsage("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + selectedSlot.UserId);
        DeleteActivityUsage("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + selectedSlot.UserId);

        DeleteFinished();
    }

    public void DeleteActivity(string user)
    {
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act2_coloring" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act4" + Module.PUZZLE.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act5" + Module.PUZZLE.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favbox6_NEW" + Module.PUZZLE.ToString() + "3");

        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_3" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_4" + Module.PUZZLE.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_5" + Module.PUZZLE.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_6" + Module.PUZZLE.ToString() + "3");

        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "4");

        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act2" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act3" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act3" + Module.PUZZLE.ToString() + "4");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act4" + Module.PUZZLE.ToString() + "0");

        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "4");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "10");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "18");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "28");

        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_2" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_3" + Module.PUZZLE.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_4" + Module.PUZZLE.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_5" + Module.PUZZLE.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_6" + Module.PUZZLE.ToString() + "4");

        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "4");

        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act1" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct4" + Module.PUZZLE.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct6" + Module.PUZZLE.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct3" + Module.PUZZLE.ToString() + "3");

        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION.ToString() + "-1");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION.ToString() + "5");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct5" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct8" + Module.OBSERVATION.ToString() + "0");

        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act4" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act6" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act5" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act5" + Module.PUZZLE.ToString() + "3");

        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act2" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act4" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act5" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act6" + Module.PUZZLE.ToString() + "0");

        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "4");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "10");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "18");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "28");

        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act1" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act2" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act5" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act5" + Module.PUZZLE.ToString() + "4");

        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act3" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act6" + Module.OBSERVATION.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act7" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION.ToString() + "-1");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION.ToString() + "2");

        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act1" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "9");

        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act4" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act5" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act6" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act7" + Module.PUZZLE.ToString() + "0"); 

        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "-1");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "7");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "11");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "15");

        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_4" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_5" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_6" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_7" + Module.PUZZLE.ToString() + "0"); //warning

        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_2" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_2" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION.ToString() + "4");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION.ToString() + "8");
      
    }

    public void DeleteBooksUsage(string user)
    {
        // string key = "read" + StoryBookSaveManager.instance.User + StoryBookSaveManager.instance.selectedBook;
        //print("IU");
        foreach (StoryBook val in Enum.GetValues(typeof(StoryBook)))
        {
            PlayerPrefs.DeleteKey("read" + user + val.ToString());
            PlayerPrefs.DeleteKey("readItTome" + user + val.ToString());
            PlayerPrefs.DeleteKey("auto" + user + val.ToString());
        }
    }
    public void DeleteActivityUsage(string user)
    { 
        //string key = btnAct.Mode + StoryBookSaveManager.instance.User + StoryBookSaveManager.instance.selectedBook;
        foreach (StoryBook val in Enum.GetValues(typeof(StoryBook)))
        {
            PlayerPrefs.DeleteKey(Module.WORD + user + val.ToString());
            PlayerPrefs.DeleteKey(Module.PUZZLE + user + val.ToString());
            PlayerPrefs.DeleteKey(Module.OBSERVATION + user + val.ToString());
        }
    }

    public void DeleteFinished()
    {
        selectedSlot.Username = "";
        selectedSlot = null;
        objSaveOptions.SetActive(false);
    }
    
    int checkDuplicate(string name)
    {
        for (int i = 0; i < lstAccount.Count; i++ )
        {
            if(lstAccount[i].Username.Equals(name, StringComparison.Ordinal))
            {
                return 1;
            }
        }

        return 0;
    }

    public void OK(GameObject inputPanel)
    {
        if (txtUsernameInput.text == "")
        {
            print("Please enter username.");
            MessageBox.ins.ShowOk("Please enter a username", MessageBox.MsgIcon.msgError, null);
            UserParentalManager.ins.PlayClickClip();
        }
        else if(checkDuplicate(txtUsernameInput.text) >= 1)
        {
            UserParentalManager.ins.PlayClickClip();
            //print("that username is already taken.");
            MessageBox.ins.ShowOk("Username already taken", MessageBox.MsgIcon.msgError, null);
        }
        else if ((txtUsernameInput.text != "" && txtUsernameInput.text.Length <= 12) || cmd == ECommand.newgame)
        {
            if (!overwriteASaveSlot)
            {
                cmd = ECommand.continue_;
                selectedSlot.GetComponent<UserAccount>().Username = txtUsernameInput.text;
                selectedSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = txtUsernameInput.text;
                inputPanel.SetActive(false);
                PlayerPrefs.SetString("student_id" + selectedSlot.GetComponent<UserAccount>().UserId.ToString(), selectedSlot.GetComponent<UserAccount>().Username);
                print("SAVED! student_id" + selectedSlot.GetComponent<UserAccount>().UserId.ToString() + selectedSlot.GetComponent<UserAccount>().Username);
            }
            else if (overwriteASaveSlot && (txtUsernameInput.text != "" && txtUsernameInput.text.Length <= 12))
            {
                cmd = ECommand.continue_;

                // PlayerPrefs.DeleteKey(prevUsername + selectedSlot.GetComponent<UserAccount>().SlotID.ToString());
                //DELETE HERE
                //DeleteSave(prevUsername, id);
                overwriteASaveSlot = false;
                selectedSlot.GetComponent<UserAccount>().Username = txtUsernameInput.text;
                selectedSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = txtUsernameInput.text;
                inputPanel.SetActive(false);
                //SAVE NEW ONE
                PlayerPrefs.SetString("student_id" + selectedSlot.GetComponent<UserAccount>().UserId.ToString(), selectedSlot.GetComponent<UserAccount>().Username);
                print("OVERWRITTEN SAVED! student_id" + selectedSlot.GetComponent<UserAccount>().UserId.ToString() + selectedSlot.GetComponent<UserAccount>().Username);
                UserParentalManager.ins.PlayClickClip();
            }
            else
            {
                UserParentalManager.ins.PlayClickClip();
                print("Please enter username.");
                MessageBox.ins.ShowOk("Please enter a username", MessageBox.MsgIcon.msgError, null);
            }
        }
        
     
    }

    void OnEnable()
    {
        OpenUserAccounts();
    }
    public void OpenUserAccounts()
    {
        panelSaves.SetActive(true);
        panelStart.SetActive(false);
        panelInput.SetActive(false);
    }

    public void EmptyUser()
    {
        //StoryBookSaveManager.instance.user = "";
    }


	int savesGroupPosN = 0;
	IEnumerator co;
	//Vector2 _savesGroupPosTemp = new Vector2(0,0);
	public void ShowNextSaves()
	{
		if(savesGroupPosN < 2)
		{
			savesGroupPosN++;
			ShowSavesN(savesGroupPosN);
		}
	}

	public void ShowPrevSaves()
	{
		if(savesGroupPosN > 0)
		{
			savesGroupPosN--;
			ShowSavesN(savesGroupPosN);
		}
	}

	void ShowSavesN(int posN)
	{
		if(co != null)
			StopCoroutine(co);
		
		switch(posN)
		{
			case 0:
				co = IEMoveToPosX(0f);
				StartCoroutine(co);
			break;
			case 1:
				co = IEMoveToPosX(-800f);
				StartCoroutine(co);
			break;
			case 2:
				co = IEMoveToPosX(-1600f);
				StartCoroutine(co);
			break;
		default: break;
		}
	}

	IEnumerator IEMoveToPosX(float xPos)
	{
		
		while(saves_group.GetLocalXPos() != xPos+1f)
		{
			//saves_group.position = Vector2.MoveTowards(saves_group.position, _savesGroupPosTemp, 3 * Time.deltaTime);

			saves_group.SetLocalXPos(Mathf.Lerp(saves_group.GetLocalXPos(),xPos,0.1f));
			yield return new WaitForSeconds(0.001f);
		}

	}

    #endregion old
}
