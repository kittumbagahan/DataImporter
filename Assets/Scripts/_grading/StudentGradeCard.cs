using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StudentGradeCard {

    public int sectionId;
    public int studentId;
    public string fullname;
    public double wordGrade;
    public double observationGrade;

    public StudentGradeCard(int sectionId, int studentId, string fullname, double word, double observation)
    {
        this.sectionId = sectionId;
        this.studentId = studentId;
        this.fullname = fullname;
        wordGrade = word;
        observationGrade = observation;
    }
}
