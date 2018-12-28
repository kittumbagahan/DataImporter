using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnableGames : MonoBehaviour {

    public static EnableGames ins;

    void Start()
    {
        ins = this;
        if(0 == UserRestrictionController.ins.restriction)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            if (IsAvailable() == 0)
            {
                GetComponent<Button>().interactable = false;
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }
        }
      


    }


    int IsAvailable()
    {
        //DataService ds = new DataService();
        DataService.Open();
        string bookname = StoryBookSaveManager.ins.selectedBook.ToString();
        BookModel book = DataService._connection.Table<BookModel>().Where(x => x.Description == bookname).FirstOrDefault();
        var model = DataService._connection.Table<StudentBookModel>().Where(x => x.SectionId == StoryBookSaveManager.ins.activeSection_id &&
        x.StudentId == StoryBookSaveManager.ins.activeUser_id && x.BookId == book.Id).FirstOrDefault();

        if (model == null) return 0;
        DataService.Close();
        return model.ReadCount + model.ReadToMeCount + model.AutoReadCount;
    }
}
