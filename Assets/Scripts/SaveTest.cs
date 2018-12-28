using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SaveTest : MonoBehaviour
{
    //SAVE ACTIVITY

    //these static variables are set on ButtonActivity.cs
    public static StoryBook storyBook;
    public static Module module;

    static int set;
    public static int Set
    {
        get { return set; }
        set { set = value; }
    }

    public static void Save()
    {
        //print("USERNAME"+storyBook.ToString() + ", " + module.ToString() +  ", " + Set);
        //-----------------------------------------------------------------------------------add scene name here
     
        print("SAVED " + ScoreManager.ins.GetGrade());
        print(StoryBookSaveManager.ins.Username + storyBook.ToString() + SceneManager.GetActiveScene().name + module.ToString() + set);

        //DataService ds = new DataService();
        DataService.Open();

        print(storyBook.ToString());
        string bookname = storyBook.ToString();
        string modulename = module.ToString();
        string scenename = SceneManager.GetActiveScene().name;
        string grade = ScoreManager.ins.GetGrade();

        BookModel book = DataService._connection.Table<BookModel>().Where(a => a.Description == bookname).FirstOrDefault();
       
        ActivityModel activityModel = DataService._connection.Table<ActivityModel>().Where(
             x => x.BookId == book.Id &&
             x.Description == scenename &&
             x.Module == modulename &&
             x.Set == set).FirstOrDefault();

        StudentActivityModel studentActivityModel = DataService._connection.Table<StudentActivityModel>().Where(x =>
            x.SectionId == StoryBookSaveManager.ins.activeSection_id &&
            x.StudentId == StoryBookSaveManager.ins.activeUser_id &&
            x.BookId == activityModel.BookId &&
            x.ActivityId == activityModel.Id
            ).FirstOrDefault();


        // network data, activity
        NetworkData networkData = new NetworkData ();
        networkData.activity_book_ID = book.Id;
        networkData.activity_description = scenename;
        networkData.activity_module = modulename;
        networkData.activity_set = set;
        // book
        networkData.book_description = bookname;

        if (studentActivityModel == null)
        {
            StudentActivityModel model = new StudentActivityModel
            {
                Id =0,
                SectionId = StoryBookSaveManager.ins.activeSection_id,
                StudentId = StoryBookSaveManager.ins.activeUser_id,
                BookId = activityModel.BookId,
                ActivityId = activityModel.Id,
                Grade = grade,
                PlayCount = 1

            };

            // network data
            networkData.studentActivity_ID = model.Id;
            networkData.studentActivity_sectionId = model.SectionId;
            networkData.studentActivity_studentId = model.StudentId;
            networkData.studentActivity_bookId = model.BookId;
            networkData.studentActivity_activityId = model.ActivityId;
            networkData.studentActivity_grade = model.Grade;
            networkData.studentActivity_playCount = model.PlayCount;

            DataService._connection.Insert(model);

            // send data to server for insert
            if (MainNetwork.Instance.clientSendFile.isActiveAndEnabled)
                MainNetwork.Instance.clientSendFile.SendData(networkData, ClientSendFile.MessageGroup.Insert);
        }
        else
        {
            print(grade + " updated!" );
            int playN = studentActivityModel.PlayCount + 1;

            string command = "Update StudentActivityModel set Grade='" + grade
                + "', PlayCount='" + playN + "' where Id='" + studentActivityModel.Id + "'";

            DataService._connection.Execute(command);

            // network data
            networkData.studentActivity_grade = grade;
            networkData.studentActivity_playCount = playN;
            networkData.studentActivity_ID = studentActivityModel.Id;

            if (MainNetwork.Instance.clientSendFile.isActiveAndEnabled)
                MainNetwork.Instance.clientSendFile.SendData(networkData, ClientSendFile.MessageGroup.Update);
        }


        //PlayerPrefs.SetString("USERNAME" + storyBook.ToString() + module.ToString() + set, "done");
    }


}
