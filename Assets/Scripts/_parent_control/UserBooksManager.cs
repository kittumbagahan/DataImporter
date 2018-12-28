using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using SQLite4Unity3d;
using System.Linq;
public class UserBooksManager : MonoBehaviour
{



    public Sprite sprtFavBox,
        sprtChatCat,
        sprtColorsMixed,
        sprtAfterTheRain,
        sprtJoeySchool,
        sprtSoundsFan,
        sprtYumShapes,
        sprtTinaJun,
        sprtWhatSee,
        sprtAbc;
    public List<UserBook> userBookLst;
    public List<int> tempLst;


    [SerializeField]
    TextMeshProUGUI txtUserBookHeader;
    [SerializeField]
    Text utxtBookHeader;

    public int userId;

    [SerializeField]
    GameObject utoBubble;

    StudentModel sm;
    void Start()
    {

        utoBubble.SetActive(false);
    }

    public void Show(int key)
    {
        userId = key;
        //DataService ds = new DataService();
        DataService.Open();
        sm = DataService._connection.Table<StudentModel>().Where(x => x.SectionId == StoryBookSaveManager.ins.activeSection_id &&
        x.Id == userId).FirstOrDefault();
        
        gameObject.SetActive(true);

        utxtBookHeader.text = sm.Nickname + "'s" + " favorite books";
        DataService.Close();
        LoadUsage();

        Invoke("Sort", 1f);
    }

    int GetReadCount(string bookname)
    {
        //DataService ds = new DataService();
        DataService.Open();
        BookModel book = DataService._connection.Table<BookModel>().Where(x => x.Description == bookname).FirstOrDefault();
        StudentBookModel sbm = DataService._connection.Table<StudentBookModel>().Where(x => x.SectionId == StoryBookSaveManager.ins.activeSection_id &&
        x.StudentId == userId && x.BookId == book.Id).FirstOrDefault();
        
        if (sbm == null) return 0;
        int cnt = sbm.ReadCount + sbm.ReadToMeCount + sbm.AutoReadCount;
        DataService.Close();
        return cnt;
    }
  
    int GetPlayCount(string bookname)
    {
        //DataService ds = new DataService();
        DataService.Open();
        BookModel book = DataService._connection.Table<BookModel>().Where(x => x.Description == bookname).FirstOrDefault();
        var activity = DataService._connection.Table<ActivityModel>().Where(x => x.BookId == book.Id);
        var studentActivity = DataService._connection.Table<StudentActivityModel>().Where(x => x.SectionId == StoryBookSaveManager.ins.activeSection_id &&
        x.StudentId == userId && x.BookId == book.Id);
        
        //ToConsole(activity);
        if (studentActivity == null) return 0;
        int sum = studentActivity.Sum(x => x.PlayCount);

        DataService.Close();
        return sum;
    }
    void LoadUsage()
    {
        print("HELLO HELLO " + GetPlayCount(StoryBook.FAVORITE_BOX.ToString()));
        //hindi nakaayos sa image                                                                  

        //StoryBookSaveManager.instance.oldUsername + "FavoriteBox";
        // 
        //        AfterTheRain
        //          ABC_Circus
        //          ChatWithMyCat
        //          Colors All Mixed Up
        //          JoeyGoesToSchool
        //          SoundsFantastic
        //          TinaAndJun
        //          WhatDidYouSee
        //          YummyShapes

        for (int i = 0; i < userBookLst.Count; i++)
        {
            //userBookLst[i].readCount = 0;
            //userBookLst[i].playedCount = 0;
            switch (userBookLst[i].CoverSprite.name)
            {
                case "ABC-Circus-TabletCover-6":
                    userBookLst[i].readCount = GetReadCount(StoryBook.ABC_CIRCUS.ToString());
                    userBookLst[i].playedCount = GetPlayCount(StoryBook.ABC_CIRCUS.ToString());
                    break;
                case "After The Rain Page-tabletcover":
                    userBookLst[i].readCount = GetReadCount(StoryBook.AFTER_THE_RAIN.ToString());
                    userBookLst[i].playedCount = GetPlayCount(StoryBook.AFTER_THE_RAIN.ToString());
                    break;
                case "Chat with my Cat p00 Cover":
                    userBookLst[i].readCount = GetReadCount(StoryBook.CHAT_WITH_MY_CAT.ToString());
                    userBookLst[i].playedCount = GetPlayCount(StoryBook.CHAT_WITH_MY_CAT.ToString());
                    break;
                case "Colors All Mixed Up-Cover-7":
                    userBookLst[i].readCount = GetReadCount(StoryBook.COLORS_ALL_MIXED_UP.ToString());
                    userBookLst[i].playedCount = GetPlayCount(StoryBook.COLORS_ALL_MIXED_UP.ToString());
                    break;
                case "Joey Goes to School-TabletCover":
                    userBookLst[i].readCount = GetReadCount(StoryBook.JOEY_GO_TO_SCHOOL.ToString());
                    userBookLst[i].playedCount = GetPlayCount(StoryBook.JOEY_GO_TO_SCHOOL.ToString());
                    break;
                case "My Favorite box-Tablet cover":
                    userBookLst[i].readCount = GetReadCount(StoryBook.FAVORITE_BOX.ToString());
                    userBookLst[i].playedCount = GetPlayCount(StoryBook.FAVORITE_BOX.ToString());
                    break;
                case "Sounds Fantastic-TabletCover-8":
                    userBookLst[i].readCount = GetReadCount(StoryBook.SOUNDS_FANTASTIC.ToString());
                    userBookLst[i].playedCount = GetPlayCount(StoryBook.SOUNDS_FANTASTIC.ToString());
                    break;
                case "Tina and Jun-TabletCover-9":
                    userBookLst[i].readCount = GetReadCount(StoryBook.TINA_AND_JUN.ToString());
                    userBookLst[i].playedCount = GetPlayCount(StoryBook.TINA_AND_JUN.ToString());
                    break;
                case "What Did You See - Cover":
                    userBookLst[i].readCount = GetReadCount(StoryBook.WHAT_DID_YOU_SEE.ToString());
                    userBookLst[i].playedCount = GetPlayCount(StoryBook.WHAT_DID_YOU_SEE.ToString());
                    break;
                case "Yummy-Shapes-tablet-cover-10":
                    userBookLst[i].readCount = GetReadCount(StoryBook.YUMMY_SHAPES.ToString());
                    userBookLst[i].playedCount = GetPlayCount(StoryBook.YUMMY_SHAPES.ToString());
                    break;
                default:

                    break;
            }
        }

    }

