using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkData {

    // device model
    public int device_ID;

    // for BookModel
    public int book_ID;
    public string book_description;

    // for ActivityModel
    public int activity_ID;
    public int activity_book_ID;
    public string activity_description;
    public string activity_module;
    public int activity_set;

    // for StudentActivityModel
    public int studentActivity_ID;
    public int studentActivity_sectionId;
    public int studentActivity_studentId;
    public int studentActivity_bookId;
    public int studentActivity_activityId;
    public string studentActivity_grade;
    public int studentActivity_playCount;

    // StudentBookModel
    public int studentBook_Id;
    public int studentBook_SectionId;
    public int studentBook_StudentId;
    public int studentBook_bookId;
    public int studentBook_readCount;
    public int studentBook_readToMeCount;
    public int studentBook_autoReadCount;
}