    void Sort()
    {
        Debug.Log("SOOOOOOOOOOORT");
        int tempCount;
        Sprite tempCover;
        // BookAccuracy tempBookAcc;
        bool swapped = false;
        for (int x = 0; x < userBookLst.Count; x++)
        {
            for (int j = 0; j < userBookLst.Count - 1; j++)
            {

                if ((userBookLst[j].readCount + userBookLst[j].playedCount) < (userBookLst[j+1].readCount + userBookLst[j+1].playedCount))
                {
                    tempCover = userBookLst[j + 1].Cover.sprite;
                    tempCount = userBookLst[j + 1].readCount;
                    // tempBookAcc = userBookLst[j + 1].bookAccuracy;
                    //read count
                    userBookLst[j + 1].ReadCount = userBookLst[j].readCount;
                    userBookLst[j].ReadCount = tempCount;

                    //played count
                    tempCount = userBookLst[j + 1].playedCount;
                    userBookLst[j + 1].playedCount = userBookLst[j].playedCount;
                    userBookLst[j].playedCount = tempCount;

                    userBookLst[j + 1].Cover.sprite = userBookLst[j].Cover.sprite;
                    userBookLst[j].Cover.sprite = tempCover;


                }
                //if (!swapped) break;
            }
        }

        for (int i = 0; i < userBookLst.Count; i++)
        {
            //print("PRINTO! " + userBookLst[i].CoverSprite.name);

            switch (userBookLst[i].CoverSprite.name)
            {
                case "ABC-Circus-TabletCover-6":
                    if(userBookLst[i].gameObject.GetComponent<AccuracyABC>() == null)
                    userBookLst[i].gameObject.AddComponent<AccuracyABC>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "After The Rain Page-tabletcover":
                    if (userBookLst[i].gameObject.GetComponent<AccuracyAfterTheRain>() == null)
                        userBookLst[i].gameObject.AddComponent<AccuracyAfterTheRain>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Chat with my Cat p00 Cover":
                    if (userBookLst[i].gameObject.GetComponent<AccuracyChatWithCat>() == null)
                        userBookLst[i].gameObject.AddComponent<AccuracyChatWithCat>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Colors All Mixed Up-Cover-7":
                    if (userBookLst[i].gameObject.GetComponent<AccuracyColorsAllMixedUp>() == null)
                        userBookLst[i].gameObject.AddComponent<AccuracyColorsAllMixedUp>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Joey Goes to School-TabletCover":
                    if (userBookLst[i].gameObject.GetComponent<AccuracyJoeyGoesToSchool>() == null)
                        userBookLst[i].gameObject.AddComponent<AccuracyJoeyGoesToSchool>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "My Favorite box-Tablet cover":
                    if (userBookLst[i].gameObject.GetComponent<AccuracyFavoriteBox>() == null)
                        userBookLst[i].gameObject.AddComponent<AccuracyFavoriteBox>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Sounds Fantastic-TabletCover-8":
                    if (userBookLst[i].gameObject.GetComponent<AccuracySoundsFantastic>() == null)
                        userBookLst[i].gameObject.AddComponent<AccuracySoundsFantastic>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Tina and Jun-TabletCover-9":
                    if (userBookLst[i].gameObject.GetComponent<AccuracyTinaAndJun>() == null)
                        userBookLst[i].gameObject.AddComponent<AccuracyTinaAndJun>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "What Did You See - Cover":
                    if (userBookLst[i].gameObject.GetComponent<AccuracyWhatDidYouSee>() == null)
                        userBookLst[i].gameObject.AddComponent<AccuracyWhatDidYouSee>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Yummy-Shapes-tablet-cover-10":
                    if (userBookLst[i].gameObject.GetComponent<AccuracyYummyShapes>() == null)
                        userBookLst[i].gameObject.AddComponent<AccuracyYummyShapes>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                default:

                    break;
            }

            userBookLst[i].TxtInfo.text = "Times Read: " + "<#ffc600>" + userBookLst[i].readCount.ToString() + "</color>";
            userBookLst[i].TxtInfo.text += "\n" + "Times Played: " + "<#ffc600>" + userBookLst[i].playedCount.ToString() + "</color>";
            userBookLst[i].TxtInfo.text += "\n" + "Accuracy: " + "<#ffc600>" + userBookLst[i].AccuracyRate.ToString() + "%" + "</color>";

            userBookLst[i].UTxtInfo.text = "Times Read: " + "<#ffc600>" + userBookLst[i].readCount.ToString() + "</color>";
            userBookLst[i].UTxtInfo.text += "\n" + "Times Played: " + "<#ffc600>" + userBookLst[i].playedCount.ToString() + "</color>";
            userBookLst[i].UTxtInfo.text += "\n" + ": " + "<#ffc600>" + userBookLst[i].AccuracyRate.ToString() + "%" + "</color>";

        }



        if (userBookLst[0].playedCount + userBookLst[0].readCount >= 5)
        {
            //print("TIFFANY! " + userBookLst[0].CoverSprite.name + userBookLst[0].playedCount + userBookLst[0].readCount);

            switch (userBookLst[0].CoverSprite.name)
            {
                case "ABC-Circus-TabletCover-6":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = sm.Nickname + "'s" +
                        " favorite book is ABC Circus Your child LOVES TO LEARN ABOUT NEW WORDS and could grow up to be a teacher or scholar! You might want to encourage your child to read and write more!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "After The Rain Page-tabletcover":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = sm.Nickname + "'s" +
                        " favorite book is After the Rain this means that your child could grow up to be event planner or project manager";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Chat with my Cat p00 Cover":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = sm.Nickname + "'s" +
                        " favorite book is Chat With My Cat! Your child is ARTISTIC  and could grow up to be a POET!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Colors All Mixed Up-Cover-7":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = sm.Nickname + "'s" +
                        "  favorite book is Colors All Mixed Up! Your child is ARTISTIC and could grow up to be an artist! You might want to encourage your child to draw or paint!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Joey Goes to School-TabletCover":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = sm.Nickname + "'s" +
                        " favorite book is Joey Goes To School! Your child LIKES CONQUERING FEAR and have NEW EXPERIENCES and could grow up to be an explorer! You might want to encourage your child to try new things and go places!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "My Favorite box-Tablet cover":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = sm.Nickname + "'s" +
                        " favorite book is My Favorite Box! Your child is IMAGINATIVE and could grow up to be a WRITER! You might want to encourage your child create and tell his/her own stories!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Sounds Fantastic-TabletCover-8":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = sm.Nickname + "'s" +
                        " favorite book is Sounds Fantastic Your child LOVES TO LISTEN TO SOUNDS and could grow up to be a composer! You might want to encourage your child to sing or learn a musical instrument!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Tina and Jun-TabletCover-9":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = sm.Nickname + "'s" +
                        " favorite book is Tina and Jun! Your child LOVES MUSIC and could grow up to be a Rock/Pop star! You might want to encourage your child to sing or learn a musical instrument!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "What Did You See - Cover":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = sm.Nickname + "'s" +
                        " favorite book is What did you See?  Your child likes to be SOCIABLE and could grow up to be a reporter! You might want to encourage your child to play and interact with other kids and other people!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Yummy-Shapes-tablet-cover-10":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = sm.Nickname + "'s" +
                        " favorite book is YUMMY SHAPES! Your child is ARTISTIC and could grow up to be chef! You might want to encourage your child to do more creative and artistic activities!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                default:
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = "";
                    break;
            }
        }


    }



}
